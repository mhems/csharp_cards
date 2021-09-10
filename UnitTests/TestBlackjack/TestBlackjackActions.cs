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
            SplitAction split = new(cfg, hit);
            Assert.AreEqual(BlackjackActionEnum.Split.ToString(), split.ToString());
        }

        [TestMethod]
        public void TestSurrender()
        {
            LateSurrenderAction surrender = new(cfg);
            Assert.AreEqual(BlackjackActionEnum.LateSurrender.ToString(), surrender.ToString());
        }
    }
}
