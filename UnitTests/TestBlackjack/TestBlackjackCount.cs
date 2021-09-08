using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;
using Blackjack;

namespace TestBlackjack
{
    [TestClass]
    public class TestBlackjackCount
    {
        private Shoe shoe;

        [TestInitialize]
        public void Setup()
        {
            shoe = new(1);
        }

        private void CheckAllZero(BlackjackCount count)
        {
            Array systems = Enum.GetValues(typeof(BlackjackCountEnum));
            foreach (BlackjackCountEnum system in systems)
            {
                Assert.AreEqual(0, count[system]);
            }
        }

        private void CheckAnyNonZero(BlackjackCount count)
        {
            Array systems = Enum.GetValues(typeof(BlackjackCountEnum));
            IEnumerable<BlackjackCountEnum> systemsIter = systems.OfType<BlackjackCountEnum>();
            int numNonZeroCounts = systemsIter.Select(s => count[s]).Where(c => c != 0).Count();

            // this should probabilistically be true
            Assert.IsTrue(numNonZeroCounts > 0);
        }

        [TestMethod]
        public void TestCount()
        {
            BlackjackCount count = new(shoe);

            CheckAllZero(count);

            shoe.Deal(10);

            CheckAnyNonZero(count);

            shoe.Shuffle();

            CheckAllZero(count);
        }

        [TestMethod]
        public void TestReset()
        {
            BlackjackCount count = new(shoe);

            CheckAllZero(count);

            shoe.Deal(10);

            CheckAnyNonZero(count);

            count.Reset();

            CheckAllZero(count);
        }
    }
}
