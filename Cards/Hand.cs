using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public class Hand : List<Card>
    {
        public virtual int Value => throw new NotImplementedException("Hand does not have a Value");

        public bool Empty => Count == 0;

        public Card DiscardLast()
        { 
            if (Empty)
            {
                throw new ArgumentException("Cannot discard from an empty Hand");
            }
            Card c = this[Count - 1];
            RemoveAt(Count - 1);
            return c;
        }
    }
}
