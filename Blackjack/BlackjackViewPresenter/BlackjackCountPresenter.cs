using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack;

namespace BlackjackViewPresenter
{
    public class BlackjackCountPresenter
    {
        private readonly IBlackjackCountView view;
        private readonly BlackjackCount count;

        public BlackjackCountPresenter(IBlackjackCountView view, BlackjackCount count)
        {
            this.view = view;
            this.count = count;
            RegisterModel();
        }

        public void RegisterModel()
        {
            count.Changed += CountChangedHandler;
        }

        public void UnregisterModel()
        {
            count.Changed -= CountChangedHandler;
        }

        private void CountChangedHandler(object obj, EventArgs _)
        {
            if (obj is BlackjackCount c)
            {
                foreach (BlackjackCountEnum system in Enum.GetValues(typeof(BlackjackCountEnum)))
                {
                    view.SetCount(system, c[system]);
                }
            }
        }
    }
}
