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
using Blackjack;
using BlackjackViewPresenter;

namespace BlackjackGUI
{
    public partial class BlackjackHandView : UserControl, IBlackjackHandView
    {
        private int count = 0;
        private int handValue;
        private bool bust;
        private bool blackjack;

        public BlackjackHand Hand
        {
            get => Presenter.Hand;
            set => Presenter.Hand = value;
        }
        internal BlackjackHandPresenter Presenter { get; private set; }
        public int Value
        {
            get => handValue;
            set
            {
                handValue = value;
                UpdateValue();
            }
        }
        public bool Bust
        {
            get => bust;
            set
            {
                bust = value;
                UpdateBust();
            }
        }

        public bool Blackjack
        {
            get => blackjack;
            set
            {
                blackjack = value;
                UpdateBlackjack();
            }
        }

        public BlackjackHandView()
        {
            InitializeComponent();
            Presenter = new BlackjackHandPresenter(this);
        }
 
        public void AddCard(Card card, bool visible=true)
        {
            BlackjackCardView cardView = CardViewFactory.GetCardView(card, visible);
            if (count >= 2)
            {
                cardTable.ColumnCount++;
                cardTable.ColumnStyles[count - 1].SizeType = SizeType.Absolute;
                cardTable.ColumnStyles[count - 1].Width = 20F;
                cardTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            }
            cardTable.Controls.Add(cardView, count, 0);
            count++;
        }

        public void ClearHand()
        {
            for (int i = count; i >= 0; i--)
            {
                cardTable.Controls.RemoveAt(i);
            }
            RestoreValue();
        }

        private void UpdateValue()
        {
            valueTextBox.Text = Value.ToString();
        }

        private void RestoreValue()
        {
            valueTextBox.Text = string.Empty;
            valueTextBox.ForeColor = Color.Black;
        }

        private void UpdateBust()
        {
            valueTextBox.ForeColor = Color.Red;
        }

        private void UpdateBlackjack()
        {
            valueTextBox.ForeColor = Color.Green;
        }
    }
}
