using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


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
            LoadUserPreferences();
            LoadBenefitsOverview();
            LoadActivityDetails();
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

        private void LoadUserPreferences()
        {
            try
            {
                // Fetch user preferences
                string query = "SELECT Interest, Intention FROM user_preference WHERE UserID = @UserId";
                SqlParameter[] parameters = { new SqlParameter("@UserId", Globals.CurrentLoggedInUserID) };
                DataTable result = DBConnection.getConnectionInstance().executeQuery(query, parameters);

                if (result.Rows.Count > 0)
                {
                    DataRow row = result.Rows[0];
                    string intention = row["Intention"].ToString();
                    string interests = row["Interest"].ToString();

                    // Process top 3 interests
                    string[] interestList = interests.Split(',')
                                                      .Select(i => i.Trim())
                                                      .OrderBy(i => int.Parse(i.Split(':')[1])) // Sort by rank
                                                      .Take(3) // Take top 3
                                                      .Select(i => i.Split(':')[0]) // Extract interest name
                                                      .ToArray();

                    // Populate interestsDataPnl
                    PopulateFlowLayout(interestsDataPnl, interestList);

                    // Populate intentionDataPnl
                    PopulateFlowLayout(intentionDataPnl, new string[] { intention });
                }
                else
                {
                    // If no preferences exist
                    PopulateFlowLayout(interestsDataPnl, new string[] { "No Interests Available" });
                    PopulateFlowLayout(intentionDataPnl, new string[] { "No Intention Available" });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user preferences: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateFlowLayout(FlowLayoutPanel panel, string[] items)
        {
            panel.Controls.Clear();

            // Configure FlowLayoutPanel for vertical alignment
            panel.FlowDirection = FlowDirection.TopDown;
            panel.WrapContents = false;

            // Calculate padding to center content vertically
            int totalHeight = items.Length * 40; // Approx. 40px height per label with spacing
            int padding = (panel.Height - totalHeight) / 2;

            if (padding > 0)
            {
                panel.Padding = new Padding(0, padding, 0, 0);
            }

            foreach (var item in items)
            {
                Label label = new Label
                {
                    Text = item,
                    Font = new System.Drawing.Font("Segoe UI", 12F),
                    AutoSize = false,
                    Width = panel.Width,
                    Height = 30,
                    Margin = new Padding(0, 0, 0, 10), // Add 10px spacing below each label
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                };

                panel.Controls.Add(label);
            }
        }

        public void LoadBenefitsOverview()
        {
            try
            {
                // Fetch user's membership data
                string membershipQuery = @"SELECT m.Benefits FROM membership m INNER JOIN users u ON m.ID = u.MembershipID WHERE u.ID = @UserId";
                SqlParameter[] membershipParams = { new SqlParameter("@UserId", Globals.CurrentLoggedInUserID) };
                DataTable membershipTable = DBConnection.getConnectionInstance().executeQuery(membershipQuery, membershipParams);

                benefitsDataPnl.Controls.Clear(); // Clear existing controls

                if (membershipTable.Rows.Count > 0)
                {
                    // User has a membership
                    string benefits = membershipTable.Rows[0]["Benefits"].ToString();
                    string[] benefitList = benefits.Split(',')
                                                    .Select(b => $"- {b.Trim()}")
                                                    .ToArray();

                    foreach (string benefit in benefitList)
                    {
                        Label benefitLabel = new Label
                        {
                            Text = benefit,
                            Font = new System.Drawing.Font("Segoe UI", 10F),
                            AutoSize = true,
                            Margin = new Padding(5)
                        };
                        benefitsDataPnl.Controls.Add(benefitLabel);
                    }
                }
                else
                {
                    // No membership, display a message
                    Label noBenefitsLabel = new Label
                    {
                        Text = "No benefits are applied yet. Buy a membership for amazing features.",
                        Font = new System.Drawing.Font("Segoe UI", 10F),
                        AutoSize = true,
                        ForeColor = System.Drawing.Color.Gray,
                        Margin = new Padding(5)
                    };
                    benefitsDataPnl.Controls.Add(noBenefitsLabel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading benefits: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadActivityDetails()
        {
            try
            {
                // Clear existing controls
                activityDataPnl.Controls.Clear();

                // Create and configure the chart
                Chart activityChart = new Chart
                {
                    Dock = DockStyle.Fill,
                    BackColor = System.Drawing.Color.White
                };

                // Create the chart area
                ChartArea chartArea = new ChartArea
                {
                    Name = "MainChartArea",
                    BackColor = System.Drawing.Color.LightGray
                };
                activityChart.ChartAreas.Add(chartArea);

                // Create the data series
                Series series = new Series
                {
                    Name = "ActivitySeries",
                    ChartType = SeriesChartType.Column,
                    ChartArea = "MainChartArea",
                    IsValueShownAsLabel = true
                };

                // Example data
                var activityData = new Dictionary<string, int>
                {
                    { "Logins", 10 },
                    { "Documents", 5 },
                    { "Events", 3 },
                    { "Downloads", 7 }
                };

                foreach (var item in activityData)
                {
                    series.Points.AddXY(item.Key, item.Value);
                }

                activityChart.Series.Add(series);

                // Add the chart to the panel
                activityDataPnl.Controls.Add(activityChart);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading activity details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void upgradeBtn_Click(object sender, EventArgs e)
        {
            _mainWindow.upgradeBtn_Click();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
