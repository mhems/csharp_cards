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

        public IBlackjackConfig Config { get; set; }

        public override BlackjackActionEnum Kind => BlackjackActionEnum.Double;

        public DoubleAction(IBlackjackConfig config, HitAction hit, StandAction stand)
        {
            this.Config = config;
            this.hit = hit;
            this.stand = stand;
        }

        public override bool Available(BlackjackTableSlot slot)
        {
            if (!Config.DoubleOffered)
            {
                return false;
            }
            if (slot.Hand.Count > 2)
            {
                return false;
            }
            if (!Config.DoubleAfterSplit && slot.Hand.IsSplit)
            {
                return false;
            }
            if (0 == (Config.DoubleTotalsAllowed & (1 << slot.Hand.Value)))
            {
                return false;
            }
            if (slot.Player.Bank.Balance < Config.DoubleCost * slot.Pot.Balance)
            {
                return false;
            }

            return true;
        }

        public override bool Execute(BlackjackTableSlot slot)
        {
            slot.Player.Bank.Transfer(slot.Pot, Config.DoubleCost * slot.Pot.Balance);
            hit.Execute(slot);
            stand.Execute(slot);
            return true;
        }
    }

    public class SplitAction : BlackjackAction
    {
        private readonly HitAction hit;

        public IBlackjackConfig Config { get; set; }

        public override BlackjackActionEnum Kind => BlackjackActionEnum.Split;

        public SplitAction(IBlackjackConfig config, HitAction hit)
        {
            this.hit = hit;
            this.Config = config;
        }

        public override bool Available(BlackjackTableSlot slot)
        {
            if (!Config.SplitOffered)
            {
                return false;
            }
            if (slot.Hand.Count > 2)
            {
                return false;
            }
            if (Config.SplitByValue)
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
            if (slot.NumSplits >= Config.NumSplitsAllowed)
            {
                return false;
            }
            else if (!Config.ReSplitAces && slot.Hand[0].IsAce && slot.Hand.IsSplit)
            {
                return false;
            }
            if (slot.Player.Bank.Balance < Config.SplitCost * slot.Pot.Balance)
            {
                return false;
            }

            return true;
        }

        public override bool Execute(BlackjackTableSlot slot)
        {
            bool wasAcePair = slot.Hand.IsPair && slot.Hand[0].IsAce;

            slot.Split();
            hit.Execute(slot);
            slot.Index++;
            hit.Execute(slot);
            slot.Index--;

            return wasAcePair && !Config.HitSplitAces;
        }
    }

    public class LateSurrenderAction : BlackjackAction
    {
        public IBlackjackConfig Config { get; }
        public override BlackjackActionEnum Kind => BlackjackActionEnum.LateSurrender;

        public LateSurrenderAction(IBlackjackConfig config)
        {
            Config = config;
        }

        public override bool Available(BlackjackTableSlot slot)
        {
            return Config.LateSurrenderOffered && slot.Hand.Count == 2 && !slot.Hand.IsSplit;
        }

        public override bool Execute(BlackjackTableSlot slot)
        {
            slot.Surrendered = true;
            return true;
        }
    }
}
