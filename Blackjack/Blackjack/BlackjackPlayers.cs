using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;

namespace Blackjack
{
    public class BlackjackPlayer : Player
    {
        public IBlackjackActionPolicy ActionPolicy { get; set; }
        public IBlackjackBettingPolicy BettingPolicy { get; set; }
        public IBlackjackInsurancePolicy InsurancePolicy { get; set; }

        public BlackjackPlayer(string name) : base(name) { }

        protected BlackjackPlayer(string name, Bank bank) : base(name, bank) { }
    }

    public class BlackjackDealer : BlackjackPlayer
    {
        public BlackjackDealer() : base("Dealer", new HouseBank())
        { 
            ActionPolicy = new DealerActionPolicy(); 
        }
    }
}
