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
                UpdateCutIndex();
            }
        }
        public int Index
        {
            get => index;
            set
            {
                index = value;
                UpdateIndex();
            }
        }
        public int Count
        {
            get => count;
            set
            {
                count = value;
                UpdateCount();
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

        private void UpdateCutIndex()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => cutIndexLabel.Text = cutIndex.ToString()));
            }
            else
            {
                cutIndexLabel.Text = cutIndex.ToString();
            }
        }

        private void UpdateIndex()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => indexLabel.Text = index.ToString()));
            }
            else
            {
                indexLabel.Text = index.ToString();
            }
        }

        private void UpdateCount()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => countLabel.Text = count.ToString()));
            }
            else
            {
                countLabel.Text = count.ToString();
            }
        }
    }
}
