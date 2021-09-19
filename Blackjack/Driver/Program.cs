using System;
using Cards;
using Blackjack;

namespace Driver
{
    public class Program
    {
        static void Main(string[] args)
        {
            BlackjackPlayer player;

            BlackjackPlayer human = new("me");
            human.Bank.Deposit(1000);

            BlackjackPlayer bot = new("bot");
            bot.Bank.Deposit(1000000);

            IBlackjackConfig config = new StandardBlackjackConfig();

            human.BettingPolicy = new MinimumBettingPolicy(config);
            human.DecisionPolicy = new InputDecisionPolicy();
            human.InsurancePolicy = new DeclineInsurancePolicy();

            bot.BettingPolicy = new MinimumBettingPolicy(config);
            bot.DecisionPolicy = new BasicStrategyPolicies();
            bot.InsurancePolicy = new DeclineInsurancePolicy();

            BlackjackTable table = new(1, config);

            //player = human;
            player = bot;

            table.SeatPlayer(player);

            BlackjackEventLogger eventLogger = new();
            table.AddLogger(eventLogger);
            FileLogger fileLogger = new("log.txt");
            eventLogger.Logging += fileLogger.OnEventMessage;
            StdOutLogger printLogger = new();
            eventLogger.Logging += printLogger.OnEventMessage;

            int numRounds = 0;
            while(true)
            {
                table.PlayRound();
                numRounds++;

                Console.WriteLine($"{numRounds} rounds played, you have ${player.Bank.Balance}");

                if (player.DecisionPolicy is InputDecisionPolicy)
                {
                    Console.WriteLine("continue (y/n)? > ");
                    if (Console.ReadLine().ToLower().StartsWith("n"))
                    {
                        break;
                    }
                }
            }
        }
    }
}
