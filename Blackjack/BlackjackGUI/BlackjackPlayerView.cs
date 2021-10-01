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
    public partial class BlackjackPlayerView : UserControl, IBlackjackPlayerView
    {
        private bool displayBalance = true;
        public IBankView Bank => bankView;
        public bool DisplayBalance
        {
            get => displayBalance;
            set
            {
                displayBalance = value;
                bankView.Visible = displayBalance;
            }
        }
        public BlackjackPlayerView()
        {
            InitializeComponent();
        }
    }
}
