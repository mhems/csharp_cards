﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        [TestMethod]
        public void TestDealerPolicy()
        {
            Card ten = CardFactory.GetCard(Card.RankEnum.Ten, Card.SuitEnum.Clubs);
            Card six = CardFactory.GetCard(Card.RankEnum.Six, Card.SuitEnum.Clubs);
            Card seven = CardFactory.GetCard(Card.RankEnum.Seven, Card.SuitEnum.Clubs);
            Card ace = CardFactory.GetCard(Card.RankEnum.Ace, Card.SuitEnum.Clubs);
            DealerDecisionPolicy policy = new();
            BlackjackHand belowHand = new(six, six);
            BlackjackHand hard17 = new(ten, seven);
            BlackjackHand soft17 = new(ace, six);
            BlackjackHand aboveHand = new(ten, ten);
            HashSet<BlackjackActionEnum> actions = new(){ BlackjackActionEnum.Hit, BlackjackActionEnum.Stand };

            Assert.AreEqual(BlackjackActionEnum.Hit, policy.Decide(belowHand, six, actions));
            Assert.AreEqual(BlackjackActionEnum.Hit, policy.Decide(soft17, ace, actions));
            Assert.AreEqual(BlackjackActionEnum.Stand, policy.Decide(hard17, ten, actions));
            Assert.AreEqual(BlackjackActionEnum.Stand, policy.Decide(aboveHand, ten, actions));
        }

        [TestMethod]
        public void TestMinBetPolicy()
        {
            MinimumBettingPolicy policy = new(5);
            Assert.AreEqual(5, policy.Bet());
        }

        [TestMethod]
        public void TestDeclineInsurancePolicy()
        {
            DeclineInsurancePolicy policy = new();
            Card ace = CardFactory.GetCard(Card.RankEnum.Ace, Card.SuitEnum.Clubs);
            BlackjackHand hand = new BlackjackHand(ace, ace);
            foreach (int rankNum in Enum.GetValues(typeof(Card.RankEnum)))
            {
                Card c = CardFactory.GetCard((Card.RankEnum)rankNum, Card.SuitEnum.Clubs);
                Assert.IsFalse(policy.Insure(hand, c));
            }
        }
    }
}