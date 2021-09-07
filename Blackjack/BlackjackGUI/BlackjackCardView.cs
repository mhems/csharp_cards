using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cards;

namespace BlackjackGUI
{
    public partial class BlackjackCardView : UserControl
    {
        public BlackjackCardView()
        {
            InitializeComponent();
        }

        public BlackjackCardView(string path = null)
        {
            InitializeComponent();
            if (path != null)
            {
                cardPictureBox.ImageLocation = path;
            }
        }
    }

    public static class CardViewFactory
    {
        private static readonly string[] ranks = new string[] {"2", "3", "4", "5", "6", "7", "8", "9", "10", "j", "q", "k", "a" };
        private static readonly string[] suits = new string[] { "c", "s", "h", "d" };
        private static readonly Dictionary<int, string> viewMap = new();

        public static BlackjackCardView GetCardView(Card card)
        {
            int hash = card.GetHashCode();
            if (!viewMap.ContainsKey(hash))
            {
                string path = $"{suits[(int)card.Suit]}{ranks[(int)card.Rank]}.gif";
                viewMap.Add(hash, path);
            }
            return new BlackjackCardView(viewMap[hash]);

        }
    }
}
