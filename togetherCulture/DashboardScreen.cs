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

namespace togetherCulture
{
    public partial class DashboardScreen : UserControl
    {
        private MainWindow _mainWindow;
        public DashboardScreen(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            usernameLbl.Text = Globals.CurrentLoggedInUsername.ToString();

            LoadUserImage();
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

        private void LoadUserImage()
        {
            // Get the saved image path from application settings
            string savedImagePath = Properties.Settings.Default.UserImagePath;

            if (!string.IsNullOrEmpty(savedImagePath) && File.Exists(savedImagePath))
            {
                // Load the saved user image
                userImg.Image = Image.FromFile(savedImagePath);
            }
            else
            {
                // Load a default image if no saved image is found
                string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

                DirectoryInfo directoryInfo = new DirectoryInfo(currentDirectory);
                while (directoryInfo != null && directoryInfo.Name != "togetherCulture")
                {
                    directoryInfo = directoryInfo.Parent;
                }

                string userImagePath = Path.Combine(directoryInfo.FullName, "resources", "defaultUserImg.png");
                userImg.Image = Image.FromFile(userImagePath);
            }
        }

        private void userImg_Click(object sender, EventArgs e)
        {
            // Open a file dialog to select a new image
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select an Image";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Load the selected image
                        string selectedImagePath = openFileDialog.FileName;
                        userImg.Image = Image.FromFile(selectedImagePath);

                        // Save the image path to application settings
                        Properties.Settings.Default.UserImagePath = selectedImagePath;
                        Properties.Settings.Default.Save();

                        MessageBox.Show("Image updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void upgradeBtn_Click(object sender, EventArgs e)
        {
            _mainWindow.upgradeBtn_Click();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
