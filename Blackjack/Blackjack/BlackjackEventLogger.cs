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
        }
        public void OnEventMessage(object obj, EventMessageArgs args)
        {
            using (StreamWriter writer = new(path))
            {
                writer.WriteLine(args.Message);
            }
        }
    }

    public class BlackjackEventLogger
    {
        public event EventHandler<EventMessageArgs> Logging;

        public void OnTableRoundBegan(object obj, EventArgs args) 
        {
            if (obj is BlackjackTable t)
            {
                Log($"began round on Table - {t.NumOccupiedSlots} seats occupied, ${t.TableBank} in bank");
            }
        }
        public void OnTableRoundEnded(object obj, EventArgs args) 
        {
            if (obj is BlackjackTable t)
            {
                Log($"ended round on Table - {t.NumOccupiedSlots} seats occupied, ${t.TableBank} in bank");
            }
        }
        public void OnTableHandDealt(object obj, HandDealtEventArgs args) 
        {
            Log($"player {args.Player} dealt hand {args.Hand}");
        }
        public void OnTableSeatChanged(object obj, SeatingEventArgs args) 
        {
            StringBuilder msg = new($"player {args.NewPlayer} took");
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
        public void OnTableSlotRoundBegan(object obj, EventArgs args)
        {
            if (obj is BlackjackTableSlot s)
            {
                Log($"began round on TableSlot with player {s.Player}");
            }
        }
        public void OnTableSlotRoundEnded(object obj, EventArgs args) 
        {
            if (obj is BlackjackTableSlot s)
            {
                Log($"ended round on TableSlot with player {s.Player}");
            }
        }
        public void OnTableSlotActing(object obj, EventArgs args) 
        {
            if (obj is BlackjackTableSlot s)
            {
                Log($"acting on TableSlot with player {s.Player}");
            }
        }
        public void OnTableSlotActingHand(object obj, EventArgs args)
        {
            if (obj is BlackjackTableSlot s)
            {
                Log($"acting on new Hand on TableSlot with player {s.Player}");
            }
        }
        public void OnActionExecuted(object obj, BlackjackActionEventArgs args) 
        {
            string handString = args.Done ? "completed" : "continues";
            Log($"executed action {args.Kind}; hand {handString}");
        }
        public void OnHitExecuted(object obj, BlackjackHitActionEventArgs args)
        {
            Log($"hit yielded a {args.CardReceived}");
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
        public void OnDecisionMade(object obj, BlackjackDecisionEventArgs args)
        {
            Log(MakeDecisionString(args));
        }
        public void OnEarlySurrenderDecision(object obj, BlackjackEarlySurrenderEventArgs args) 
        {
            Log(MakeEarlySurrenderDecisionString(args));
        }
        public void OnBetMade(object obj, BlackjackBetEventArgs args)
        {
            Log(MakeBetString(args));
        }
        public void OnInsuranceDecision(object obj, BlackjackInsuranceEventArgs args) 
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
        public void OnShoeShuffling(object obj, EventArgs args)
        {
            Log("shoe shuffled");
        }
        public void OnShoeExhausted(object obj, EventArgs args)
        {
            Log("shoe exhausted");
        }
        public void OnShoeDealt(object obj, DealtEventArgs args)
        {
            Log($"shoe dealt {args.DealtCards.Length} cards: {String.Join(",", args.DealtCards)}");
        }
        public void OnShoeBurnt(object obj, BurnEventArgs args)
        {
            Log($"shoe burnt {args.NumBurnt} cards");
        }

        private void Log(string message)
        {
            Logging?.Invoke(this, new EventMessageArgs(message));
        }
        private static string MakeDecisionString(BlackjackDecisionEventArgs args)
        {
            return $"decided on {args.Decision} from [{string.Join(", ", args.AvailableActions)}] with hand {args.Hand} vs {args.UpCard.Rank}";
        }
        private static string MakeEarlySurrenderDecisionString(BlackjackEarlySurrenderEventArgs args)
        {
            string decisionString = args.Surrendered ? "accepted" : "declined";
            return $"{decisionString} early surrender with hand {args.Hand} vs {args.UpCard.Rank}";
        }
        private static string MakeBetString(BlackjackBetEventArgs args)
        {
            return $"made bet of ${args.Amount}";
        }
        private static string MakeInsuranceDecisionString(BlackjackInsuranceEventArgs args)
        {
            string decisionString = args.Insured ? "accepted" : "declined";
            return $"{decisionString} insurance with hand {args.Hand} vs {args.UpCard.Rank}";
        }
    }
}
