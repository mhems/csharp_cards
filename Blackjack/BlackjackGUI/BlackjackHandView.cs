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
    public partial class BlackjackHandView : UserControl
    {
        private int count = 2;
        public BlackjackHandView()
        {
            InitializeComponent();
        }
 
        internal void AddCard(BlackjackCardView card)
        {
            if (count >= 2)
            {
                cardTable.ColumnCount++;
                cardTable.ColumnStyles[count - 1].SizeType = SizeType.Absolute;
                cardTable.ColumnStyles[count - 1].Width = 20F;
                cardTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            }
            cardTable.Controls.Add(card, count, 0);
            count++;
        }
    }
}
