using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public class Shoe
    {
        private readonly Random rng = new ();
        private readonly List<Card> cards = new ();

        public int NumDecks { get; private set; }
        public int CutIndex { get; set; }
        public int Index { get; private set; }
        public int Count => cards.Count;
        public bool IsExhausted => Index >= CutIndex;

        public event EventHandler Shuffling;
        public event EventHandler Exhausted;
        public event EventHandler<DealtEventArgs> Dealt;
        public event EventHandler<BurnEventArgs> Burnt;

        public Shoe(int numDecks = 1)
        {
            if (numDecks < 1)
            {
                throw new ArgumentException("numDecks must be greater than zero");
            }

            NumDecks = numDecks;
            for (int n = 0; n < NumDecks; n++)
            {
                foreach (int suit in Enum.GetValues(typeof(Card.SuitEnum)))
                {
                    foreach (int rank in Enum.GetValues(typeof(Card.RankEnum)))
                    {
                        Card c = CardFactory.GetCard((Card.RankEnum)rank, (Card.SuitEnum)suit);
                        cards.Add(c);
                    }
                }
            }
            Index = 0;
            CutIndex = cards.Count;
            CheckForExhaustion();
            Shuffle();
        }

        public Card[] Deal(int n)
        {
            Card[] ret = new Card[n];
            for (int i = 0; i < n; i++)
            {
                CheckForExhaustion();
                ret[i] = cards[Index];
                Index++;
            }
            Dealt?.Invoke(this, new DealtEventArgs(ret));
            return ret;
        }

        public void Burn(int n = 1)
        {
            for (int i = 0; i < n; i++)
            {
                CheckForExhaustion();
                Index++;
            }
            Burnt?.Invoke(this, new BurnEventArgs(n));
        }

        private void CheckForExhaustion()
        {
            if (IsExhausted)
            {
                Exhausted?.Invoke(this, new EventArgs());
                Shuffle();
            }
        }

        public void Shuffle()
        {
            int k;
            Card temp;
            Shuffling?.Invoke(this, new EventArgs());
            for (int n = cards.Count - 1; n >= 0; n--)
            {
                k = rng.Next(n + 1);
                temp = cards[k];
                cards[k] = cards[n];
                cards[n] = temp;
            }
            Index = 0;
        }

        public class DealtEventArgs : EventArgs
        {
            public Card[] DealtCards { get; private set; }
            public DealtEventArgs(Card[] cards)
            {
                DealtCards = cards;
            }
        }

        public class BurnEventArgs : EventArgs
        {
            public int NumBurnt { get; private set; }
            public BurnEventArgs(int n)
            {
                NumBurnt = n;
            }
        }
    }
}
