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
    public class TestBlackjackPolicies
    {
        private IBlackjackConfig cfg;

        private bool decisionHandled;
        private BlackjackHand handHandled;
        private Card upCardHandled;
        private HashSet<BlackjackActionEnum> availableActionsHandled;
        private BlackjackActionEnum decisionMade;
        private bool betHandled;
        private int amountBet;
        private bool insuranceHandled;
        private bool insuranceTaken;
        private bool surrenderHandled;
        private bool surrenderTaken;

        [TestInitialize]
        public void Setup()
        {
            cfg = new StandardBlackjackConfig();
            decisionHandled = false;
            handHandled = null;
            availableActionsHandled = null;
            betHandled = false;
            amountBet = 0;
            insuranceHandled = false;
            insuranceTaken = false;
            surrenderHandled = false;
            surrenderTaken = false;
        }

        [TestMethod]
        public void TestDealerPolicy()
        {
            Card ten = CardFactory.GetCard(Card.RankEnum.Ten, Card.SuitEnum.Clubs);
            Card six = CardFactory.GetCard(Card.RankEnum.Six, Card.SuitEnum.Clubs);
            Card seven = CardFactory.GetCard(Card.RankEnum.Seven, Card.SuitEnum.Clubs);
            Card ace = CardFactory.GetCard(Card.RankEnum.Ace, Card.SuitEnum.Clubs);
            DealerDecisionPolicy policy = new(cfg);
            BlackjackHand belowHand = new(six, six);
            BlackjackHand hard17 = new(ten, seven);
            BlackjackHand soft17 = new(ace, six);
            BlackjackHand aboveHand = new(ten, ten);
            HashSet<BlackjackActionEnum> actions = new(){ BlackjackActionEnum.Hit, BlackjackActionEnum.Stand };

            policy.Decided += OnDecisionMade;

            Assert.AreEqual(BlackjackActionEnum.Hit, policy.Decide(belowHand, six, actions));

            Assert.IsTrue(decisionHandled);
            Assert.AreEqual(belowHand, handHandled);
            Assert.AreEqual(six, upCardHandled);
            Assert.AreEqual(BlackjackActionEnum.Hit, decisionMade);
            Assert.AreEqual(actions, availableActionsHandled);

            Assert.AreEqual(BlackjackActionEnum.Hit, policy.Decide(soft17, ace, actions));
            Assert.AreEqual(BlackjackActionEnum.Stand, policy.Decide(hard17, ten, actions));
            Assert.AreEqual(BlackjackActionEnum.Stand, policy.Decide(aboveHand, ten, actions));
        }

        [TestMethod]
        public void TestMinBetPolicy()
        {
            MinimumBettingPolicy policy = new(cfg);
            policy.Betting += OnBetMade;
            Assert.AreEqual(cfg.MinimumBet, policy.Bet());

            Assert.IsTrue(betHandled);
            Assert.AreEqual(cfg.MinimumBet, amountBet);
        }

        [TestMethod]
        public void TestDeclineInsurancePolicy()
        {
            Card? c = null;
            DeclineInsurancePolicy policy = new();
            policy.Insured += OnInsurance;
            Card ace = CardFactory.GetCard(Card.RankEnum.Ace, Card.SuitEnum.Clubs);
            BlackjackHand hand = new (ace, ace);
            foreach (int rankNum in Enum.GetValues(typeof(Card.RankEnum)))
            {
                c = CardFactory.GetCard((Card.RankEnum)rankNum, Card.SuitEnum.Clubs);
                Assert.IsFalse(policy.Insure(hand, c.Value));
            }

            Assert.IsTrue(insuranceHandled);
            Assert.IsFalse(insuranceTaken);

            Assert.AreEqual(hand, handHandled);
            Assert.AreEqual(c, upCardHandled);
        }

        [TestMethod]
        public void TestDeclineSurrenderPolicy()
        {
            Card? c = null;
            DeclineEarlySurrenderPolicy policy = new();
            policy.Surrendered += OnSurrender;
            Card eight = CardFactory.GetCard(Card.RankEnum.Eight, Card.SuitEnum.Clubs);
            BlackjackHand hand = new(eight, eight);
            foreach (int rankNum in Enum.GetValues(typeof(Card.RankEnum)))
            {
                c = CardFactory.GetCard((Card.RankEnum)rankNum, Card.SuitEnum.Clubs);
                Assert.IsFalse(policy.Surrender(hand, c.Value));
            }

            Assert.IsTrue(surrenderHandled);
            Assert.IsFalse(surrenderTaken);

            Assert.AreEqual(hand, handHandled);
            Assert.AreEqual(c, upCardHandled);
        }

        public void OnDecisionMade(object _, BlackjackDecisionEventArgs args)
        {
            decisionHandled = true;
            handHandled = args.Hand;
            upCardHandled = args.UpCard;
            decisionMade = args.Decision;
            availableActionsHandled = args.AvailableActions;
        }

        public void OnBetMade(object _, BlackjackBetEventArgs args)
        {
            betHandled = true;
            amountBet = args.Amount;
        }

        public void OnInsurance(object _, BlackjackInsuranceEventArgs args)
        {
            insuranceHandled = true;
            insuranceTaken = args.Insured;
            handHandled = args.Hand;
            upCardHandled = args.UpCard;
        }

        public void OnSurrender(object _, BlackjackEarlySurrenderEventArgs args)
        {
            surrenderHandled = true;
            surrenderTaken = args.Surrendered;
            handHandled = args.Hand;
            upCardHandled = args.UpCard;
        }
    }
}
