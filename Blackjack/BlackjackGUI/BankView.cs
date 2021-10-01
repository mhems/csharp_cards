using BlackjackViewPresenter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackjackGUI
{
    public partial class BankView : UserControl, IBankView
    {
        private int balance;

        public BankView()
        {
            InitializeComponent();
        }

        public int Balance
        {
            get => balance;
            set
            {
                balance = value;
                balanceLabel.Text = $"${balance}";
            }
        }
    }
}
