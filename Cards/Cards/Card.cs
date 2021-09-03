using System;

namespace Cards
{
    public readonly struct Card
    {
        #region Constants
        public const string RANKS = "23456789TJQKA";
        public const string SUITS = "CDHS";
        private readonly string[] rank_strs;
        private readonly string[] suit_strs;
        #endregion

        #region Fields
        private readonly int rank_index;
        private readonly int suit_index;
        #endregion

        #region Properties
        public bool IsAce => rank_index == 12;
        public bool IsFace => !IsAce && (rank_index > 8);
        public bool IsNumeric => rank_index <= 8;
        public char Rank => RANKS[rank_index];
        public char Suit => SUITS[suit_index];
        public string RankString
        {
            get
            {
                if (IsNumeric)
                {
                    if (rank_index == 8)
                    {
                        return "10";
                    }
                    return RANKS[rank_index].ToString();
                }
                else 
                {
                    return rank_strs[rank_index - 9];
                }
            }
        }
        public string SuitString => suit_strs[suit_index];
        #endregion

        public Card(char rank, char suit)
        {
            rank_index = RANKS.IndexOf(Char.ToUpper(rank));
            if (rank_index < 0)
            {
                throw new ArgumentException($"rank must be one of {String.Join(",", RANKS.ToCharArray())}");
            }

            suit_index = SUITS.IndexOf(Char.ToUpper(suit));
            if (suit_index < 0)
            {
                throw new ArgumentException($"suit must be one of {String.Join(",", SUITS.ToCharArray())}");
            }

            rank_strs = new string[] { "Jack", "Queen", "King", "Ace" };
            suit_strs = new string[] { "Clubs", "Diamonds", "Hearts", "Spades" };
        }

        internal Card(int rank_index, int suit_index)
        {
            if (rank_index >= RANKS.Length)
            {
                throw new ArgumentException($"rank_index must be less than {RANKS.Length}");
            }
            this.rank_index = rank_index;

            if (suit_index >= SUITS.Length)
            {
                throw new ArgumentException($"suit_index must be less than {SUITS.Length}");
            }
            this.suit_index = suit_index;

            rank_strs = new string[] { "Jack", "Queen", "King", "Ace" };
            suit_strs = new string[] { "Clubs", "Diamonds", "Hearts", "Spades" };
        }

        public override string ToString()
        {
            return $"{RankString} of {SuitString}";
        }
    }
}
