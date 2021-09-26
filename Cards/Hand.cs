using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public class Hand
    {
        protected List<Card> cards = new();

        public virtual int Value => throw new NotImplementedException("Hand does not have a Value");
        public int Count => cards.Count;
        public bool Empty => Count == 0;
        public Card this[int index]
        {
            get => cards[index];
            set => cards[index] = value;
        }

        public event EventHandler<EventArgs> Cleared;
        public event EventHandler<CardAddedEventArgs> Added;
        public event EventHandler<CardRemovedEventArgs> Removed;


        public Card DiscardLast()
        { 
            if (Empty)
            {
                throw new ArgumentException("Cannot discard from an empty Hand");
            }
            Card c = cards[Count - 1];
            cards.RemoveAt(Count - 1);
            return c;
        }

        public void Add(Card card, bool visible=true)
        {
            cards.Add(card);
            Added?.Invoke(this, new CardAddedEventArgs(card, visible));
        }

        public void RemoveAt(int index)
        {
            Card removal = cards[index];
            cards.RemoveAt(index);
            Removed?.Invoke(this, new CardRemovedEventArgs(removal, index));
        }

        public void Clear()
        {
            cards.Clear();
            Cleared?.Invoke(this, new EventArgs());
        }

        public override string ToString()
        {
            return $"[{string.Join(", ", cards)}]";
        }
    }

    public class CardAddedEventArgs : EventArgs
    {
        public Card AddedCard { get; set; }
        public bool Visible { get; set; }
        public CardAddedEventArgs(Card addedCard, bool visible=true)
        {
            AddedCard = addedCard;
            Visible = visible;
        }
    }

    public class CardRemovedEventArgs : EventArgs
    {
        public Card RemovedCard { get; set; }
        public int RemovalIndex { get; set; }
        public CardRemovedEventArgs(Card removedCard, int index)
        {
            RemovedCard = removedCard;
            RemovalIndex = index;
        }
    }
}
