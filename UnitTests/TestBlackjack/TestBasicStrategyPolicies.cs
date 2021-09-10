using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack;
using System.IO;

namespace TestBlackjack
{
    [TestClass]
    public class TestBasicStrategyPolicies
    {
        private static bool CompareFiles(string name, string prefix)
        {
            return File.ReadAllBytes(name).SequenceEqual(File.ReadAllBytes(Path.Join(prefix, name)));
        }

        [TestMethod]
        public void TestSerialization()
        {
            BasicStrategyPolicies policy = new();
            string dir = "tmp";
            policy.Save(dir);

            Assert.IsTrue(CompareFiles("BasicChart.csv", dir));
            Assert.IsTrue(CompareFiles("HardChart.csv", dir));
            Assert.IsTrue(CompareFiles("SoftChart.csv", dir));
            Assert.IsTrue(CompareFiles("PairChart.csv", dir));
        }

        [TestMethod]
        public void TestEarlySurrender()
        {
            StandardBlackjackConfig cfg = new();
            BasicEarlySurrenderPolicy policy = new(cfg);
            // TODO
        }
    }
}
