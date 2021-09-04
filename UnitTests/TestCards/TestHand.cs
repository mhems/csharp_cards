using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;

namespace TestCards
{
    [TestClass]
    public class TestHand
    {
        [TestMethod]
        public void TestDiscardLast()
        {
            Hand hand = new ();
            Assert.AreEqual(0, hand.Count);
            Assert.IsTrue(hand.Empty);
            Assert.ThrowsException<ArgumentException>(() => hand.DiscardLast());

            Card firstCard = CardFactory.GetCard(Card.RankEnum.Two, Card.SuitEnum.Diamonds);
            hand.Add(firstCard);
            Assert.AreEqual(1, hand.Count);
            Assert.IsFalse(hand.Empty);
            hand.DiscardLast();
            Assert.AreEqual(0, hand.Count);
            Assert.IsTrue(hand.Empty);

            hand.Add(firstCard);
            hand.Add(CardFactory.GetCard(Card.RankEnum.Ace, Card.SuitEnum.Hearts));
            Assert.AreEqual(2, hand.Count);
            Assert.IsFalse(hand.Empty);

            hand.DiscardLast();
            Assert.AreEqual(1, hand.Count);
            Assert.IsFalse(hand.Empty);
            Assert.AreEqual(firstCard, hand[0]);
        }
    }
}
