using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack;
using Cards;

namespace TestBlackjack
{
    [TestClass]
    public class TestBlackjackActions
    {
        private IBlackjackConfig cfg;
        private HitAction hit;
        private StandAction stand; 

        [TestInitialize]
        public void Setup()
        {
            cfg = new StandardBlackjackConfig();
            Shoe shoe = new(1);
            hit = new(shoe);
            stand = new();
        }

        [TestMethod]
        public void TestHit()
        {
            Assert.AreEqual(BlackjackActionEnum.Hit.ToString(), hit.ToString());
            Assert.IsTrue(hit.Available(null));
        }

        [TestMethod]
        public void TestStand()
        {
            Assert.AreEqual(BlackjackActionEnum.Stand.ToString(), stand.ToString());
            Assert.IsTrue(stand.Available(null));
        }

        [TestMethod]
        public void TestDouble()
        {
            DoubleAction double_ = new(cfg, hit, stand);
            Assert.AreEqual(BlackjackActionEnum.Double.ToString(), double_.ToString());
        }

        [TestMethod]
        public void TestSplit()
        {
            Card fourOfSpades = CardFactory.GetCard(Card.RankEnum.Four, Card.SuitEnum.Spades);
            Card fourOfHearts = CardFactory.GetCard(Card.RankEnum.Four, Card.SuitEnum.Hearts);
            Card fourOfClubs = CardFactory.GetCard(Card.RankEnum.Four, Card.SuitEnum.Clubs);
            Card fourOfDiamonds = CardFactory.GetCard(Card.RankEnum.Four, Card.SuitEnum.Diamonds);

            TestShoe shoe = TestUtilities.MakeTestShoe(new List<Card>
            {
                fourOfDiamonds,
                fourOfHearts,
                CardFactory.GetCard(Card.RankEnum.Eight, Card.SuitEnum.Clubs),
                CardFactory.GetCard(Card.RankEnum.Five, Card.SuitEnum.Hearts),
                CardFactory.GetCard(Card.RankEnum.Seven, Card.SuitEnum.Diamonds),
                CardFactory.GetCard(Card.RankEnum.Three, Card.SuitEnum.Spades)
            });
            hit = new(shoe);
            SplitAction split = new(cfg, hit);
            Assert.AreEqual(BlackjackActionEnum.Split.ToString(), split.ToString());

            IBlackjackConfig config = new StandardBlackjackConfig();
            BlackjackTableSlot slot = new(config);
            slot.Player = TestUtilities.MakeTestPlayer();
            int startingBalance = slot.Player.Bank.Balance;
            slot.Player.Bank.Transfer(slot.Pot, config.MinimumBet);
            slot.Hand.Add(fourOfSpades);
            slot.Hand.Add(fourOfClubs);

            Assert.IsTrue(split.Available(slot));
            Assert.AreEqual(1, slot.NumHands);
            Assert.AreEqual(0, slot.NumSplits);

            split.Act(slot);

            Assert.AreEqual(2, slot.NumHands);
            Assert.AreEqual(1, slot.NumSplits);

            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);
            Assert.AreEqual(2, slot.Hand.Count);
            Assert.AreEqual(fourOfSpades, slot.Hand[0]);
            Assert.AreEqual(fourOfDiamonds, slot.Hand[1]);

            slot.Index = 1;
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);
            Assert.AreEqual(2, slot.Hand.Count);
            Assert.AreEqual(fourOfClubs, slot.Hand[0]);
            Assert.AreEqual(fourOfHearts, slot.Hand[1]);

            // ---

            slot.Index = 1;
            Assert.IsTrue(split.Available(slot));

            slot.Index = 0;
            Assert.IsTrue(split.Available(slot));

            split.Act(slot);

            Assert.AreEqual(3, slot.NumHands);
            Assert.AreEqual(2, slot.NumSplits);

            slot.Index = 0;
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);
            Assert.AreEqual(fourOfSpades, slot.Hand[0]);
            Assert.AreEqual(Card.RankEnum.Eight, slot.Hand[1].Rank);
            Assert.IsFalse(split.Available(slot));

            slot.Index = 1;
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);
            Assert.AreEqual(fourOfDiamonds, slot.Hand[0]);
            Assert.AreEqual(Card.RankEnum.Five, slot.Hand[1].Rank);
            Assert.IsFalse(split.Available(slot));

            // ---

            slot.Index = 2;
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);
            Assert.AreEqual(fourOfClubs, slot.Hand[0]);
            Assert.AreEqual(fourOfHearts, slot.Hand[1]);
            Assert.IsTrue(split.Available(slot));

            split.Act(slot);

            Assert.AreEqual(4, slot.NumHands);
            Assert.AreEqual(3, slot.NumSplits);

            slot.Index = 0;
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);
            Assert.AreEqual(fourOfSpades, slot.Hand[0]);
            Assert.AreEqual(Card.RankEnum.Eight, slot.Hand[1].Rank);
            Assert.IsFalse(split.Available(slot));

            slot.Index = 1;
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);
            Assert.AreEqual(fourOfDiamonds, slot.Hand[0]);
            Assert.AreEqual(Card.RankEnum.Five, slot.Hand[1].Rank);
            Assert.IsFalse(split.Available(slot));

            slot.Index = 2;
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);
            Assert.IsFalse(split.Available(slot));
            Assert.AreEqual(fourOfClubs, slot.Hand[0]);
            Assert.AreEqual(Card.RankEnum.Seven, slot.Hand[1].Rank);

            slot.Index = 3;
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);
            Assert.IsFalse(split.Available(slot));
            Assert.AreEqual(fourOfHearts, slot.Hand[0]);
            Assert.AreEqual(Card.RankEnum.Three, slot.Hand[1].Rank);

            Assert.AreEqual(startingBalance - 4 * config.MinimumBet, slot.Player.Bank.Balance);
        }

        [TestMethod]
        public void TestSurrender()
        {
            LateSurrenderAction surrender = new(cfg);
            Assert.AreEqual(BlackjackActionEnum.LateSurrender.ToString(), surrender.ToString());
        }
    }
}
