using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cards;

namespace BlackjackGUI
{
    public partial class BlackjackApp : Form
    {
        public BlackjackApp()
        {
            InitializeComponent();
        }

        private void hitButton_Click(object sender, EventArgs e)
        {
            Card card = CardFactory.GetCard((Card.RankEnum)new Random().Next(0, 13), Card.SuitEnum.Hearts);
            playerView.HandView.AddCard(card);
        }
    }
}
