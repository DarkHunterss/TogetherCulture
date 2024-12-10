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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();

            // Disable resizing
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            var userManager = new UserManager();
            userManager.Logout();

            Login login = new Login();
            login.Show();
            Hide();
        }
    }
}
