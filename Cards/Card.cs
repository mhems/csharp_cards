using System;
using System.Collections.Generic;

namespace Cards
{
    public readonly struct Card
    {
        public enum RankEnum
        {
            Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten,
            Jack, Queen, King, Ace         
        }

        public enum SuitEnum
        {
            Clubs, Spades, Hearts, Diamonds
        }

        #region Properties
        public bool IsAce => Rank == Card.RankEnum.Ace;
        public bool IsFace => !IsAce && ((int)Rank >= (int)Card.RankEnum.Jack);
        public bool IsNumeric => (int)Rank <= (int)Card.RankEnum.Ten;
        public readonly RankEnum Rank { get; }
        public readonly SuitEnum Suit { get; }
        #endregion
        
        internal Card(RankEnum rank, SuitEnum suit)
        {
            this.Rank = rank;
            this.Suit = suit;
        }

        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }

        public override int GetHashCode()
        {
            return ComputeHashCode((int)Rank, (int)Suit);
        }

        public override bool Equals(object obj)
        {
            if (obj is Card otherCard)
            {
                return GetHashCode() == otherCard.GetHashCode();
            }
            return false;
        }

        internal static int ComputeHashCode(int rank, int suit)
        {
            return (100 * suit) + rank;
        }
    }

    public static class CardFactory
    {
        private static readonly Dictionary<int, Card> cardDict = new ();
        public static int Count => cardDict.Count;

        public static Card GetCard(Card.RankEnum rank, Card.SuitEnum suit)
        {
            int guid = Card.ComputeHashCode((int)rank, (int)suit);
            if (cardDict.ContainsKey(guid))
            {
                return cardDict[guid];
            }
            else
            {
                Card c = new (rank, suit);
                cardDict.Add(guid, c);
                return c;
            }
        }
    }
}
