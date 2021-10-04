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
    public partial class BlackjackTableSlotView : UserControl, IBlackjackTableSlotView
    {
        private readonly List<BankView> potList = new();
        private readonly List<BlackjackHandView> handList = new();
        private bool isDealer;

        public IBlackjackPlayerView Player => player;
        public IBankView[] Pots => potList.ToArray();
        public IBankView Pot => potList[Index];
        public IBlackjackHandView[] Hands => handList.ToArray();
        public IBlackjackHandView Hand => handList[Index];
        public IBankView InsurancePot => insurancePot;
        public int Index { get; set; }
        public bool IsDealer
        {
            get => isDealer;
            set
            {
                isDealer = value;
                UpdateIsDealer();
            }
        }

        public BlackjackTableSlotView()
        {
            InitializeComponent();
            BlackjackHandView handView = new();
            handList.Add(handView);
            tableLayoutPanel.Controls.Add(handView, 0, 1);
            BankView pot = new();
            potList.Add(pot);
            tableLayoutPanel.Controls.Add(pot, 0, 0);
        }

        private void UpdateIsDealer()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => UpdateIsDealer()));
                return;
            }
            potList[0].Visible = !IsDealer;
            insurancePot.Visible = !IsDealer;
        }
    }
}
