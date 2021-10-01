using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack;

namespace BlackjackViewPresenter
{
    public class BlackjackTablePresenter
    {
        private readonly IBlackjackTableView view;
        private readonly BlackjackTable table;

        public BlackjackTablePresenter(IBlackjackTableView view, BlackjackTable table)
        {
            this.view = view;
            this.table = table;
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
