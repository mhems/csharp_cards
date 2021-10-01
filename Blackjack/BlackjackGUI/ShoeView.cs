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
    public partial class ShoeView : UserControl, IShoeView
    {
        private int cutIndex;
        private int index;
        private int count;

        public ShoeView()
        {
            InitializeComponent();
        }

        public int CutIndex
        {
            get => cutIndex;
            set
            {
                cutIndex = value;
                cutIndexLabel.Text = cutIndex.ToString();
            }
        }
        public int Index
        {
            get => index;
            set
            {
                index = value;
                indexLabel.Text = index.ToString();
            }
        }
        public int Count
        {
            get => count;
            set
            {
                count = value;
                countLabel.Text = count.ToString();
            }
        }

        public void Burn(int n)
        {
            throw new NotImplementedException();
        }

        public void Shuffle()
        {
            throw new NotImplementedException();
        }
    }
}
