using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack;

namespace BlackjackViewPresenter
{
    public class BlackjackTableSlotPresenter
    {
        private readonly IBlackjackTableSlotView view;
        private readonly BlackjackTableSlot slot;

        public BlackjackTableSlotPresenter(IBlackjackTableSlotView view, BlackjackTableSlot slot)
        {
            this.view = view;
            this.slot = slot;
            RegisterModel();
            if (slot.IsDealer)
            {
                view.IsDealer = true;
                view.Hands[0].IsDealer = true;
            }
        }

        public void RegisterModel()
        {
            if (slot.IsDealer)
            {
                slot.RoundBegun += RoundBeginHandler;
            }
        }

        public void UnregisterModel()
        {
            if (slot.IsDealer)
            {
                slot.RoundBegun -= RoundBeginHandler;
            }
        }

        private void RoundBeginHandler(object _, EventArgs args)
        {
            view.Hands[0].IsDealer = slot.IsDealer;
        }
    }
}
