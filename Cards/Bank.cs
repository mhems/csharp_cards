using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException() : base() { }
        public InsufficientFundsException(string msg) : base(msg) { }
        public InsufficientFundsException(string msg, Exception inner) : base(msg, inner) { }
    }

    public class Bank
    {
        public event EventHandler<BankTransactionEventArgs> Withdrawn;
        public event EventHandler<BankTransactionEventArgs> Deposited;

        public int Balance { get; protected set; } = 0;
        public Func<double, int> Round { get; set; } = d => (int)Math.Floor(d);

        public Bank(int balance = 0)
        {
            if (balance < 0)
            {
                throw new ArgumentException("Cannot construct a Bank with a negative balance");
            }
            Balance = balance;
        }

        public virtual void TransferFactor(Bank recipient, double factor)
        {
            Transfer(recipient, Balance * factor);
        }

        public virtual void Transfer(Bank recipient)
        {
            Transfer(recipient, Balance);
        }

        public virtual void Transfer(Bank recipient, double amount)
        {
            if (recipient == null)
            {
                throw new ArgumentException("recipient Bank cannot be null");
            }
            if (amount < 0)
            {
                throw new ArgumentException("Cannot transact with a negative amount");
            }
            Withdraw(amount);
            recipient.Deposit(amount);
        }

        public virtual int Withdraw(double amount)
        {
            return Withdraw(Round(amount));
        }

        public virtual int Withdraw(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Cannot withdraw a negative amount");
            }
            if (amount > Balance)
            {
                throw new InsufficientFundsException($"Cannot withdraw ${amount} from ${Balance}");
            }
            Balance -= amount;
            OnWithdrawal(new BankTransactionEventArgs(amount, Balance));
            return amount;
        }

        protected virtual void OnWithdrawal(BankTransactionEventArgs args)
        {
            Withdrawn?.Invoke(this, args);
        }

        public virtual void Deposit(double amount)
        {
            Deposit(Round(amount));
        }

        public virtual void Deposit(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Cannot deposit a negative amount");
            }
            Balance += amount;
            OnDeposit(new BankTransactionEventArgs(amount, Balance));
        }

        protected virtual void OnDeposit(BankTransactionEventArgs args)
        {
            Deposited?.Invoke(this, args);
        }
    }

    public class HouseBank : Bank
    {
        public HouseBank(int balance = 0) : base(balance) { }

        public override int Withdraw(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Cannot withdraw a negative amount");
            }
            Balance -= amount;
            OnWithdrawal(new BankTransactionEventArgs(-amount, Balance));
            return amount;
        }
    }

    public class BankTransactionEventArgs : EventArgs
    {
        public int Amount { get ; }
        public int Balance { get ; }

        public BankTransactionEventArgs(int Amount, int Balance)
        {
            this.Amount = Amount;
            this.Balance = Balance;
        }
    }
}
