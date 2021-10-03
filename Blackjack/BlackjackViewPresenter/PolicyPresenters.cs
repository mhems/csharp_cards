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
    public interface IDecisionWaiter
    {
        public bool DecisionMade { get; set; }
    }

    public static class DecisionUtils
    {
        public static void WaitOnEvent(this IDecisionWaiter waiter)
        {
            // TODO: polling seems sub-optimal.
            // there should be a library method to sync wait on an event to fire.
            // i cannot find one though.
            while (!waiter.DecisionMade)
            {
                Thread.Sleep(1000);
            }
        }

        public static EventHandler<EventArgs> GetDecisionHandler(this IDecisionWaiter waiter)
        {
            return new EventHandler<EventArgs>((sender, e) => waiter.DecisionMade = true);
        }
    }

    public class InteractiveDecisionPolicy : BlackjackDecisionPolicy, IDecisionWaiter
    {
        private readonly IBlackjackDecisionView view;
        public bool DecisionMade { get; set; }
        public InteractiveDecisionPolicy(IBlackjackDecisionView view)
        {
            this.view = view;
            view.DecisionMade += this.GetDecisionHandler();
        }
        protected override BlackjackActionEnum DecideInner(BlackjackHand hand, Card upCard, HashSet<BlackjackActionEnum> availableActions)
        {
            view.Prompt(hand, upCard, availableActions);
            this.WaitOnEvent();
            return view.Action;
        }
    }

    public class InteractiveBettingPolicy : BlackjackBettingPolicy, IDecisionWaiter
    {
        private readonly IBlackjackBetView view;
        public bool DecisionMade { get; set; }
        public InteractiveBettingPolicy(IBlackjackBetView view)
        {
            this.view = view;
            view.BetMade += this.GetDecisionHandler();
        }
        protected override int BetInner(int minimumBet)
        {
            view.Prompt(minimumBet);
            this.WaitOnEvent();
            return view.Bet;
        }
    }

    public class InteractiveInsurancePolicy : BlackjackInsurancePolicy, IDecisionWaiter
    {
        private readonly IBlackjackInsuranceView view;
        public bool DecisionMade { get; set; }
        public InteractiveInsurancePolicy(IBlackjackInsuranceView view)
        {
            this.view = view;
            view.DecisionMade += this.GetDecisionHandler();
        }
        protected override bool InsureInner(BlackjackHand hand, Card upCard)
        {
            view.Prompt(hand, upCard);
            this.WaitOnEvent();
            return view.Insured;
        }
    }

    public class InteractiveSurrenderPolicy : BlackjackEarlySurrenderPolicy, IDecisionWaiter
    {
        private readonly IBlackjackEarlySurrenderView view;
        public bool DecisionMade { get; set; }
        public InteractiveSurrenderPolicy(IBlackjackEarlySurrenderView view)
        {
            this.view = view;
            view.DecisionMade += this.GetDecisionHandler();
        }
        protected override bool SurrenderInner(BlackjackHand hand, Card upCard)
        {
            view.Prompt(hand, upCard);
            this.WaitOnEvent();
            return view.Surrendered;
        }
    }
}
