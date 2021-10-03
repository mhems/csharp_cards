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
    public partial class LogView : UserControl, ILogView
    {
        public LogView()
        {
            InitializeComponent();
            new BlackjackViewPresenter.LogPresenter(this, new Blackjack.BlackjackEventLogger());
        }

        public void Clear()
        {
            textBox.Clear();
        }

        public void Log(string entry)
        {
            textBox.Text += entry + "\n";
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
