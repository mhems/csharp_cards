using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack;
using Cards;

namespace BlackjackViewPresenter
{
    public class InteractiveDecisionPolicy : BlackjackDecisionPolicy
    {
        private readonly IBlackjackDecisionView view;
        public InteractiveDecisionPolicy(IBlackjackDecisionView view)
        {
            this.view = view;
        }
        protected override BlackjackActionEnum DecideInner(BlackjackHand hand, Card upCard, HashSet<BlackjackActionEnum> availableActions)
        {
            view.Prompt(hand, upCard, availableActions);
            // wait
            return view.Action;
        }
    }

    public class InteractiveBettingPolicy : BlackjackBettingPolicy
    {
        private readonly IBlackjackBetView view;
        public InteractiveBettingPolicy(IBlackjackBetView view)
        {
            this.view = view;
        }
        protected override int BetInner()
        {
            view.Prompt();
            // wait
            return view.Bet;
        }
    }

    public class InteractiveInsurancePolicy : BlackjackInsurancePolicy
    {
        private readonly IBlackjackInsuranceView view;
        public InteractiveInsurancePolicy(IBlackjackInsuranceView view)
        {
            this.view = view;
        }
        protected override bool InsureInner(BlackjackHand hand, Card upCard)
        {
            view.Prompt(hand, upCard);
            // wait
            return view.Insured;
        }
    }

    public class InteractiveSurrenderPolicy : BlackjackEarlySurrenderPolicy
    {
        private readonly IBlackjackEarlySurrenderView view;
        public InteractiveSurrenderPolicy(IBlackjackEarlySurrenderView view)
        {
            this.view = view;
        }
        protected override bool SurrenderInner(BlackjackHand hand, Card upCard)
        {
            view.Prompt(hand, upCard);
            // wait
            return view.Surrendered;
        }
    }
}
