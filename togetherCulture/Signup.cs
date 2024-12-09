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
    public partial class Signup : Form
    {

        public Signup()
        {
            InitializeComponent();

            // Disable resizing
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        private void signupBtn_Click(object sender, EventArgs e)
        {
            // Retrieve user input
            string username = usernameTxtBox.Text;
            string email = emailTxtBox.Text;
            string password = passwordTxtBox.Text;


            // Validate input
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ShowDialogMessage("All fields are required.", "Error");
                return;
            }

            if (!IsValidEmail(email))
            {
                ShowDialogMessage("Invalid email address.", "Error");
                return;
            }

            if (!IsValidPassword(password))
            {
                ShowDialogMessage("Password must be at least 8 characters", "Error");
                return;
            }

            try
            {
                var userManager = new UserManager();

                // Attempt sign-up
                bool isSignedUp = userManager.Signup(username, password, email);

                // Provide feedback
                if (isSignedUp)
                {
                    ShowDialogMessage("Sign-up successful! You can now logged in.", "Success");

                    // Clear fields or redirect to the login screen
                    usernameTxtBox.Clear();
                    emailTxtBox.Clear();
                    passwordTxtBox.Clear();
                }
                else
                {
                    ShowDialogMessage("Sign-up failed. Username or email may already exist.", "Error");
                }
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                ShowDialogMessage($"An error occurred during sign-up: {ex.Message}", "Error");
            }
        }

        private void redirectLoginBtn_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            Hide();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {

        }
    }

}
