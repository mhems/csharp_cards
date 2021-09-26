using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;

namespace Blackjack
{
    public class BlackjackHand : Hand
    {
        #region Properties
        public override int Value => ComputeValue();
        public int NumAces => cards.Where(c => c.IsAce).Count();
        public bool HasAce => NumAces > 0;
        public bool IsSoft => HasAce && CheckSoft();
        public bool IsPair => (Count == 2) && (this[0].Rank == this[1].Rank);
        public bool IsBust => Value > 21;
        public bool IsBlackjack => Value == 21;
        public bool IsNaturalBlackjack => (Count == 2) && !IsSplit && IsBlackjack;
        public bool IsSplit { get; internal set; } = false;
        #endregion

        public BlackjackHand() { }

        public BlackjackHand(Card first, Card second)
        {
            Add(first);
            Add(second);
        }

        private int ComputeValue()
        {
            int value = cards.Where(c => !c.IsAce).Select(c => CardValue(c)).Sum();
            if (NumAces > 0)
            {
                value += (NumAces - 1) * 1;
                if (value + 11 <= 21)
                {
                    value += 11;
                }
                else
                {
                    value += 1;
                }
            }
            return value;
        }

        internal static int CardValue(Card card)
        {
            if (card.IsAce)
            {
                return 11;
            }
            if (card.IsFace)
            {
                return 10;
            }
            return 2 + (int)card.Rank;
        }

        private bool CheckSoft()
        {
            int value = cards.Where(c => !c.IsAce).Select(c => CardValue(c)).Sum();
            if (NumAces > 0)
            {
                value += (NumAces - 1) * 1;
                if (value + 11 <= 21)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
