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
using Blackjack;
using BlackjackViewPresenter;
using Cards;

namespace BlackjackGUI
{
    public partial class BlackjackDecisionView : UserControl, IBlackjackDecisionView
    {
        private readonly Dictionary<BlackjackActionEnum, Button> actionButtonMap = new();

        public BlackjackActionEnum Action { get; set; }
        public AutoResetEvent Signal { get; set; }


        public BlackjackDecisionView()
        {
            InitializeComponent();
            actionButtonMap.Add(BlackjackActionEnum.Hit, hitButton);
            actionButtonMap.Add(BlackjackActionEnum.Stand, standButton);
            actionButtonMap.Add(BlackjackActionEnum.Double, doubleButton);
            actionButtonMap.Add(BlackjackActionEnum.Split, splitButton);
            actionButtonMap.Add(BlackjackActionEnum.LateSurrender, surrenderButton);
            EnableAll(false);
        }

        public void Prompt(BlackjackHand hand, Card upCard, HashSet<BlackjackActionEnum> availableActions)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => Prompt(hand, upCard, availableActions)));
            }
            foreach (BlackjackActionEnum availableAction in availableActions)
            {
                actionButtonMap[availableAction].Enabled = true;
            }
        }

        private void SignalDecision()
        { 
            EnableAll(false);
            Signal.Set();
        }

        private void EnableAll(bool enable)
        {
            hitButton.Enabled = enable;
            standButton.Enabled = enable;
            doubleButton.Enabled = enable;
            splitButton.Enabled = enable;
            surrenderButton.Enabled = enable;
        }

        private void HitButton_Click(object sender, EventArgs e)
        {
            Action = BlackjackActionEnum.Hit;
            SignalDecision();
        }

        private void StandButton_Click(object sender, EventArgs e)
        {
            Action = BlackjackActionEnum.Stand;
            SignalDecision();
        }

        private void DoubleButton_Click(object sender, EventArgs e)
        {
            Action = BlackjackActionEnum.Double;
            SignalDecision();
        }

        private void SplitButton_Click(object sender, EventArgs e)
        {
            Action = BlackjackActionEnum.Split;
            SignalDecision();
        }

        private void SurrenderButton_Click(object sender, EventArgs e)
        {
            Action = BlackjackActionEnum.LateSurrender;
            SignalDecision();
        }
    }
}
