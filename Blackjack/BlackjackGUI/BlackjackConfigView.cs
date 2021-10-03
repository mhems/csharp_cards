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
        private Dictionary<string, string> config;
        private readonly Dictionary<string, TextBox> labelMap = new();
        public Dictionary<string, string> Config
        { 
            get => config;
            set
            {
                config = value;
                DisplayValues();
            }
        }
        public BlackjackConfigPresenter Presenter { get; set; }

        public BlackjackConfigView()
        {
            InitializeComponent();
        }

        private void DisplayValues()
        {
            if (labelMap.Count > 0)
            {
                UpdateValues();
            }
            else if (config != null)
            {
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
                    tableLayoutPanel.Controls.Add(valBox, 1, tableLayoutPanel.RowCount - 1);
                }
            }
        }

        private void UpdateValues()
        {
            foreach ((string name, TextBox box) in labelMap)
            {
                box.Text = config[name];
            }
        }

        private void RestoreButton_Click(object sender, EventArgs e)
        {
            UpdateValues();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            foreach ((string name, TextBox box) in labelMap)
            {
                config[name] = box.Text;
            }
            HashSet<string> badKeys = Presenter.SaveViewToConfig();
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
