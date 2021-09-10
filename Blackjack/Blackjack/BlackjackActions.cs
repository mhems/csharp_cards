using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;

namespace Blackjack
{
    public enum BlackjackActionEnum
    {
        Hit,
        Stand,
        Double,
        Split,
        LateSurrender
    }

    public class ActionUnavailableException : Exception
    {
        public ActionUnavailableException() : base() { }
        public ActionUnavailableException(string msg) : base(msg) { }
        public ActionUnavailableException(string msg, Exception inner) : base(msg, inner) { }
    }

    public abstract class BlackjackAction
    {
        public abstract BlackjackActionEnum Kind { get; }

        public event EventHandler<BlackjackActionEventArgs> Acted;

        public bool Act(BlackjackTableSlot slot)
        {
            if (!Available(slot))
            {
                throw new ActionUnavailableException($"Command '{Kind}' is unavailable");
            }

            bool done = Execute(slot);
            Acted?.Invoke(this, new BlackjackActionEventArgs(Kind, done));
            return done;
        }

        public abstract bool Execute(BlackjackTableSlot slot);
        public abstract bool Available(BlackjackTableSlot slot);

        public override string ToString()
        { 
            return Kind.ToString();
        }
    }

    public class BlackjackActionEventArgs : EventArgs
    {
        public BlackjackActionEnum Kind { get; }
        public bool Done{ get; }

        public BlackjackActionEventArgs(BlackjackActionEnum kind, bool done)
        {
            this.Kind = kind;
            this.Done = done;
        }
    }

    public class HitAction : BlackjackAction
    {
        private readonly Shoe shoe;

        public override BlackjackActionEnum Kind => BlackjackActionEnum.Hit;

        public event EventHandler<BlackjackHitActionEventArgs> Hit;

        public HitAction(Shoe shoe)
        {
            this.shoe = shoe;
        }

        public override bool Available(BlackjackTableSlot _)
        {
            return true;
        }

        public override bool Execute(BlackjackTableSlot slot)
        {
            Card newCard = shoe.Deal(1)[0];
            slot.Hand.Add(newCard);
            Hit?.Invoke(this, new BlackjackHitActionEventArgs(newCard));
            return false;
        }
    }

    public class BlackjackHitActionEventArgs : EventArgs
    {
        public Card CardReceived { get; }
        public BlackjackHitActionEventArgs(Card cardReceived)
        {
            this.CardReceived = cardReceived;
        }
    }

    public class StandAction : BlackjackAction
    {
        public override BlackjackActionEnum Kind => BlackjackActionEnum.Stand;

        public override bool Available(BlackjackTableSlot _)
        {
            return true;
        }

        public override bool Execute(BlackjackTableSlot _)
        {
            return true;
        }
    }

    public class DoubleAction: BlackjackAction
    {
        private readonly HitAction hit;
        private readonly StandAction stand;

        public override BlackjackActionEnum Kind => BlackjackActionEnum.Double;

        public DoubleAction(HitAction hit, StandAction stand)
        {
            this.hit = hit;
            this.stand = stand;
        }

        public override bool Available(BlackjackTableSlot slot)
        {
            if (!BlackjackConfig.DoubleOffered)
            {
                return false;
            }
            if (slot.Hand.Count > 2)
            {
                return false;
            }
            if (!BlackjackConfig.DoubleAfterSplit && slot.Hand.IsSplit)
            {
                return false;
            }
            if (0 == (BlackjackConfig.DoubleTotalsAllowed & (1 << slot.Hand.Value)))
            {
                return false;
            }
            if (slot.Player.Bank.Balance < BlackjackConfig.DoubleCost * slot.Pot.Balance)
            {
                return false;
            }

            return true;
        }

        public override bool Execute(BlackjackTableSlot slot)
        {
            slot.Player.Bank.Transfer(slot.Pot, BlackjackConfig.DoubleCost * slot.Pot.Balance);
            hit.Execute(slot);
            stand.Execute(slot);
            return true;
        }
    }

    public class SplitAction : BlackjackAction
    {
        private readonly HitAction hit;

        public override BlackjackActionEnum Kind => BlackjackActionEnum.Split;

        public SplitAction(HitAction hit)
        {
            this.hit = hit;
        }

        public override bool Available(BlackjackTableSlot slot)
        {
            if (slot.Hand.Count > 2)
            {
                return false;
            }
            if (BlackjackConfig.SplitByValue)
            {
                if (BlackjackHand.CardValue(slot.Hand[0]) != BlackjackHand.CardValue(slot.Hand[1]))
                {
                    return false;
                }
            }
            else if (!slot.Hand.IsPair)
            {
                return false;
            }
            if (slot.NumSplits >= BlackjackConfig.NumSplitsAllowed)
            {
                return false;
            }
            if (slot.Player.Bank.Balance < BlackjackConfig.SplitCost * slot.Pot.Balance)
            {
                return false;
            }

            return true;
        }

        public override bool Execute(BlackjackTableSlot slot)
        {
            slot.Split();
            hit.Execute(slot);
            slot.Index++;
            hit.Execute(slot);
            slot.Index--;
            return false;
        }
    }

    public class LateSurrenderAction : BlackjackAction
    {
        public override BlackjackActionEnum Kind => BlackjackActionEnum.LateSurrender;

        public override bool Available(BlackjackTableSlot slot)
        {
            return BlackjackConfig.LateSurrenderOffered && slot.Hand.Count == 2 && !slot.Hand.IsSplit;
        }

        public override bool Execute(BlackjackTableSlot slot)
        {
            slot.Surrendered = true;
            return true;
        }
    }
}
