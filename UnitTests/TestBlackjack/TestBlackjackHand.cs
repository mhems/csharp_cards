using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestBlackjack;
using Cards;
using Blackjack;

namespace TestBlackjack
{
    [TestClass]
    public class TestBlackjackHand
    {
        private Card two, ten, jack, queen, king, ace;
        private bool splitHandlerCalled = false;
        private BlackjackHand left, right;

        [TestInitialize]
        public void Setup()
        {
            two = CardFactory.GetCard(Card.RankEnum.Two, Card.SuitEnum.Hearts);
            ten = CardFactory.GetCard(Card.RankEnum.Ten, Card.SuitEnum.Clubs);
            jack = CardFactory.GetCard(Card.RankEnum.Jack, Card.SuitEnum.Diamonds);
            queen = CardFactory.GetCard(Card.RankEnum.Queen, Card.SuitEnum.Spades);
            king = CardFactory.GetCard(Card.RankEnum.King, Card.SuitEnum.Clubs);
            ace = CardFactory.GetCard(Card.RankEnum.Ace, Card.SuitEnum.Hearts);
        }

        [TestCleanup]
        public void Cleanup()
        {
            splitHandlerCalled = false;
            left = null;
            right = null;
        }

        [TestMethod]
        public void TestBasics()
        {
            BlackjackHand hand = new();
            Assert.AreEqual(0, hand.Value);
            Assert.IsFalse(hand.HasAce);
            Assert.AreEqual(0, hand.NumAces);
            Assert.IsFalse(hand.IsSoft);
            Assert.IsFalse(hand.IsBust);
            Assert.IsFalse(hand.IsPair);
            Assert.IsFalse(hand.IsBlackjack);
            Assert.IsFalse(hand.IsNaturalBlackjack);
            Assert.IsFalse(hand.IsSplit);

            hand.Add(two);
            Assert.AreEqual(2, hand.Value);
            Assert.IsFalse(hand.HasAce);
            Assert.AreEqual(0, hand.NumAces);
            Assert.IsFalse(hand.IsSoft);
            Assert.IsFalse(hand.IsBust);
            Assert.IsFalse(hand.IsPair);
            Assert.IsFalse(hand.IsBlackjack);
            Assert.IsFalse(hand.IsNaturalBlackjack);
            Assert.IsFalse(hand.IsSplit);

            hand.Add(ten);
            Assert.AreEqual(12, hand.Value);
            Assert.IsFalse(hand.HasAce);
            Assert.AreEqual(0, hand.NumAces);
            Assert.IsFalse(hand.IsSoft);
            Assert.IsFalse(hand.IsBust);
            Assert.IsFalse(hand.IsPair);
            Assert.IsFalse(hand.IsBlackjack);
            Assert.IsFalse(hand.IsNaturalBlackjack);
            Assert.IsFalse(hand.IsSplit);

            hand.Add(ten);
            Assert.AreEqual(22, hand.Value);
            Assert.IsFalse(hand.HasAce);
            Assert.AreEqual(0, hand.NumAces);
            Assert.IsFalse(hand.IsSoft);
            Assert.IsTrue(hand.IsBust);
            Assert.IsFalse(hand.IsPair);
            Assert.IsFalse(hand.IsBlackjack);
            Assert.IsFalse(hand.IsNaturalBlackjack);
            Assert.IsFalse(hand.IsSplit);

            hand = new BlackjackHand(two, jack);
            Assert.AreEqual(two, hand[0]);
            Assert.AreEqual(jack, hand[1]);
        }

        [TestMethod]
        public void TestBlackjack()
        {
            BlackjackHand hand = new(jack, ace);

            Assert.AreEqual(21, hand.Value);
            Assert.IsTrue(hand.HasAce);
            Assert.AreEqual(1, hand.NumAces);
            Assert.IsTrue(hand.IsSoft);
            Assert.IsFalse(hand.IsBust);
            Assert.IsFalse(hand.IsPair);
            Assert.IsTrue(hand.IsBlackjack);
            Assert.IsTrue(hand.IsNaturalBlackjack);
            Assert.IsFalse(hand.IsSplit);

            hand.Add(queen);
            Assert.AreEqual(21, hand.Value);
            Assert.IsTrue(hand.HasAce);
            Assert.AreEqual(1, hand.NumAces);
            Assert.IsFalse(hand.IsSoft);
            Assert.IsFalse(hand.IsBust);
            Assert.IsFalse(hand.IsPair);
            Assert.IsTrue(hand.IsBlackjack);
            Assert.IsFalse(hand.IsNaturalBlackjack);
            Assert.IsFalse(hand.IsSplit);
        }

        private void SplitHandler(object obj, BlackjackHand.SplitEventArgs args)
        {
            splitHandlerCalled = true;
            left = args.LeftHand;
            right = args.RightHand;
        }

