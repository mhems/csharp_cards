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
        private readonly BlackjackHand hand;
        private readonly Func<Card, ICardView> cardViewFactory;

        public BlackjackHandPresenter(IBlackjackHandView view, BlackjackHand hand, Func<Card, ICardView> cardViewFactory)
        {
            this.view = view;
            this.hand = hand;
            this.cardViewFactory = cardViewFactory;
            RegisterModel();
        }

        public void RegisterModel()
        {
            hand.Added += CardAddedHandler;
            hand.Cleared += HandClearedHandler;
        }

        public void UnregisterModel()
        {
            hand.Added -= CardAddedHandler;
            hand.Cleared -= HandClearedHandler;
        }

        private void CardAddedHandler(object obj, CardAddedEventArgs args)
        {
            ICardView cardView = cardViewFactory.Invoke(args.AddedCard);
            view.AddCard(cardView);
            if (obj is BlackjackHand hand)
            {
                view.Value = hand.Value;
            }
        }

        private void HandClearedHandler(object obj, EventArgs _)
        {
            view.ClearHand();
            view.Value = 0;
        }
    }
}
