using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;

namespace Blackjack
{
    public interface IBlackjackActionPolicy
    {
        public BlackjackActionEnum Decide(BlackjackHand hand, Card upCard, HashSet<BlackjackActionEnum> availableActions);
    }

    public class DealerActionPolicy : IBlackjackActionPolicy
    {
        public BlackjackActionEnum Decide(BlackjackHand hand, Card card, HashSet<BlackjackActionEnum> _)
        {
            int value = hand.Value;
            if (value < 17)
            {
                return BlackjackActionEnum.Hit;
            }
            else if (hand.IsSoft && value == 17)
            {
                return BlackjackActionEnum.Hit;
            }
            return BlackjackActionEnum.Stand;
        }
    }

    public interface IBlackjackBettingPolicy
    {
        public int Bet();
    }

    public class MinimumBettingPolicy : IBlackjackBettingPolicy
    {
        public int Amount { get; private set; }

        public MinimumBettingPolicy(int amount)
        {
            Amount = amount;
        }
        public int Bet()
        {
            return Amount;
        }
    }

    public interface IBlackjackInsurancePolicy
    {
        public bool Insure(BlackjackHand hand, Card upCard);
    }

    public class DeclineInsurancePolicy : IBlackjackInsurancePolicy
    {
        public bool Insure(BlackjackHand hand, Card _)
        {
            return false;
        }
    }
}
