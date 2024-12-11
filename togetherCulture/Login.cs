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
                    // Check if preferences exist for this user
                    if (!DoesUserPreferenceExist(Globals.CurrentLoggedInUserID))
                    {
                        // Show User Preference Screen
                        using (UserPreferenceScreen preferenceScreen = new UserPreferenceScreen())
                        {
                            var result = preferenceScreen.ShowDialog();

                            if (result == DialogResult.OK)
                            {
                                // Update user last visit date info every time login is successful
                                userManager.UpdateLastVisitInfo();

                                // Preferences saved successfully
                                OpenMainWindow();
                                this.Hide();
                                return; // Exit to avoid reopening login
                            }
                            else
                            {
                                MessageBox.Show(
                                    "You need to set up your preferences before proceeding to the dashboard.",
                                    "Setup Required",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning
                                );
                                return; // Stay on the login form
                            }
                        }
                    }

                    // Update user last visit date info every time login is successful
                    userManager.UpdateLastVisitInfo();

                    // If preferences already exist, proceed to MainWindow
                    OpenMainWindow();
                    this.Hide();
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

        // Helper Method to Open MainWindow
        private void OpenMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show(); 
        }


        // Helper Method to Check User Preferences
        private bool DoesUserPreferenceExist(int userId)
        {
            string query = "SELECT COUNT(1) FROM user_preference WHERE UserID = @UserId";
            SqlParameter[] parameters = {
                new SqlParameter("@UserId", userId)
            };

            object result = DBConnection.getConnectionInstance().executeScalar(query, parameters);
            return Convert.ToInt32(result) > 0; // Returns true if preferences exist
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
    }
}
