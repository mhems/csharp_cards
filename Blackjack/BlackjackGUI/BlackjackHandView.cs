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
        private bool isDealer;

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
        public bool IsDealer
        {
            get => isDealer;
            set
            {
                isDealer = value;
                UpdateIsDealer();
            }
        }

        public BlackjackHandView()
        {
            InitializeComponent();
            Presenter = new BlackjackHandPresenter(this);
        }
 
        public void AddCard(Card card, bool visible=true)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => AddCard(card, visible)));
                return;
            }
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
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => ClearHand()));
                return;
            }
            for (int i = cardTable.ColumnCount - 1; i >= 0; i--)
            {
                try
                {
                    cardTable.Controls.RemoveAt(i);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    MessageBox.Show(e.StackTrace);
                }
            }
            RestoreValue();
            count = 0;
            cardTable.ColumnCount = 2;
        }

        public void RevealHoleCard()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => RevealHoleCard()));
                return;
            }
            BlackjackCardView cardView = (BlackjackCardView)cardTable.GetControlFromPosition(0, 0);
            cardView.FaceUp = true;
            valueTextBox.Visible = true;
        }

        private void UpdateIsDealer()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => UpdateIsDealer()));
                return;
            }
            valueTextBox.Visible = !IsDealer;
        }

        private void UpdateValue()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => UpdateValue()));
                return;
            }
            valueTextBox.Text = Value.ToString();
        }

        private void RestoreValue()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => RestoreValue()));
                return;
            }
            valueTextBox.Text = string.Empty;
            valueTextBox.ForeColor = Color.Black;
        }

        private void UpdateBust()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => UpdateBust()));
                return;
            }
            valueTextBox.ForeColor = Color.Red;
        }

        private void UpdateBlackjack()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => UpdateBlackjack()));
                return;
            }
            valueTextBox.ForeColor = Color.Green;
        }
    }
}
