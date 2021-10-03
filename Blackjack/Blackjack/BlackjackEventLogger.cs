using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;

namespace Blackjack
{
    public class EventMessageArgs : EventArgs
    {
        public string Message { get; }
        public EventMessageArgs(string message)
        {
            Message = message;
        }
    }

    public class FileLogger
    {
        private readonly string path;
        public FileLogger(string path)
        {
            this.path = path;
            File.Delete(path);
        }
        public void OnEventMessage(object obj, EventMessageArgs args)
        {
            using (StreamWriter writer = File.AppendText(path))
            {
                writer.WriteLine(args.Message);
            }
        }
    }

    public class StdOutLogger
    {
        public void OnEventMessage(object obj, EventMessageArgs args)
        {
            Console.WriteLine(args.Message);
        }
    }

    public class BlackjackStatsLogger : BlackjackEventLogger
    {
        public void OnTableRoundEnd(object obj, EventArgs _)
        {
            if (obj is BlackjackTable table)
            {
                Log($"{table.Count[BlackjackCountEnum.HiLo]},{table.TableBank.Balance}");
            }
        }
    }

    public class BlackjackEventLogger
    {
        public event EventHandler<EventMessageArgs> Logging;

        public void OnTableRoundBegan(object obj, EventArgs _)
        {
            if (obj is BlackjackTable t)
            {
                Log($"began round on Table: {t.NumOccupiedSlots} seats occupied, ${t.TableBank.Balance} in bank");
            }
        }
        public void OnTableRoundEnded(object obj, EventArgs _) 
        {
            if (obj is BlackjackTable t)
            {
                Log($"ended round on Table: {t.NumOccupiedSlots} seats occupied, ${t.TableBank.Balance} in bank");
            }
        }
        public void OnTableHandDealt(object _, HandDealtEventArgs args) 
        {
            Log($"player {args.Player} dealt hand {args.Hand} (Total={args.Hand.Value})");
        }
        public void OnTableSeatChanged(object _, SeatingEventArgs args) 
        {
            StringBuilder msg = new($"player {args.NewPlayer} took ");
            if (args.PreviousPlayer == null)
            {
                msg.Append($"empty seat {args.SeatIndex}");
            }
            else
            {
                msg.Append($"seat {args.SeatIndex} from player {args.PreviousPlayer}");
            }
            Log(msg.ToString());
        }
        public void OnTableSlotRoundBegan(object obj, EventArgs _)
        {
            if (obj is BlackjackTableSlot s)
            {
                Log($"began round on TableSlot with player {s.Player}");
            }
        }
        public void OnTableSlotRoundEnded(object obj, EventArgs _) 
        {
            if (obj is BlackjackTableSlot s)
            {
                Log($"ended round on TableSlot with player {s.Player}");
            }
        }
        public void OnTableSlotActing(object obj, EventArgs _) 
        {
            if (obj is BlackjackTableSlot s)
            {
                Log($"acting on TableSlot with player {s.Player}");
            }
        }
        public void OnTableSlotActingHand(object obj, EventArgs _)
        {
            if (obj is BlackjackTableSlot s)
            {
                Log($"acting on new Hand on TableSlot with player {s.Player}");
            }
        }
        public void OnActionExecuted(object obj, BlackjackActionEventArgs args)
        {
            if (obj is BlackjackTableSlot slot)
            {
                string handString = args.Done ? "completed" : "continues";
                Log($"executed action {args.Kind}; hand {handString} (Total={slot.Hand.Value})");
            }
        }
        public void OnHitExecuted(object obj, BlackjackHitActionEventArgs args)
        {
            if (obj is BlackjackTableSlot slot)
            {
                Log($"hit yielded a {args.CardReceived}, Total={slot.Hand.Value}");
            }
        }
        public void OnPlayerDecisionMade(object obj, BlackjackDecisionEventArgs args) 
        { 
            if (obj is Player p)
            {
                Log($"player {p} {MakeDecisionString(args)}");
            }
        }
        public void OnPlayerEarlySurrenderDecision(object obj, BlackjackEarlySurrenderEventArgs args) 
        {
            if (obj is Player p)
            {
                Log($"player {p} {MakeEarlySurrenderDecisionString(args)}");
            }
        }
        public void OnPlayerBetMade(object obj, BlackjackBetEventArgs args) 
        {
            if (obj is Player p)
            {
                Log($"player {p} {MakeBetString(args)}");
            }
        }
        public void OnPlayerInsuranceDecision(object obj, BlackjackInsuranceEventArgs args) 
        {
            if (obj is Player p)
            {
                Log($"player {p} {MakeInsuranceDecisionString(args)}");
            }
        }
        public void OnDecisionMade(object _, BlackjackDecisionEventArgs args)
        {
            Log(MakeDecisionString(args));
        }
        public void OnEarlySurrenderDecision(object _, BlackjackEarlySurrenderEventArgs args) 
        {
            Log(MakeEarlySurrenderDecisionString(args));
        }
        public void OnBetMade(object _, BlackjackBetEventArgs args)
        {
            Log(MakeBetString(args));
        }
        public void OnInsuranceDecision(object _, BlackjackInsuranceEventArgs args) 
        {
            Log(MakeInsuranceDecisionString(args));
        }
        public void OnPlayerSpent(object obj, BankTransactionEventArgs args)
        {
            if (obj is Player p)
            {
                Log($"player {p} spent {args.Amount}");
            }
        }
        public void OnPlayerEarned(object obj, BankTransactionEventArgs args)
        {
            if (obj is Player p)
            {
                Log($"player {p} earned {args.Amount}");
            }
        }
        public void OnSpent(object obj, BankTransactionEventArgs args)
        {
            Log($"spent ${args.Amount}, now have ${args.Balance}");
        }
        public void OnEarned(object obj, BankTransactionEventArgs args)
        {
            Log($"earned ${args.Amount}, now have ${args.Balance}");
        }
        public void OnHouseEarned(object _, BankTransactionEventArgs args)
        {
            Log($"house (${args.Balance}) won {args.Amount}");
        }
        public void OnHouseSpent(object obj, BankTransactionEventArgs args)
        {
            Log($"house (${args.Balance}) lost {args.Amount}");
        }
        public void OnShoeShuffling(object obj, EventArgs _)
        {
            Log("shoe shuffled");
        }
        public void OnShoeExhausted(object obj, EventArgs _)
        {
            Log("shoe exhausted");
        }
        public void OnShoeDealt(object _, DealtEventArgs args)
        {
            string msg = $"shoe dealt {args.DealtCards.Length}";
            if (args.Visible)
            {
                msg += $" cards: {String.Join(",", args.DealtCards)}";
            }
            Log(msg);
        }
        public void OnShoeBurnt(object _, BurnEventArgs args)
        {
            Log($"shoe burnt {args.NumBurnt} cards");
        }
        public void OnCountChanged(object obj, EventArgs _)
        {
            if (obj is BlackjackCount)
            {
                Log($"count changed to {obj}");
            }
        }

        protected void Log(string message)
        {
            Logging?.Invoke(this, new EventMessageArgs(message));
        }
        private static string MakeDecisionString(BlackjackDecisionEventArgs args)
        {
            return $"decided on {args.Decision} from [{string.Join(", ", args.AvailableActions)}] with hand {args.Hand} (Total={args.Hand.Value}) vs {args.UpCard.Rank}";
        }
        private static string MakeEarlySurrenderDecisionString(BlackjackEarlySurrenderEventArgs args)
        {
            string decisionString = args.Surrendered ? "accepted" : "declined";
            return $"{decisionString} early surrender with hand {args.Hand} (Total={args.Hand.Value}) vs {args.UpCard.Rank}";
        }
        private static string MakeBetString(BlackjackBetEventArgs args)
        {
            return $"made bet of ${args.Amount}";
        }
        private static string MakeInsuranceDecisionString(BlackjackInsuranceEventArgs args)
        {
            string decisionString = args.Insured ? "accepted" : "declined";
            return $"{decisionString} insurance with hand {args.Hand} (Total={args.Hand.Value}) vs {args.UpCard.Rank}";
        }
    }
}
