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

        public event EventHandler<BlackjackDecisionEventArgs> Decided
        {
            add { DecisionPolicy.Decided += value; }
            remove { DecisionPolicy.Decided -= value; }
        }

        public event EventHandler<BlackjackEarlySurrenderEventArgs> Surrendered
        {
            add { EarlySurrenderPolicy.Surrendered += value; }
            remove { EarlySurrenderPolicy.Surrendered -= value; }
        }

        public event EventHandler<BlackjackBetEventArgs> Betting
        {
            add { BettingPolicy.Betting += value; }
            remove { BettingPolicy.Betting -= value; }
        }

        public event EventHandler<BlackjackInsuranceEventArgs> Insured
        {
            add { InsurancePolicy.Insured += value; }
            remove { InsurancePolicy.Insured -= value; }
        }

        public BlackjackPlayer(string name) : base(name) { }

        protected BlackjackPlayer(string name, Bank bank) : base(name, bank) { }
    }

    public class BlackjackDealer : BlackjackPlayer
    {
        public BlackjackDealer(IBlackjackConfig config) : base("Dealer", new Bank())
        { 
            DecisionPolicy = new DealerDecisionPolicy(config); 
        }
    }
}
