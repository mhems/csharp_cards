using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack;

namespace TestBlackjack
{
    [TestClass]
    public class TestBlackjackActions
    {
        [TestMethod]
        public void TestHit()
        {
            HitAction hit = new();
            Assert.AreEqual(BlackjackActionEnum.Hit.ToString(), hit.ToString());
            Assert.IsTrue(hit.Available());
        }

        [TestMethod]
        public void TestStand()
        {
            StandAction stand = new();
            Assert.AreEqual(BlackjackActionEnum.Stand.ToString(), stand.ToString());
            Assert.IsTrue(stand.Available());
        }

        [TestMethod]
        public void TestDouble()
        {
            DoubleAction double_ = new();
            Assert.AreEqual(BlackjackActionEnum.Double.ToString(), double_.ToString());
        }

        [TestMethod]
        public void TestSplit()
        {
            SplitAction split = new();
            Assert.AreEqual(BlackjackActionEnum.Split.ToString(), split.ToString());
        }

        [TestMethod]
        public void TestSurrender()
        {
            SurrenderAction surrender = new();
            Assert.AreEqual(BlackjackActionEnum.Surrender.ToString(), surrender.ToString());
        }
    }
}
