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
    public partial class DashboardScreen : UserControl
    {
        public event EventHandler CloseRequested;

        public DashboardScreen()
        {
            InitializeComponent();

            usernameLbl.Text = Globals.CurrentLoggedInUser.ToString();
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            var userManager = new UserManager();
            userManager.Logout();

            Login login = new Login();
            login.Show();

            Form parentForm = FindForm();
            if (parentForm != null)
            {
                parentForm.Hide(); // Hide the parent window
            }


            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
