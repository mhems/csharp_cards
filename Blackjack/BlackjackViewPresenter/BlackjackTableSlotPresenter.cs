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
        }

        public void RegisterModel()
        {

        }

        public void UnregisterModel()
        {

        }
    }
}
