using Cards;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CardsTests
{
    [TestClass]
    public class TestCard
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
        public void TestCardFactory()
        {
            foreach (int suit in Enum.GetValues(typeof(Card.SuitEnum)))
            {
                foreach (int rank in Enum.GetValues(typeof(Card.RankEnum)))
                {
                    _ = CardFactory.GetCard((Card.RankEnum)rank, (Card.SuitEnum)suit);
                }
            }
            Assert.AreEqual(52, CardFactory.Count);
        }

        [TestMethod]
        public void TestRankAndSuit()
        {
            Assert.AreEqual(Card.RankEnum.Two, two.Rank);
            Assert.AreEqual(Card.SuitEnum.Hearts, two.Suit);

            Assert.AreEqual(Card.RankEnum.Ten, ten.Rank);
            Assert.AreEqual(Card.SuitEnum.Clubs, ten.Suit);

            Assert.AreEqual(Card.RankEnum.Jack, jack.Rank);
            Assert.AreEqual(Card.SuitEnum.Diamonds, jack.Suit);

            Assert.AreEqual(Card.RankEnum.Queen, queen.Rank);
            Assert.AreEqual(Card.SuitEnum.Spades, queen.Suit);

            Assert.AreEqual(Card.RankEnum.King, king.Rank);
            Assert.AreEqual(Card.SuitEnum.Clubs, king.Suit);

            Assert.AreEqual(Card.RankEnum.Ace, ace.Rank);
            Assert.AreEqual(Card.SuitEnum.Hearts, ace.Suit);
        }

        [TestMethod]
        public void TestBasicProps()
        {
            Assert.IsTrue(two.IsNumeric);
            Assert.IsFalse(two.IsFace);
            Assert.IsFalse(two.IsAce);

            Assert.IsTrue(ten.IsNumeric);
            Assert.IsFalse(ten.IsFace);
            Assert.IsFalse(ten.IsAce);

            Assert.IsFalse(jack.IsNumeric);
            Assert.IsTrue(jack.IsFace);
            Assert.IsFalse(jack.IsAce);

            Assert.IsFalse(king.IsNumeric);
            Assert.IsTrue(king.IsFace);
            Assert.IsFalse(king.IsAce);

            Assert.IsFalse(ace.IsNumeric);
            Assert.IsFalse(ace.IsFace);
            Assert.IsTrue(ace.IsAce);
        }

        [TestMethod]
        public void TestToString()
        {
            Assert.AreEqual("Two of Hearts", two.ToString());
            Assert.AreEqual("Ten of Clubs", ten.ToString());
            Assert.AreEqual("Jack of Diamonds", jack.ToString());
            Assert.AreEqual("Queen of Spades", queen.ToString());
            Assert.AreEqual("King of Clubs", king.ToString());
            Assert.AreEqual("Ace of Hearts", ace.ToString());
        }

        [TestMethod]
        public void TestHashCode()
        {
            HashSet<int> hashes = new();
            foreach (int suit in Enum.GetValues(typeof(Card.SuitEnum)))
            {
                foreach (int rank in Enum.GetValues(typeof(Card.RankEnum)))
                {
                    Card c = CardFactory.GetCard((Card.RankEnum)rank, (Card.SuitEnum)suit);
                    hashes.Add(c.GetHashCode());
                }
            }
            Assert.AreEqual(52, hashes.Count);
        }
    }
}