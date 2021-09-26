using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack;

namespace BlackjackViewPresenter
{
    public class BlackjackPlayerPresenter
    {
        private readonly IBlackjackPlayerView view;
        private readonly BlackjackPlayer player;

        public BlackjackPlayerPresenter(IBlackjackPlayerView view, BlackjackPlayer player)
        {
            this.view = view;
            this.player = player;
        }

        public BlackjackPlayer CreateFromView()
        {
            BlackjackPlayer player = new(view.Name);
            player.Bank.Deposit(view.Bank.Balance);
            return player;
        }
    }
}
