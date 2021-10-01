using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack;
using Cards;

namespace BlackjackViewPresenter
{
    public class BlackjackHandPresenter
    {
        private readonly IBlackjackHandView view;
        private BlackjackHand hand;

        public BlackjackHand Hand
        {
            get => hand;
            set
            {
                if (hand != null)
                {
                    UnregisterModel();
                }
                hand = value;
                RegisterModel();
            }
        }


        public BlackjackHandPresenter(IBlackjackHandView view)
        {
            this.view = view;
        }

        public BlackjackHandPresenter(IBlackjackHandView view, BlackjackHand hand) : this(view)
        {
            Hand = hand;
        }

        public void RegisterModel()
        {
            Hand.Added += CardAddedHandler;
            Hand.Cleared += HandClearedHandler;
        }

        public void UnregisterModel()
        {
            Hand.Added -= CardAddedHandler;
            Hand.Cleared -= HandClearedHandler;
        }

        private void CardAddedHandler(object obj, CardAddedEventArgs args)
        {
            view.AddCard(args.AddedCard, args.Visible);
            if (obj is BlackjackHand hand)
            {
                view.Value = hand.Value;
                if (hand.IsBust)
                {
                    view.Bust = true;
                }
                if (hand.IsBlackjack)
                {
                    view.Blackjack = true;
                }
            }
        }

        private void HandClearedHandler(object obj, EventArgs _)
        {
            view.ClearHand();
            view.Value = 0;
        }
    }
}
