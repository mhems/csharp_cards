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
    public class TestPlayer
    {
        private bool spentCalled;
        private bool earnedCalled;
        private int amount;

        private void SpentHandler(object obj, BankTransactionEventArgs args)
        {
            spentCalled = true;
            amount = args.Amount;
        }

        private void EarnedHandler(object obj, BankTransactionEventArgs args)
        {
            earnedCalled = true;
            amount = args.Amount;
        }

        [TestInitialize]
        public void Setup()
        {
            spentCalled = false;
            earnedCalled = false;
        }

        [TestMethod]
        public void TestCtor()
        {
            Player p1 = new("Alice");
            Player p2 = new("Bob");

            Assert.AreEqual("Alice", p1.Name);
            Assert.AreEqual(0, p1.Bank.Balance);


            Assert.AreEqual("Bob", p2.Name);
            Assert.AreEqual(0, p2.Bank.Balance);

            Assert.AreNotEqual(p1.Id, p2.Id);
            Assert.AreNotEqual(p1.GetHashCode(), p2.GetHashCode());
            Assert.IsFalse(p1.Equals(p2));

            Player p3 = new("Alice");
            Assert.IsFalse(p1.Equals(p3));
        }

        [TestMethod]
        public void TestPlayerBank()
        {
            Player p1 = new("Alice");
            p1.Spent += SpentHandler;
            p1.Earned += EarnedHandler;

            p1.Payout(100);
            Assert.AreEqual(100, p1.Bank.Balance);
            Assert.IsTrue(earnedCalled);
            Assert.IsFalse(spentCalled);
            Assert.AreEqual(100, amount);

            amount = 0;
            earnedCalled = false;
            p1.Bet(50);
            Assert.AreEqual(50, p1.Bank.Balance);
            Assert.IsFalse(earnedCalled);
            Assert.IsTrue(spentCalled);
            Assert.AreEqual(50, amount);
        }
    }
}
