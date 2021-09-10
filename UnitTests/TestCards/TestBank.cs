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
    public class TestBank
    {
        private bool gotDepositEvent;
        private bool gotWithdrawEvent;
        private double eventAmount;
        private double eventBalance;

        [TestInitialize]
        public void Setup()
        {
            gotDepositEvent = false;
            gotWithdrawEvent = false;
            eventAmount = 0;
        }

        private void DepositHandler(object obj, BankTransactionEventArgs args)
        {
            gotDepositEvent = true;
            eventAmount = args.Amount;
            eventBalance = args.Balance;
        }

        private void WithdrawHandler(object obj, BankTransactionEventArgs args)
        {
            gotWithdrawEvent = true;
            eventAmount = args.Amount;
            eventBalance = args.Balance;
        }

        [TestMethod]
        public void TestDeposit()
        {
            Bank bank = new (100);
            bank.Deposited += DepositHandler;
            bank.Withdrawn += WithdrawHandler;

            Assert.ThrowsException<ArgumentException>(() => bank.Deposit(-10));
            Assert.IsFalse(gotDepositEvent);
            Assert.IsFalse(gotWithdrawEvent);

            bank.Deposit(50);

            Assert.AreEqual(150, bank.Balance);
            Assert.IsTrue(gotDepositEvent);
            Assert.IsFalse(gotWithdrawEvent);
            Assert.AreEqual(150, eventBalance);
            Assert.AreEqual(50, eventAmount);
        }

        [TestMethod]
        public void TestWithdraw()
        {
            Bank bank = new(100);
            bank.Deposited += DepositHandler;
            bank.Withdrawn += WithdrawHandler;

            Assert.ThrowsException<ArgumentException>(() => bank.Withdraw(-10));
            Assert.IsFalse(gotDepositEvent);
            Assert.IsFalse(gotWithdrawEvent);

            bank.Withdraw(100);

            Assert.AreEqual(0, bank.Balance);
            Assert.IsFalse(gotDepositEvent);
            Assert.IsTrue(gotWithdrawEvent);
            Assert.AreEqual(0, eventBalance);
            Assert.AreEqual(100, eventAmount);
            gotWithdrawEvent = false;

            Assert.ThrowsException<InsufficientFundsException>(() => bank.Withdraw(5));
            Assert.IsFalse(gotDepositEvent);
            Assert.IsFalse(gotWithdrawEvent);
            Assert.AreEqual(0, bank.Balance);
        }

        [TestMethod]
        public void TestTransact()
        {
            Bank donor = new(100);
            Bank donee = new(0);

            donor.Withdrawn += WithdrawHandler;
            donee.Deposited += DepositHandler;

            Assert.ThrowsException<ArgumentException>(() => donor.Transfer(null, 0));
            Assert.ThrowsException<ArgumentException>(() => donor.Transfer(donee, -10));

            donor.Transfer(donee, 50);
            Assert.AreEqual(50, donor.Balance);
            Assert.AreEqual(50, donee.Balance);
        }

        [TestMethod]
        public void TestHouseBank()
        {
            Bank bank = new HouseBank(100);
            bank.Withdraw(100);
            Assert.AreEqual(0, bank.Balance);

            bank.Withdraw(100);
            Assert.AreEqual(-100, bank.Balance);
        }
    }
}
