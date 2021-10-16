using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blackjack;
using Cards;

namespace BlackjackViewPresenter
{
    public class InteractiveDecisionPolicy : BlackjackDecisionPolicy
    {
        private readonly IBlackjackDecisionView view;
        private readonly AutoResetEvent are = new(false);
        public InteractiveDecisionPolicy(IBlackjackDecisionView view)
        {
            this.view = view;
            view.Signal = are;
        }
        protected override BlackjackActionEnum DecideInner(BlackjackHand hand, Card upCard, HashSet<BlackjackActionEnum> availableActions)
        {
            view.Prompt(hand, upCard, availableActions);
            are.WaitOne();
            return view.Action;
        }
    }

    public class InteractiveBettingPolicy : BlackjackBettingPolicy
    {
        private readonly IBlackjackBetView view;
        private readonly AutoResetEvent are = new(false);
        public InteractiveBettingPolicy(IBlackjackBetView view)
        {
            this.view = view;
            view.Signal = are;
        }
        protected override int BetInner(int minimumBet, int maximumBet)
        {
            view.Prompt(minimumBet, maximumBet);
            are.WaitOne();
            return view.Bet;
        }
    }

    public class InteractiveInsurancePolicy : BlackjackInsurancePolicy
    {
        private readonly IBlackjackInsuranceView view;
        private readonly AutoResetEvent are = new(false);
        public InteractiveInsurancePolicy(IBlackjackInsuranceView view)
        {
            this.view = view;
            view.Signal = are;
        }
        protected override bool InsureInner(BlackjackHand hand, Card upCard)
        {
            view.Prompt(hand, upCard);
            are.WaitOne();
            return view.Insure;
        }
    }

    public class InteractiveSurrenderPolicy : BlackjackEarlySurrenderPolicy
    {
        private readonly IBlackjackEarlySurrenderView view;
        private readonly AutoResetEvent are = new(false);
        public InteractiveSurrenderPolicy(IBlackjackEarlySurrenderView view)
        {
            this.view = view;
            view.Signal = are;
        }
        protected override bool SurrenderInner(BlackjackHand hand, Card upCard)
        {
            view.Prompt(hand, upCard);
            are.WaitOne();
            return view.Surrender;
        }
    }
}
