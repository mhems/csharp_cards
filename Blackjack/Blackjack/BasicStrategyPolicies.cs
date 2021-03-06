using Cards;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class BasicStrategyPolicies : BlackjackDecisionPolicy
    {
        class StrategyChart
        {
            private readonly Dictionary<int, List<BlackjackActionEnum>> chart = new();
            public BlackjackActionEnum this[int total, Card card]
            {
                get => chart[total][BlackjackHand.CardValue(card) - 2];
            }

            public static StrategyChart FromFile(string file)
            {
                if (File.Exists(file))
                {
                    using (StreamReader reader = new(file))
                    {
                        StrategyChart chart = new();

                        string line, total, actions;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] tokens = line.Split(":");
                            int totalInt = 0;
                            List<BlackjackActionEnum> actionEnums = new();
                            if (tokens.Length != 2)
                            {
                                throw new ArgumentException("line must have only one : separator");
                            }
                            (total, actions) = (tokens[0], tokens[1]);
                            try
                            {
                                totalInt = Int32.Parse(total);
                            }
                            catch (Exception e)
                            {
                                throw new ArgumentException("total must be int", e);
                            }
                            foreach (string actionString in actions.Split(","))
                            {
                                try
                                {
                                    actionEnums.Add((BlackjackActionEnum)Enum.Parse(typeof(BlackjackActionEnum), actionString));
                                }
                                catch (Exception e)
                                {
                                    throw new ArgumentException($"unrecognized {nameof(BlackjackActionEnum)} {actionString}", e);
                                }
                            }
                            if (actionEnums.Count != 10)
                            {
                                throw new ArgumentException("chart must advise on all 10 rank values");
                            }
                            chart.chart.Add(totalInt, actionEnums);
                        }

                        return chart;
                    }
                }
                return null;
            }

            public void ToFile(string file)
            {
                using (StreamWriter writer = new(file))
                {
                    foreach (int total in chart.Keys.OrderByDescending(i => i))
                    {
                        writer.Write(total);
                        writer.Write(':');
                        writer.Write(string.Join(",", chart[total].Select(e => e.ToString())));
                        writer.WriteLine();
                    }
                }
            }
        }

        private readonly StrategyChart BasicChart;
        private readonly StrategyChart HardChart;
        private readonly StrategyChart SoftChart;
        private readonly StrategyChart PairChart;

        public BasicStrategyPolicies()
        {
            BasicChart = StrategyChart.FromFile("BasicChart.csv");
            HardChart = StrategyChart.FromFile("HardChart.csv");
            SoftChart = StrategyChart.FromFile("SoftChart.csv");
            PairChart = StrategyChart.FromFile("PairChart.csv");
        }

        public void Save(string path)
        {
            BasicChart?.ToFile(Path.Join(path, "BasicChart.csv"));
            HardChart?.ToFile(Path.Join(path, "HardChart.csv"));
            SoftChart?.ToFile(Path.Join(path, "SoftChart.csv"));
            PairChart?.ToFile(Path.Join(path, "PairChart.csv"));
        }

        protected override BlackjackActionEnum DecideInner(BlackjackHand hand, Card upCard, HashSet<BlackjackActionEnum> availableActions)
        {
            BlackjackActionEnum action;
            int total = hand.Value;

            if (PairChart != null && hand.IsPair)
            {
                action = PairChart[BlackjackHand.CardValue(hand[0]), upCard];
                if (availableActions.Contains(action))
                {
                    return action;
                }
            }
            if (SoftChart != null && hand.IsSoft)
            {
                action = SoftChart[total, upCard];
                if (availableActions.Contains(action))
                {
                    return action;
                }
            }
            if (HardChart != null)
            {
                action = HardChart[total, upCard];
                if (availableActions.Contains(action))
                {
                    return action;
                }
            }
            return BasicChart[total, upCard];
        }
    }

    public class BasicEarlySurrenderPolicy : BlackjackEarlySurrenderPolicy
    {
        public IBlackjackConfig Config { get; }

        public BasicEarlySurrenderPolicy(IBlackjackConfig config)
        {
            Config = config;
        }

        protected override bool SurrenderInner(BlackjackHand hand, Card upCard)
        {
            if (upCard.IsAce)
            {
                if (!hand.IsSoft)
                {
                    return ((hand.Value >= 5 && hand.Value <= 7) ||
                        (hand.Value >= 12 && hand.Value <= 17) ||
                        (Config.DealerHitsSoft17 && hand.Value == 4));
                }
                else
                {
                    return false;
                }
            }
            else if (BlackjackHand.CardValue(upCard) == 10)
            {
                if (!hand.IsSoft)
                {
                    return hand.Value >= 14 && hand.Value <= 16;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return upCard.Rank == Card.RankEnum.Nine && hand.Value == 16 && !hand.IsPair;
            }
        }
    }
}
