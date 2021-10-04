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
    public partial class BlackjackTableView : UserControl, IBlackjackTableView
    {
        private readonly IBlackjackTableSlotView[] slots;
        private readonly BlackjackInsuranceDialog insuranceView = new();
        private readonly BlackjackSurrenderDialog surrenderView = new();

        public ILogView Log => logView;
        public IBlackjackConfigView Config => configView;
        public IBlackjackTableSlotView DealerSlot => dealerSlotView;
        public IBlackjackTableSlotView[] PlayerSlots => slots;
        public IBlackjackDecisionView Decision => decisionView;
        public IBlackjackBetView Bet => betView;
        public IBlackjackInsuranceView Insurance => insuranceView;
        public IBlackjackEarlySurrenderView Surrender => surrenderView;
        public IBankView Bank => bankView;
        public IShoeView Shoe => shoeView;
        public IBlackjackCountView Count => countView;

        public event EventHandler<EventArgs> RoundStarted;

        public BlackjackTableView()
        {
            InitializeComponent();
            slots = new BlackjackTableSlotView[1] { playerSlotView };
        }

        private void PlayButton_Click(object sender, EventArgs args)
        {
            RoundStarted?.Invoke(this, new EventArgs());
            playButton.Enabled = false;
        }

        public void EndRound()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => EndRound()));
                return;
            }
            playButton.Enabled = true;
        }
    }
}
