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
    public class TestBlackjackPlayers
    {
        [TestMethod]
        public void TestBlackjackPlayer()
        {
            BlackjackPlayer p1 = new("test");

            Assert.AreEqual("test", p1.Name);
            Assert.AreEqual(0, p1.Bank.Balance);
        }

        [TestMethod]
        public void TestDealer()
        {
            IBlackjackConfig config = new StandardBlackjackConfig();
            BlackjackDealer dealer = new(config);

            Assert.AreEqual(BlackjackDealer.DealerName, dealer.Name);
            Assert.AreEqual(0, dealer.Bank.Balance);
            Assert.IsTrue(dealer.DecisionPolicy is DealerDecisionPolicy);
        }
    }
}
