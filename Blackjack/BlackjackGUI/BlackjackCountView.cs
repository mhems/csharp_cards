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
    public partial class BlackjackCountView : UserControl, IBlackjackCountView
    {
        public BlackjackCountView()
        {
            InitializeComponent();
            Label sysHdr = new();
            sysHdr.Text = "System";
            sysHdr.Font = new ("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            tableLayoutPanel.Controls.Add(sysHdr, 0, 0);
            Label cntHdr = new();
            cntHdr.Text = "Count";
            cntHdr.Font = new("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            tableLayoutPanel.Controls.Add(cntHdr, 1, 0);
            foreach (BlackjackCountEnum system in Enum.GetValues(typeof(BlackjackCountEnum)))
            {
                int row = (int)system + 1;

                Label sysLbl = new();
                sysLbl.Text = system.ToString();
                tableLayoutPanel.Controls.Add(sysLbl, 0, row);

                Label cntLbl = new();
                cntLbl.Text = "0";
                tableLayoutPanel.Controls.Add(cntLbl, 1, row);
            }
        }

        public void SetCount(BlackjackCountEnum system, float count)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => SetCount(system, count)));
                return;
            }
            Label lbl = (Label)tableLayoutPanel.GetControlFromPosition(1, (int)system + 1);
            lbl.Text = count.ToString();
        }
    }
}
