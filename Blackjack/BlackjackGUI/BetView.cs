using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlackjackViewPresenter;

namespace BlackjackGUI
{
    public partial class BetView : UserControl, IBlackjackBetView
    {
        private int bet;
        private int minimumBet;

        public int Bet => bet;

        public event EventHandler<EventArgs> BetMade;

        public BetView()
        {
            InitializeComponent();
        }

        public void Prompt(int minimumBet)
        {
            this.minimumBet = minimumBet;
            if (Bet >= minimumBet)
            {
                BetMade?.Invoke(this, new EventArgs());
            }
        }

        private void BetTextBox_TextChanged(object sender, EventArgs e)
        {
            bool isInt = Int32.TryParse(betTextBox.Text, out int val);
            if (isInt && val >= minimumBet)
            {
                bet = val;
                BetMade?.Invoke(this, new EventArgs());
            }
            else
            {
                betTextBox.ForeColor = Color.Red;
            }
        }
    }
}
