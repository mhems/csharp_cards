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

        public Card DiscardLast()
        {
            Card c = this[Count - 1];
            RemoveAt(Count - 1);
            return c;
        }
    }
}
