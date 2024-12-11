using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static togetherCulture.Utility;

namespace togetherCulture
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            // Disable resizing
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            // Show screen in the center
            StartPosition = FormStartPosition.CenterScreen;

   
            // Enable KeyPreview for the form
            this.KeyPreview = true;

            this.KeyDown += Login_KeyDown;
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            // Retrieve user input
            string username = usernameTxtBox.Text.Trim();
            string password = passwordTxtBox.Text.Trim();

            // Validate input
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowDialogMessage("Username and password are required.", "Error");
                return;
            }

            try
            {
                var userManager = new UserManager();

                bool isLoggedIn = userManager.Login(username, password);

                if (isLoggedIn)
                {
                    // update user last visit date info every time login is successful
                    userManager.UpdateLastVisitInfo();

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Hide();
                }
                else
                {
                    ShowDialogMessage("Invalid username or password.", "Error");
                }
            }
            catch (Exception ex)
            {
                ShowDialogMessage($"An error occurred during login: {ex.Message}", "Error");
            }
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginBtn_Click(sender,e);
                e.SuppressKeyPress = true;
            }
        }

        private void redirectSignupBtn_Click(object sender, EventArgs e)
        {
            Signup signup = new Signup();
            signup.Show();
            Hide();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
