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

namespace BlackjackGUI
{
    public partial class BlackjackConfigView : UserControl, IBlackjackConfigView
    {
        private BlackjackConfigPresenter presenter;
        private Dictionary<string, string> config;
        private Dictionary<string, TextBox> labelMap = new();
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
            presenter = new(this, new StandardBlackjackConfig());
            presenter.PresentConfigToView();
        }

        private void DisplayValues()
        {
            labelMap.Clear();
            for (int i = 1; i < tableLayoutPanel.RowCount; i++)
            {
               // tableLayoutPanel.Controls.RemoveAt(i);
            }

            // writes config to GUI
            foreach ((string name, string value) in config)
            {
                tableLayoutPanel.RowCount++;
                Label nameLbl = new();
                nameLbl.AutoSize = true;
                nameLbl.Text = name;
                nameLbl.Margin = new Padding(0, 5, 0, 0);
                TextBox valBox = new();
                valBox.Text = value;
                labelMap.Add(name, valBox);
                tableLayoutPanel.Controls.Add(nameLbl, 0, tableLayoutPanel.RowCount - 1);
                tableLayoutPanel.Controls.Add(valBox,  1, tableLayoutPanel.RowCount - 1);
            }
        }

        private void RestoreButton_Click(object sender, EventArgs e)
        {
            foreach ((string name, TextBox box) in labelMap)
            {
                box.Text = config[name];
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            foreach ((string name, TextBox box) in labelMap)
            {
                config[name] = box.Text;
            }
            HashSet<string> badKeys = presenter.SaveViewToConfig();
            if (badKeys.Count > 0)
            {
                foreach (string key in badKeys)
                {
                    labelMap[key].ForeColor = Color.Red;
                }
            }
            foreach ((string name, TextBox box) in labelMap)
            {
                if (!badKeys.Contains(name))
                {
                    box.ForeColor = Color.Black;
                }
            }
        }
    }
}
