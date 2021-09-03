using NUnit.Framework;
using Cards;
using System;
using System.Diagnostics;

namespace CardsTests
{
    public class CardTests
    {
        private Card two, ten, jack, queen, king, ace;

        [SetUp]
        public void Setup()
        {
            two = new Card('2', 'h');
            ten = new Card('t', 's');
            jack = new Card('j', 'c');
            queen = new Card('Q', 'd');
            king = new Card('K', 'd');
            ace = new Card('a', 'd');
        }

        [Test]
        public void TestCtor()
        {
            Assert.AreEqual('2', two.Rank);
            Assert.AreEqual('H', two.Suit);

            Assert.AreEqual('T', ten.Rank);
            Assert.AreEqual('S', ten.Suit);

            Assert.AreEqual('J', jack.Rank);
            Assert.AreEqual('C', jack.Suit);

            Assert.AreEqual('Q', queen.Rank);
            Assert.AreEqual('D', queen.Suit);

            Assert.AreEqual('K', king.Rank);
            Assert.AreEqual('D', king.Suit);

            Assert.AreEqual('A', ace.Rank);
            Assert.AreEqual('D', ace.Suit);

            Assert.Throws<ArgumentException>(() => new Card('1', 'h'));
            Assert.Throws<ArgumentException>(() => new Card('2', 'f'));
        }

        [Test]
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

        [Test]
        public void TestStrProps()
        {
            Assert.AreEqual("2", two.RankString);
            Assert.AreEqual("10", ten.RankString);
            Assert.AreEqual("Jack", jack.RankString);
            Assert.AreEqual("Queen", queen.RankString);
            Assert.AreEqual("King", king.RankString);
            Assert.AreEqual("Ace", ace.RankString);

            Assert.AreEqual("Hearts", two.SuitString);
            Assert.AreEqual("Spades", ten.SuitString);
            Assert.AreEqual("Clubs", jack.SuitString);
            Assert.AreEqual("Diamonds", queen.SuitString);
        }
   
        [Test]
        public void TestToString()
        {
            Assert.AreEqual("10 of Spades", ten.ToString());
        }
    }
}