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
using BlackjackViewPresenter;

namespace BlackjackGUI
{
    public partial class BlackjackCardView : UserControl, ICardView
    {
        private const string prefix = @"C:\Users\15854\source\repos\cs_cards\res\";
        private static readonly string[] ranks = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "j", "q", "k", "a" };
        private static readonly string[] suits = new string[] { "c", "s", "h", "d" };
        private bool faceUp;

        public Card.RankEnum Rank { get; set; }
        public Card.SuitEnum Suit { get; set; }
        public bool FaceUp
        {
            get => faceUp;
            set
            {
                faceUp = value;
                UpdateVisibility();
            }
        }

        public BlackjackCardView(Card card, bool faceUp)
        {
            Rank = card.Rank;
            Suit = card.Suit;
            FaceUp = faceUp;
        }

        public void UpdateVisibility()
        {
            if (FaceUp)
            {
                cardPictureBox.ImageLocation = GetCardPath();
            }
            else
            {
                cardPictureBox.ImageLocation = GetCardBackPath();
            }
        }

        public string GetCardPath()
        {
            return $"{prefix}{suits[(int)Suit]}{ranks[(int)Rank]}.gif";
        }

        public static string GetCardBackPath()
        {
            return "back.gif";
        }
    }

    public static class CardViewFactory
    {
        private static readonly Dictionary<int, BlackjackCardView> viewMap = new();
  
        public static BlackjackCardView GetCardView(Card card, bool faceUp)
        {
            int hash = card.GetHashCode();
            if (!viewMap.ContainsKey(hash))
            {
                BlackjackCardView view = new(card, faceUp);
                viewMap.Add(hash, view);
            }
            return viewMap[hash];

        }
    }
}
