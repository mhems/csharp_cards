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
        private HitAction hit;
        private StandAction stand; 

        [TestInitialize]
        public void Setup()
        {
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
            DoubleAction double_ = new(hit, stand);
            Assert.AreEqual(BlackjackActionEnum.Double.ToString(), double_.ToString());
        }

        [TestMethod]
        public void TestSplit()
        {
            SplitAction split = new(hit);
            Assert.AreEqual(BlackjackActionEnum.Split.ToString(), split.ToString());
        }

        [TestMethod]
        public void TestSurrender()
        {
            LateSurrenderAction surrender = new();
            Assert.AreEqual(BlackjackActionEnum.LateSurrender.ToString(), surrender.ToString());
        }
    }
}
