using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public class Player
    {
        private static int id = 0;

        public Bank Bank { get; }
        public string Name { get; protected set; }
        public int Id { get; protected set; }

        public Player(string name) : this(name, new Bank()) { }

        protected Player(string name, Bank bank)
        {
            Name = name;
            this.Bank = bank;
            Id = id;
            id++;
        }

        public void Bet(int amount)
        {
            Bank.Withdraw(amount);
        }

        public void Payout(int amount)
        {
            Bank.Deposit(amount);
        }

        public override string ToString()
        {
            return $"Player '{Name}' ({Id}) : ${Bank.Balance}";
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override bool Equals(object other)
        {
            if (other is Player)
            {
                return GetHashCode() == other.GetHashCode();
            }
            return false;
        }
    }
}
