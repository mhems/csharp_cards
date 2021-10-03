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
using BlackjackViewPresenter;

namespace BlackjackGUI
{
    public partial class BlackjackApp : Form
    {
        public IBlackjackTableView Table => tableView;
        public BlackjackApp()
        {
            InitializeComponent();
        }
    }
}
