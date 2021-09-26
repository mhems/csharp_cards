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
        private bool addHandled;
        private bool removeHandled;
        private bool clearHandled;
        private Card cardHandled;
        private int indexHandled;

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

        [TestMethod]
        public void TestAdd()
        {
            Hand hand = new();
            hand.Added += AddHandler;
            Assert.AreEqual(0, hand.Count);
            Card added = CardFactory.GetCard(Card.RankEnum.Five, Card.SuitEnum.Clubs);
            hand.Add(added);
            Assert.IsTrue(addHandled);
            Assert.AreEqual(added, cardHandled);
            Assert.AreEqual(1, hand.Count);
            Assert.AreEqual(added, hand[0]);

            added = CardFactory.GetCard(Card.RankEnum.Ten, Card.SuitEnum.Diamonds);
            hand.Add(added);
            Assert.AreEqual(2, hand.Count);
            Assert.AreEqual(added, hand[1]);
        }

        [TestMethod]
        public void TestRemove()
        {
            Hand hand = new();
            hand.Removed += RemoveHandler;
            Card first = CardFactory.GetCard(Card.RankEnum.Five, Card.SuitEnum.Clubs);
            hand.Add(first);
            Card second = CardFactory.GetCard(Card.RankEnum.Ten, Card.SuitEnum.Diamonds);
            hand.Add(second);

            hand.RemoveAt(0);
            Assert.AreEqual(1, hand.Count);
            Assert.IsTrue(removeHandled);
            Assert.AreEqual(first, cardHandled);
            Assert.AreEqual(second, hand[0]);
        }

        [TestMethod]
        public void TestClear()
        {
            Hand hand = new();
            hand.Cleared += ClearHandler;
            Card first = CardFactory.GetCard(Card.RankEnum.Five, Card.SuitEnum.Clubs);
            hand.Add(first);
            Card second = CardFactory.GetCard(Card.RankEnum.Ten, Card.SuitEnum.Diamonds);
            hand.Add(second);

            hand.Clear();

            Assert.AreEqual(0, hand.Count);
            Assert.IsTrue(hand.Empty);
            Assert.IsTrue(clearHandled);
        }

        [TestMethod]
        public void TestToString()
        {
            Hand hand = new();
            Assert.AreEqual("[]", hand.ToString());

            Card first = CardFactory.GetCard(Card.RankEnum.Five, Card.SuitEnum.Clubs);
            hand.Add(first);
            Card second = CardFactory.GetCard(Card.RankEnum.Ten, Card.SuitEnum.Diamonds);
            hand.Add(second);

            Assert.AreEqual("[Five of Clubs, Ten of Diamonds]", hand.ToString());
        }

        public void AddHandler(object _, CardAddedEventArgs args)
        {
            addHandled = true;
            cardHandled = args.AddedCard;
        }

        public void RemoveHandler(object _, CardRemovedEventArgs args)
        {
            removeHandled = true;
            cardHandled = args.RemovedCard;
            indexHandled = args.RemovalIndex;
        }

        public void ClearHandler(object _, EventArgs args)
        {
            clearHandled = true;
        }
    }
}
