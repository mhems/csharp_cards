using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Blackjack;
using BlackjackViewPresenter;
using Cards;

namespace BlackjackGUI
{
    public class BlackjackInsuranceDialog : IBlackjackInsuranceView
    {
        public bool Insured { get; private set; }

        public event EventHandler<EventArgs> DecisionMade;

        public void Prompt(BlackjackHand hand, Card upCard)
        {
            DialogResult result = MessageBox.Show("Insurance", "Take insurance?", MessageBoxButtons.YesNo);
            Insured = result == DialogResult.Yes;
            DecisionMade?.Invoke(this, new EventArgs());
        }
    }

    public class BlackjackSurrenderDialog : IBlackjackEarlySurrenderView
    {
        public bool Surrendered { get; private set; }

        public event EventHandler<EventArgs> DecisionMade;

        public void Prompt(BlackjackHand hand, Card upCard)
        {
            DialogResult result = MessageBox.Show("Early Surrender", "Surrender early?", MessageBoxButtons.YesNo);
            Surrendered = result == DialogResult.Yes;
            DecisionMade?.Invoke(this, new EventArgs());
        }
    }
}
