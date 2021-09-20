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
        public BlackjackDecisionPolicy DecisionPolicy { get; set; }
        public BlackjackEarlySurrenderPolicy EarlySurrenderPolicy { get; set; }
        public BlackjackBettingPolicy BettingPolicy { get; set; }
        public BlackjackInsurancePolicy InsurancePolicy { get; set; }

        public BlackjackPlayer(string name) : base(name) { }

        protected BlackjackPlayer(string name, Bank bank) : base(name, bank) { }
    }

    public class BlackjackDealer : BlackjackPlayer
    {
        public const string DealerName = "Dealer";
        public BlackjackDealer(IBlackjackConfig config) : base(DealerName, new Bank())
        { 
            DecisionPolicy = new DealerDecisionPolicy(config); 
        }
    }
}
