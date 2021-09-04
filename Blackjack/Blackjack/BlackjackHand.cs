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
        public int NumAces => this.Where(c => c.IsAce).Count();
        public bool HasAce => NumAces > 0;
        public bool IsSoft => HasAce && CheckSoft();
        public bool IsPair => (Count == 2) && (this[0].Rank == this[1].Rank);
        public bool IsBust => Value > 21;
        public bool IsBlackjack => Value == 21;
        public bool IsNaturalBlackjack => IsBlackjack && (Count == 2) && !Split;
        public bool Split { get; private set; } = false;
        #endregion

        public BlackjackHand()
        {

        }

        public BlackjackHand(Card first, Card second)
        {
            Add(first);
            Add(second);
        }

        public (BlackjackHand, BlackjackHand) SplitHand(Card nextCardLeftHand, Card nextCardRightHand)
        {
            if (!IsPair)
            {
                throw new ActionUnavailableException("Cannot split a non-paired BlackjackHand");
            }

            BlackjackHand leftHand = new()
            {
                Split = true
            };
            leftHand.Add(this[0]);
            leftHand.Add(nextCardLeftHand);

            BlackjackHand rightHand = new()
            {
                Split = true
            };
            rightHand.Add(this[1]);
            rightHand.Add(nextCardRightHand);

            return (leftHand, rightHand);
        }

        private int ComputeValue()
        {
            int value = this.Where(c => !c.IsAce).Select(c => CardValue(c)).Sum();
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

        private static int CardValue(Card card)
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
            int value = this.Where(c => !c.IsAce).Select(c => CardValue(c)).Sum();
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
