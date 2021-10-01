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
    public partial class BlackjackConfigView : UserControl, IBlackjackConfigView
    {
        private Dictionary<string, string> config;
        public Dictionary<string, string> Config
        { 
            get => config;
            set
            {
                config = value;
                DisplayValues();
            }
        }

        public event EventHandler<EventArgs> Changed;

        public BlackjackConfigView()
        {
            InitializeComponent();
        }

        private void DisplayValues()
        {
            // writes config to GUI
        }

        private void RestoreButton_Click(object sender, EventArgs e)
        {
            DisplayValues();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // write GUI values to Config map
            Changed?.Invoke(this, new EventArgs());
        }
    }
}
