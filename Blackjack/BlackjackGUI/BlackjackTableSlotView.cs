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
        private readonly List<IBankView> potList = new();
        private readonly List<IBlackjackHandView> handList = new();

        public IBlackjackPlayerView Player => player;
        public IBankView[] Pots => potList.ToArray();
        public IBankView Pot => potList[Index];
        public IBlackjackHandView[] Hands => handList.ToArray();
        public IBlackjackHandView Hand => handList[Index];
        public IBankView InsurancePot => insurancePot;
        public int Index { get; set; }

        public BlackjackTableSlotView()
        {
            InitializeComponent();
        }
    }
}
