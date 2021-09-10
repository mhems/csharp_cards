using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;

namespace Blackjack
{
    public abstract class BlackjackDecisionPolicy
    {
        public event EventHandler<BlackjackDecisionEventArgs> Decided;

        public BlackjackActionEnum Decide(BlackjackHand hand, Card upCard, HashSet<BlackjackActionEnum> availableActions)
        {
            BlackjackActionEnum decision = DecideInner(hand, upCard, availableActions);
            Decided?.Invoke(this, new BlackjackDecisionEventArgs(hand, upCard, availableActions, decision));
            return decision;
        }
        protected abstract BlackjackActionEnum DecideInner(BlackjackHand hand, Card upCard, HashSet<BlackjackActionEnum> availableActions);
    }

    public class BlackjackDecisionEventArgs : EventArgs
    {
        public BlackjackHand Hand { get; }
        public Card UpCard { get; }
        public HashSet<BlackjackActionEnum> AvailableActions { get; }
        public BlackjackActionEnum Decision { get; }

        public BlackjackDecisionEventArgs(BlackjackHand hand, Card upCard, HashSet<BlackjackActionEnum> availableActions, BlackjackActionEnum decision)
        {
            Hand = hand;
            UpCard = upCard;
            AvailableActions = availableActions;
            Decision = decision;
        }
    }

    public class DealerDecisionPolicy : BlackjackDecisionPolicy
    {
        protected override BlackjackActionEnum DecideInner(BlackjackHand hand, Card card, HashSet<BlackjackActionEnum> _)
        {
            int value = hand.Value;
            if (value < 17)
            {
                return BlackjackActionEnum.Hit;
            }
            else if (BlackjackConfig.Config.DealerHitsSoft17 && hand.IsSoft && value == 17)
            {
                return BlackjackActionEnum.Hit;
            }
            return BlackjackActionEnum.Stand;
        }
    }

    public abstract class BlackjackBettingPolicy
    {
        public abstract int Bet();
    }

    public class MinimumBettingPolicy : BlackjackBettingPolicy
    {
        public override int Bet()
        {
            return BlackjackConfig.Config.MinimumBet;
        }
    }

    public abstract class BlackjackInsurancePolicy
    {
        public event EventHandler<BlackjackInsuranceEventArgs> Insured;

        public bool Insure(BlackjackHand hand, Card upCard)
        { 
            bool insured = InsureInner(hand, upCard);
            Insured?.Invoke(this, new BlackjackInsuranceEventArgs(hand, upCard, insured));
            return insured;
        }

        protected abstract bool InsureInner(BlackjackHand hand, Card upCard);
    }

    public class BlackjackInsuranceEventArgs : EventArgs
    {
        public BlackjackHand Hand { get; }
        public Card UpCard { get; }
        public bool Insured { get; }
        public BlackjackInsuranceEventArgs(BlackjackHand hand, Card upCard, bool insured)
        {
            Hand = hand;
            UpCard = upCard;
            Insured = insured;
        }
    }

    public class DeclineInsurancePolicy : BlackjackInsurancePolicy
    {
        protected override bool InsureInner(BlackjackHand hand, Card _)
        {
            return false;
        }
    }

    public abstract class BlackjackEarlySurrenderPolicy
    {
        public event EventHandler<BlackjackEarlySurrenderEventArgs> Surrendered;

        public bool Surrender(BlackjackHand hand, Card upCard)
        {
            bool surrendered = SurrenderInner(hand, upCard);
            Surrendered?.Invoke(this, new BlackjackEarlySurrenderEventArgs(hand, upCard, surrendered));
            return surrendered;
        }

        protected abstract bool SurrenderInner(BlackjackHand hand, Card upCard);
    }

    public class BlackjackEarlySurrenderEventArgs : EventArgs
    {
        public BlackjackHand Hand { get; }
        public Card UpCard { get; }
        public bool Surrendered { get; }
        public BlackjackEarlySurrenderEventArgs(BlackjackHand hand, Card upCard, bool surrendered)
        {
            Hand = hand;
            UpCard = upCard;
            Surrendered = surrendered;
        }
    }

    public class DeclineEarlySurrenderPolicy : BlackjackEarlySurrenderPolicy
    {
        protected override bool SurrenderInner(BlackjackHand hand, Card _)
        {
            return false;
        }
    }
}