        [TestMethod]
        public void TestPairs()
        {
            BlackjackHand hand = new(king, king);
            Assert.AreEqual(20, hand.Value);
            Assert.IsFalse(hand.HasAce);
            Assert.AreEqual(0, hand.NumAces);
            Assert.IsFalse(hand.IsSoft);
            Assert.IsFalse(hand.IsBust);
            Assert.IsTrue(hand.IsPair);
            Assert.IsFalse(hand.IsBlackjack);
            Assert.IsFalse(hand.IsNaturalBlackjack);
            Assert.IsFalse(hand.IsSplit);

            BlackjackHand a, b;
            hand.Split += SplitHandler;
            (a, b) = hand.SplitHand(ace, queen);

            Assert.IsTrue(splitHandlerCalled);
            Assert.AreEqual(a, left);
            Assert.AreEqual(b, right);

            Assert.AreEqual(king, a[0]);
            Assert.AreEqual(ace, a[1]);
            Assert.AreEqual(21, a.Value);
            Assert.IsTrue(a.IsSplit);
            Assert.AreEqual(1, a.NumAces);
            Assert.IsTrue(a.HasAce);
            Assert.IsTrue(a.IsSoft);
            Assert.IsFalse(a.IsPair);
            Assert.IsFalse(a.IsBust);
            Assert.IsTrue(a.IsBlackjack);
            Assert.IsFalse(a.IsNaturalBlackjack);

            Assert.AreEqual(king, b[0]);
            Assert.AreEqual(queen, b[1]);
            Assert.AreEqual(20, b.Value);
            Assert.IsTrue(b.IsSplit);
            Assert.AreEqual(0, b.NumAces);
            Assert.IsFalse(b.HasAce);
            Assert.IsFalse(b.IsSoft);
            Assert.IsFalse(b.IsPair);
            Assert.IsFalse(b.IsBust);
            Assert.IsFalse(b.IsBlackjack);
            Assert.IsFalse(b.IsNaturalBlackjack);
        }

        [TestMethod]
        public void TestSplitErrors()
        {
            BlackjackHand hand = new(two, ten);
            Assert.ThrowsException<ActionUnavailableException>(() => hand.SplitHand(jack, ace));

            hand = new(two, two);
            hand.Add(two);
            Assert.ThrowsException<ActionUnavailableException>(() => hand.SplitHand(jack, ace));
        }

        [TestMethod]
        public void TestMultiAces()
        {
            Card nine = CardFactory.GetCard(Card.RankEnum.Nine, Card.SuitEnum.Clubs);
            BlackjackHand hand = new(ace, nine);

            Assert.AreEqual(20, hand.Value);
            Assert.IsTrue(hand.HasAce);
            Assert.AreEqual(1, hand.NumAces);
            Assert.IsTrue(hand.IsSoft);
            Assert.IsFalse(hand.IsBust);
            Assert.IsFalse(hand.IsPair);
            Assert.IsFalse(hand.IsBlackjack);
            Assert.IsFalse(hand.IsNaturalBlackjack);
            Assert.IsFalse(hand.IsSplit);

            hand.Add(ace);
            Assert.AreEqual(21, hand.Value);
            Assert.IsTrue(hand.HasAce);
            Assert.AreEqual(2, hand.NumAces);
            Assert.IsTrue(hand.IsSoft);
            Assert.IsFalse(hand.IsBust);
            Assert.IsFalse(hand.IsPair);
            Assert.IsTrue(hand.IsBlackjack);
            Assert.IsFalse(hand.IsNaturalBlackjack);
            Assert.IsFalse(hand.IsSplit);

            hand.Add(ace);
            Assert.AreEqual(12, hand.Value);
            Assert.IsTrue(hand.HasAce);
            Assert.AreEqual(3, hand.NumAces);
            Assert.IsFalse(hand.IsSoft);
            Assert.IsFalse(hand.IsBust);
            Assert.IsFalse(hand.IsPair);
            Assert.IsFalse(hand.IsBlackjack);
            Assert.IsFalse(hand.IsNaturalBlackjack);
            Assert.IsFalse(hand.IsSplit);

            hand.Add(nine);
            Assert.AreEqual(21, hand.Value);
            Assert.IsTrue(hand.HasAce);
            Assert.AreEqual(3, hand.NumAces);
            Assert.IsFalse(hand.IsSoft);
            Assert.IsFalse(hand.IsBust);
            Assert.IsFalse(hand.IsPair);
            Assert.IsTrue(hand.IsBlackjack);
            Assert.IsFalse(hand.IsNaturalBlackjack);
            Assert.IsFalse(hand.IsSplit);
        }
    }
}
