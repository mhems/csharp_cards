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
        private bool hitHandled;
        private bool actHandled;
        private BlackjackActionEnum actionHandled;
        private bool actionDone;
        private Card hitCard;

        [TestInitialize]
        public void Initialize()
        {
            hitHandled = false;
            actHandled = false;
            actionDone = false;
        }

        [TestMethod]
        public void TestHit()
        {
            Card six = CardFactory.GetCard(Card.RankEnum.Six, Card.SuitEnum.Clubs);
            Card seven = CardFactory.GetCard(Card.RankEnum.Seven, Card.SuitEnum.Hearts);
            TestShoe shoe = TestUtilities.MakeTestShoe(new List<Card> { seven });
            HitAction hit = new(shoe);

            hit.Hit += OnHit;
            hit.Acted += OnAction;

            Assert.AreEqual(BlackjackActionEnum.Hit.ToString(), hit.ToString());

            BlackjackTableSlot slot = TestUtilities.MakeTestSlot();
            IBlackjackConfig config = slot.Config;
            int startingBalance = slot.Player.Bank.Balance;

            slot.Hand.Add(six);
            slot.Hand.Add(six);

            Assert.IsTrue(hit.Available(slot));

            bool done = hit.Act(slot);
            Assert.IsFalse(done);
            Assert.AreEqual(3, slot.Hand.Count);
            Assert.AreEqual(seven, slot.Hand[2]);
            Assert.AreEqual(1, slot.NumHands);
            Assert.AreEqual(0, slot.NumSplits);

            Assert.IsTrue(hitHandled);
            Assert.IsTrue(actHandled);
            Assert.IsFalse(actionDone);
            Assert.AreEqual(seven, hitCard);
            Assert.AreEqual(BlackjackActionEnum.Hit, actionHandled);

            Assert.IsTrue(hit.Available(slot));
            Assert.AreEqual(startingBalance, slot.Player.Bank.Balance);
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);
        }

        [TestMethod]
        public void TestStand()
        {
            StandAction stand = new();
            Assert.AreEqual(BlackjackActionEnum.Stand.ToString(), stand.ToString());
            BlackjackTableSlot slot = TestUtilities.MakeTestSlot();
            IBlackjackConfig config = slot.Config;
            int startingBalance = slot.Player.Bank.Balance;

            stand.Acted += OnAction;

            Assert.IsTrue(stand.Available(slot));

            bool done = stand.Act(slot);
            Assert.IsTrue(done);
            Assert.AreEqual(0, slot.Hand.Count);
            Assert.AreEqual(1, slot.NumHands);
            Assert.AreEqual(0, slot.NumSplits);

            Assert.IsTrue(actHandled);
            Assert.IsTrue(actionDone);
            Assert.AreEqual(BlackjackActionEnum.Stand, actionHandled);

            Assert.IsTrue(stand.Available(slot));
            Assert.AreEqual(startingBalance, slot.Player.Bank.Balance);
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);
        }

        [TestMethod]
        public void TestDouble()
        {
            Card five = CardFactory.GetCard(Card.RankEnum.Five, Card.SuitEnum.Clubs);
            Card ten = CardFactory.GetCard(Card.RankEnum.Ten, Card.SuitEnum.Diamonds);
            TestShoe shoe = new(new List<Card>() { ten });

            BlackjackTableSlot slot = TestUtilities.MakeTestSlot();
            IBlackjackConfig config = slot.Config;
            int startingBalance = slot.Player.Bank.Balance;

            slot.Hand.Add(five);
            slot.Hand.Add(five);

            HitAction hit = new(shoe);
            StandAction stand = new();
            DoubleAction double_ = new(config, hit, stand);
            Assert.AreEqual(BlackjackActionEnum.Double.ToString(), double_.ToString());

            double_.Acted += OnAction;
            hit.Hit += OnHit;

            Assert.IsTrue(double_.Available(slot));

            bool done = double_.Act(slot);

            Assert.IsTrue(done);
            Assert.AreEqual(3, slot.Hand.Count);
            Assert.AreEqual(1, slot.NumHands);
            Assert.AreEqual(0, slot.NumSplits);
            Assert.AreEqual(ten, slot.Hand[2]);

            Assert.IsFalse(double_.Available(slot));
            Assert.AreEqual(startingBalance - config.MinimumBet, slot.Player.Bank.Balance);
            Assert.AreEqual(2 * config.MinimumBet, slot.Pot.Balance);

            Assert.IsTrue(actHandled);
            Assert.IsTrue(actionDone);
            Assert.AreEqual(BlackjackActionEnum.Double, actionHandled);

            Assert.IsTrue(hitHandled);
            Assert.AreEqual(ten, hitCard);

            Assert.ThrowsException<ActionUnavailableException>(() => double_.Act(slot));
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

            BlackjackTableSlot slot = TestUtilities.MakeTestSlot();
            IBlackjackConfig config = slot.Config;
            HitAction hit = new(shoe);
            SplitAction split = new(slot.Config, hit);
            Assert.AreEqual(BlackjackActionEnum.Split.ToString(), split.ToString());

            split.Acted += OnAction;
            hit.Hit += OnHit;

            int startingBalance = slot.Player.Bank.Balance;
            slot.Hand.Add(fourOfSpades);
            slot.Hand.Add(fourOfClubs);

            Assert.IsTrue(split.Available(slot));
            Assert.AreEqual(1, slot.NumHands);
            Assert.AreEqual(0, slot.NumSplits);

            split.Act(slot);

            Assert.IsTrue(actHandled);
            Assert.IsFalse(actionDone);
            Assert.AreEqual(BlackjackActionEnum.Split, actionHandled);
            Assert.IsTrue(hitHandled);
            Assert.AreEqual(Card.RankEnum.Four, hitCard.Rank);

            Assert.AreEqual(2, slot.NumHands);
            Assert.AreEqual(1, slot.NumSplits);

            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);
            Assert.AreEqual(2, slot.Hand.Count);
            Assert.AreEqual(fourOfSpades, slot.Hand[0]);
            Assert.AreEqual(fourOfDiamonds, slot.Hand[1]);
            Assert.AreEqual(startingBalance - config.MinimumBet, slot.Player.Bank.Balance);
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);

            slot.Index = 1;
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);
            Assert.AreEqual(2, slot.Hand.Count);
            Assert.AreEqual(fourOfClubs, slot.Hand[0]);
            Assert.AreEqual(fourOfHearts, slot.Hand[1]);
            Assert.AreEqual(startingBalance - config.MinimumBet, slot.Player.Bank.Balance);
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);

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
            Assert.AreEqual(startingBalance - 2 * config.MinimumBet, slot.Player.Bank.Balance);
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);

            slot.Index = 1;
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);
            Assert.AreEqual(fourOfDiamonds, slot.Hand[0]);
            Assert.AreEqual(Card.RankEnum.Five, slot.Hand[1].Rank);
            Assert.IsFalse(split.Available(slot));
            Assert.AreEqual(startingBalance - 2 * config.MinimumBet, slot.Player.Bank.Balance);
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);

            // ---

            slot.Index = 2;
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);
            Assert.AreEqual(fourOfClubs, slot.Hand[0]);
            Assert.AreEqual(fourOfHearts, slot.Hand[1]);
            Assert.IsTrue(split.Available(slot));
            Assert.AreEqual(startingBalance - 2 * config.MinimumBet, slot.Player.Bank.Balance);
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);

            split.Act(slot);

            Assert.AreEqual(4, slot.NumHands);
            Assert.AreEqual(3, slot.NumSplits);

            slot.Index = 0;
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);
            Assert.AreEqual(fourOfSpades, slot.Hand[0]);
            Assert.AreEqual(Card.RankEnum.Eight, slot.Hand[1].Rank);
            Assert.IsFalse(split.Available(slot));
            Assert.AreEqual(startingBalance - 3 * config.MinimumBet, slot.Player.Bank.Balance);
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);

            slot.Index = 1;
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);
            Assert.AreEqual(fourOfDiamonds, slot.Hand[0]);
            Assert.AreEqual(Card.RankEnum.Five, slot.Hand[1].Rank);
            Assert.IsFalse(split.Available(slot));
            Assert.AreEqual(startingBalance - 3 * config.MinimumBet, slot.Player.Bank.Balance);
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);

            slot.Index = 2;
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);
            Assert.IsFalse(split.Available(slot));
            Assert.AreEqual(fourOfClubs, slot.Hand[0]);
            Assert.AreEqual(Card.RankEnum.Seven, slot.Hand[1].Rank);
            Assert.AreEqual(startingBalance - 3 * config.MinimumBet, slot.Player.Bank.Balance);
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);

            slot.Index = 3;
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);
            Assert.IsFalse(split.Available(slot));
            Assert.AreEqual(fourOfHearts, slot.Hand[0]);
            Assert.AreEqual(Card.RankEnum.Three, slot.Hand[1].Rank);
            Assert.AreEqual(startingBalance - 3 * config.MinimumBet, slot.Player.Bank.Balance);
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);
        }

        [TestMethod]
        public void TestSurrender()
        {
            Card ten = CardFactory.GetCard(Card.RankEnum.Ten, Card.SuitEnum.Diamonds);
            BlackjackTableSlot slot = TestUtilities.MakeTestSlot();
            IBlackjackConfig config = slot.Config;
            int startingBalance = slot.Player.Bank.Balance;

            slot.Hand.Add(ten);
            slot.Hand.Add(ten);

            LateSurrenderAction surrender = new(config);
            Assert.AreEqual(BlackjackActionEnum.LateSurrender.ToString(), surrender.ToString());

            surrender.Acted += OnAction;

            Assert.IsTrue(surrender.Available(slot));

            bool done = surrender.Act(slot);

            Assert.IsTrue(done);
            Assert.AreEqual(2, slot.Hand.Count);
            Assert.AreEqual(1, slot.NumHands);
            Assert.AreEqual(0, slot.NumSplits);

            Assert.AreEqual(startingBalance, slot.Player.Bank.Balance);
            Assert.AreEqual(config.MinimumBet, slot.Pot.Balance);
            Assert.IsTrue(slot.Surrendered);

            Assert.IsTrue(actHandled);
            Assert.IsTrue(actionDone);
            Assert.AreEqual(BlackjackActionEnum.LateSurrender, actionHandled);
        }

        public void OnHit(object obj, BlackjackHitActionEventArgs args)
        {
            hitHandled = true;
            hitCard = args.CardReceived;
        }

        public void OnAction(object obj, BlackjackActionEventArgs args)
        {
            actHandled = true;
            actionHandled = args.Kind;
            actionDone = args.Done;
        }
    }
}
