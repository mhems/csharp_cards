using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlackjackViewPresenter;

namespace BlackjackGUI
{
    public partial class BetView : UserControl, IBlackjackBetView
    {
        private int minimumBet;
        private int maximumBet;
        private bool prompted;

        public int Bet { get; set; }
        public AutoResetEvent Signal { get; set; }

        public BetView()
        {
            InitializeComponent();
            betTextBox.Text = "0";
        }

        public void Prompt(int minimumBet, int maximumBet)
        {
            this.minimumBet = minimumBet;
            this.maximumBet = maximumBet;
            prompted = true;
            ParseBet();
        }

        private void BetButton_Click(object sender, EventArgs e)
        {
            ParseBet();
        }

        private void ParseBet()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => ParseBet()));
                return;
            }

            bool isInt = Int32.TryParse(betTextBox.Text, out int val);
            if (isInt && prompted && val >= minimumBet && val <= maximumBet)
            {
                Bet = val;
                SetColor(Color.Black);
                Signal.Set();
            }
            else
            {
                SetColor(Color.Red);
            }
        }

        private void SetColor(Color color)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => SetColor(color)));
                return;
            }
            betTextBox.ForeColor = color;
        }
    }
}
