using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public class Shoe
    {
        private readonly Random rng = new Random();
        private readonly List<Card> cards = new List<Card>();

        public int NumDecks { get; private set; }
        public int CutIndex { get; set; }
        public int Index { get; private set; }
        public int Count => cards.Count;
        public bool IsExhausted => Index >= CutIndex;

        public event EventHandler Shuffling;
        public event EventHandler Exhausted;
        public event EventHandler Dealt;
        public event EventHandler Burnt;

        public Shoe(int numDecks = 1)
        {
            if (numDecks < 1)
            {
                throw new ArgumentException("NumDecks must be greater than zero");
            }

            NumDecks = numDecks;
            for (int n = 0; n < NumDecks; n++)
            {
                for (int suit = 0; suit < 4; suit++)
                {
                    for (int rank = 0; rank < 13; rank++)
                    {
                        cards.Add(new Card(rank, suit));
                    }
                }
            }
            Index = 0;
            CutIndex = 0;
            CheckForExhaustion();
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
            for (int i = n; i >= 0; i--)
            {
                CheckForExhaustion();
                Burnt?.Invoke(this, new EventArgs());
                Index++;
            }
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
            for (int n = cards.Count - 1; n > 0; n--)
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
    }
}
