using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Blackjack;
using BlackjackViewPresenter;
using Cards;

namespace BlackjackGUI
{
    public class BlackjackInsuranceDialog : IBlackjackInsuranceView
    {
        public bool Insure { get; set; }
        public AutoResetEvent Signal { get; set; }
        public void Prompt(BlackjackHand hand, Card upCard)
        {
            DialogResult result = MessageBox.Show("Dealer shows an Ace.\nTake insurance?", "Insurance", MessageBoxButtons.YesNo);
            Insure = result == DialogResult.Yes;
            Signal.Set();
        }
    }

    public class BlackjackSurrenderDialog : IBlackjackEarlySurrenderView
    {
        public bool Surrender { get; set; }
        public AutoResetEvent Signal { get; set; }
        public void Prompt(BlackjackHand hand, Card upCard)
        {
            DialogResult result = MessageBox.Show("Surrender your hand early?", "Early Surrender", MessageBoxButtons.YesNo);
            Surrender = result == DialogResult.Yes;
            Signal.Set();
        }
    }
}
