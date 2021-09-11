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
    public class TestShoe
    {
        private bool gotDealtEvent = false;
        private bool gotShuffleEvent = false;
        private bool gotExhaustedEvent = false;
        private bool gotBurnEvent = false;
        private Card[] dealtCards = null;
        private int numBurnt = 0;

        [TestCleanup]
        public void Cleanup()
        {
            gotDealtEvent = false;
            gotShuffleEvent = false;
            gotExhaustedEvent = false;
            gotBurnEvent = false;
            dealtCards = null;
            numBurnt = 0;
        }

        [TestMethod]
        public void TestCtor()
        {
            Assert.ThrowsException<ArgumentException>(() => new Shoe(0));
            Assert.ThrowsException<ArgumentException>(() => new Shoe(-1));

            Shoe implicitDeck = new();
            Assert.AreEqual(1, implicitDeck.NumDecks);
            Assert.AreEqual(52, implicitDeck.Count);
            Assert.IsFalse(implicitDeck.IsExhausted);
            Assert.AreEqual(0, implicitDeck.Index);

            Shoe deck = new(1);
            Assert.AreEqual(1, deck.NumDecks);
            Assert.AreEqual(52, deck.Count);
            Assert.IsFalse(deck.IsExhausted);
            Assert.AreEqual(0, deck.Index);

            Shoe shoe = new(6);
            Assert.AreEqual(6, shoe.NumDecks);
            Assert.AreEqual(6*52, shoe.Count);
            Assert.IsFalse(shoe.IsExhausted);
            Assert.AreEqual(0, shoe.Index);
        }

        private void DealtHandler(object obj, DealtEventArgs args)
        {
            gotDealtEvent = true;
            dealtCards = args.DealtCards;
        }

        private void ShuffleHandler(object obj, EventArgs args)
        {
            gotShuffleEvent = true;
        }

        private void ExhaustedHandler(object obj, EventArgs args)
        {
            gotExhaustedEvent = true;
        }

        [TestMethod]
        public void TestDeckDeal()
        {
            Shoe deck = new();
            Assert.AreEqual(52, deck.NumCardsRemaining);

            Card[] cards = deck.Deal(1);
            Assert.AreEqual(51, deck.NumCardsRemaining);
            Assert.AreEqual(1, cards.Length);
            Assert.AreEqual(52, deck.Count);
            Assert.AreEqual(1, deck.Index);
            Assert.IsFalse(deck.IsExhausted);

            cards = deck.Deal(5);
            Assert.AreEqual(46, deck.NumCardsRemaining);
            Assert.AreEqual(5, cards.Length);
            Assert.AreEqual(52, deck.Count);
            Assert.AreEqual(6, deck.Index);
            Assert.IsFalse(deck.IsExhausted);

            deck.Dealt += DealtHandler;
            deck.Exhausted += ExhaustedHandler;
            deck.Shuffling += ShuffleHandler;
            cards = deck.Deal(46);
            Assert.AreEqual(0, deck.NumCardsRemaining);
            Assert.AreEqual(46, cards.Length);
            Assert.IsTrue(gotDealtEvent);
            Assert.IsFalse(gotExhaustedEvent);
            Assert.IsFalse(gotShuffleEvent);
            Assert.IsNotNull(dealtCards);
            Assert.AreEqual(46, dealtCards.Length);
            Assert.AreEqual(cards, dealtCards);
            Assert.AreEqual(52, deck.Count);
            Assert.AreEqual(52, deck.Index);
            Assert.IsTrue(deck.IsExhausted);
            deck.Dealt -= DealtHandler;

            cards = deck.Deal(1);
            Assert.AreEqual(51, deck.NumCardsRemaining);
            Assert.AreEqual(1, cards.Length);
            Assert.IsTrue(gotShuffleEvent);
            Assert.IsTrue(gotExhaustedEvent);
            Assert.AreEqual(1, deck.Index);
            Assert.IsFalse(deck.IsExhausted);
        }

        [TestMethod]
        public void TestShoeDeal()
        {
            Shoe shoe = new (6);
            Assert.AreEqual(6 * 52, shoe.NumCardsRemaining);
            shoe.Exhausted += ExhaustedHandler;
            shoe.Shuffling += ShuffleHandler;

            Card[] cards = shoe.Deal(104);
            Assert.AreEqual(6 * 52 - 104, shoe.NumCardsRemaining);
            Assert.AreEqual(104, cards.Length);
            Assert.IsFalse(shoe.IsExhausted);
            Assert.IsFalse(gotExhaustedEvent);
            Assert.IsFalse(gotShuffleEvent);

            cards = shoe.Deal(260);
            Assert.AreEqual(260, shoe.NumCardsRemaining);
            Assert.AreEqual(260, cards.Length);
            Assert.IsTrue(gotExhaustedEvent);
            Assert.IsTrue(gotShuffleEvent);
        }

        private void BurnHandler(object obj, BurnEventArgs args)
        {
            gotBurnEvent = true;
            numBurnt = args.NumBurnt;
        }

        [TestMethod]
        public void TestBurn()
        {
            Shoe deck = new();
            Assert.AreEqual(52, deck.NumCardsRemaining);
            deck.Burnt += BurnHandler;
            deck.Exhausted += ExhaustedHandler;
            deck.Shuffling += ShuffleHandler;

            deck.Burn();
            Assert.AreEqual(51, deck.NumCardsRemaining);
            Assert.AreEqual(1, deck.Index);
            Assert.AreEqual(1, numBurnt);
            Assert.IsTrue(gotBurnEvent);
            Assert.IsFalse(gotExhaustedEvent);
            Assert.IsFalse(gotShuffleEvent);

            gotBurnEvent = false;
            deck.Burn(51);
            Assert.AreEqual(0, deck.NumCardsRemaining);
            Assert.AreEqual(52, deck.Index);
            Assert.AreEqual(51, numBurnt);
            Assert.IsTrue(gotBurnEvent);
            Assert.IsFalse(gotExhaustedEvent);
            Assert.IsFalse(gotShuffleEvent);

            gotBurnEvent = false;
            deck.Burn();
            Assert.AreEqual(51, deck.NumCardsRemaining);
            Assert.AreEqual(1, deck.Index);
            Assert.AreEqual(1, numBurnt);
            Assert.IsTrue(gotBurnEvent);
            Assert.IsTrue(gotExhaustedEvent);
            Assert.IsTrue(gotShuffleEvent);
        }

        private static int NumDifferent<T>(T[] a, T[] b)
        {
            if (a.Length == b.Length)
            {
                int numDiff = 0;
                for (int i = 0; i < a.Length; i++)
                {
                    if (!a[i].Equals(b[i]))
                    {
                        numDiff++;
                    }
                }
                return numDiff;
            }
            return -1;
        }

        [TestMethod]
        public void TestShuffle()
        {
            Shoe deck = new();
            deck.Shuffling += ShuffleHandler;

            Card[] firstDeal = deck.Deal(52);
            Card firstCard = firstDeal[0];
            Assert.IsFalse(gotShuffleEvent);
            Assert.AreEqual(52, deck.Index);

            Card[] secondDeal= deck.Deal(52);
            Assert.IsTrue(gotShuffleEvent);
            Assert.AreNotEqual(firstDeal, secondDeal);

            for (int n = 0; n < 10; n++)
            {
                int delta = NumDifferent(firstDeal, secondDeal);
                Assert.IsTrue(delta >= (int)(0.9 * 52));
                firstDeal = deck.Deal(52);
                secondDeal = deck.Deal(52);
            }
        }

        [TestMethod]
        public void TestCutIndex()
        {
            Shoe deck = new();
            Assert.AreEqual(52, deck.NumCardsRemaining);
            Assert.AreEqual(deck.Count, deck.CutIndex);
            Assert.AreEqual(0, deck.Index);
            Assert.IsFalse(deck.IsExhausted);

            deck.Exhausted += ExhaustedHandler;
            deck.Shuffling += ShuffleHandler;

            deck.CutIndex = 48;
            Assert.AreEqual(48, deck.NumCardsRemaining);
            Assert.IsFalse(deck.IsExhausted);
            Assert.AreEqual(0, deck.Index);

            Card[] cards = deck.Deal(48);
            Assert.AreEqual(0, deck.NumCardsRemaining);
            Assert.AreEqual(48, cards.Length);
            Assert.AreEqual(48, deck.Index);
            Assert.IsTrue(deck.IsExhausted);
            Assert.IsFalse(gotExhaustedEvent);
            Assert.IsFalse(gotShuffleEvent);

            Card[] secondDeal = deck.Deal(2);
            Assert.AreEqual(46, deck.NumCardsRemaining);
            Assert.AreEqual(2, secondDeal.Length);
            Assert.AreEqual(2, deck.Index);
            Assert.AreEqual(48, deck.CutIndex);
            Assert.IsFalse(deck.IsExhausted);
            Assert.IsTrue(gotExhaustedEvent);
            Assert.IsTrue(gotShuffleEvent);

            gotShuffleEvent = false;
            gotExhaustedEvent = false;
            Card[] thirdDeal = deck.Deal(52);
            Assert.AreEqual(42, deck.NumCardsRemaining);
            Assert.AreEqual(52, thirdDeal.Length);
            Assert.AreEqual(48, deck.CutIndex);
            Assert.IsFalse(deck.IsExhausted);
            Assert.IsTrue(gotExhaustedEvent);
            Assert.IsTrue(gotShuffleEvent);
        }
    }
}
