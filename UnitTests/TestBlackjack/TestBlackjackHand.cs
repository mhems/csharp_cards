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
