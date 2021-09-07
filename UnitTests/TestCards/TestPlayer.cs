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

            p1.Payout(100);
            Assert.AreEqual(100, p1.Bank.Balance);

            p1.Bet(50);
            Assert.AreEqual(50, p1.Bank.Balance);
        }
    }
}
