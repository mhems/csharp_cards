using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        public event EventHandler<EventArgs> DecisionMade;

        public BlackjackDecisionView()
        {
            InitializeComponent();
            actionButtonMap.Add(BlackjackActionEnum.Hit, hitButton);
            actionButtonMap.Add(BlackjackActionEnum.Stand, standButton);
            actionButtonMap.Add(BlackjackActionEnum.Double, doubleButton);
            actionButtonMap.Add(BlackjackActionEnum.Split, splitButton);
            actionButtonMap.Add(BlackjackActionEnum.LateSurrender, surrenderButton);
        }

        public BlackjackActionEnum Action { get; private set; }

        public void Prompt(BlackjackHand hand, Card upCard, HashSet<BlackjackActionEnum> availableActions)
        {
            foreach (BlackjackActionEnum availableAction in availableActions)
            {
                actionButtonMap[availableAction].Enabled = true;
            }
        }

        private void SignalDecision()
        { 
            DecisionMade?.Invoke(this, new EventArgs());
            EnableAll(false);
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
