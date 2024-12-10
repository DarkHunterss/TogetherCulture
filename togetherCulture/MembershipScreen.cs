using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace togetherCulture
{
    public partial class MembershipScreen : UserControl
    {
        private MainWindow _mainWindow;
        public MembershipScreen(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void backMembershipBtn_Click(object sender, EventArgs e)
        {
            _mainWindow.backMembershipBtn_Click();
        }
    }
}
