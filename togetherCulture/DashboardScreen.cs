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
            LoadSuggestedEvents();
            LoadActivityDetails(); //"Column", "Bar", "Pie", "Line", "Area"
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

        private void LoadSuggestedEvents()
        {
            try
            {
                // Clear existing controls
                eventsDataPnl.Controls.Clear();

                // Query to fetch user's interests ranked 1 or 2
                string interestQuery = "SELECT Interest FROM user_preference WHERE UserID = @UserId";
                SqlParameter[] interestParams = { new SqlParameter("@UserId", Globals.CurrentLoggedInUserID) };
                DataTable interestTable = DBConnection.getConnectionInstance().executeQuery(interestQuery, interestParams);

                if (interestTable.Rows.Count > 0)
                {
                    // Extract user interests with rank 1 or 2
                    string[] rankedInterests = interestTable.Rows[0]["Interest"].ToString()
                                                .Split(',')
                                                .Select(i => i.Trim())
                                                .Where(i => int.Parse(i.Split(':')[1]) <= 2) // Filter ranks 1 and 2
                                                .Select(i => i.Split(':')[0]) // Extract interest names
                                                .ToArray();

                    if (rankedInterests.Length > 0)
                    {
                        // Build a query to fetch events that match user interests
                        string eventQuery = @"
                    SELECT ID, Name, Description, Date, Location, AttendanceCount 
                    FROM event_list 
                    WHERE ";
                        eventQuery += string.Join(" OR ", rankedInterests.Select((_, index) => $"Tags LIKE @Interest{index}"));

                        // Create SQL parameters for interests
                        List<SqlParameter> eventParams = rankedInterests
                            .Select((interest, index) => new SqlParameter($"@Interest{index}", $"%{interest}%"))
                            .ToList();

                        DataTable eventTable = DBConnection.getConnectionInstance().executeQuery(eventQuery, eventParams.ToArray());

                        if (eventTable.Rows.Count > 0)
                        {
                            // Populate the suggested events
                            foreach (DataRow row in eventTable.Rows)
                            {
                                string eventName = row["Name"].ToString();
                                string description = row["Description"].ToString();
                                string location = row["Location"].ToString();
                                DateTime eventDate = Convert.ToDateTime(row["Date"]);
                                int attendanceCount = Convert.ToInt32(row["AttendanceCount"]);

                                AddEventToPanel(eventName, description, location, eventDate, attendanceCount);
                            }
                        }
                        else
                        {
                            AddNoEventsMessage("No events match your top interests.");
                        }
                    }
                    else
                    {
                        AddNoEventsMessage("No top-ranked interests found to suggest events.");
                    }
                }
                else
                {
                    AddNoEventsMessage("No interests found. Update your profile to see suggested events.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading suggested events: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddEventToPanel(string eventName, string description, string location, DateTime eventDate, int attendanceCount)
        {
            Panel eventPanel = new Panel
            {
                BackColor = Color.White,
                Size = new Size(eventsDataPnl.Width - 40, 130),
                Margin = new Padding(10)
            };

            Label nameLabel = new Label
            {
                Text = $"Event: {eventName}",
                Font = new Font("Segoe UI semibold", 12),
                Location = new Point(10, 10),
                AutoSize = true
            };

            Label descriptionLabel = new Label
            {
                Text = $"Description: {description}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(10, 40),
                Size = new Size(eventPanel.Width - 20, 40)
            };

            Label locationLabel = new Label
            {
                Text = $"Location: {location}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(10, 90),
                AutoSize = true
            };

            Label dateLabel = new Label
            {
                Text = $"Date: {eventDate.ToShortDateString()}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(300, 90),
                AutoSize = true
            };

            Label attendanceLabel = new Label
            {
                Text = $"Attendance: {attendanceCount}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(500, 90),
                AutoSize = true
            };

            eventPanel.Controls.Add(nameLabel);
            eventPanel.Controls.Add(descriptionLabel);
            eventPanel.Controls.Add(locationLabel);
            eventPanel.Controls.Add(dateLabel);
            eventPanel.Controls.Add(attendanceLabel);

            eventsDataPnl.Controls.Add(eventPanel);
        }

        private void AddNoEventsMessage(string message)
        {
            Label noEventsLabel = new Label
            {
                Text = message,
                Font = new Font("Segoe UI", 12, FontStyle.Italic),
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            eventsDataPnl.Controls.Add(noEventsLabel);
        }

        public void LoadActivityDetails(string chartType = "Area")
        {
            try
            {
                // Clear existing controls in the activity panel
                chatPnl.Controls.Clear();

                // Initialize activity counts
                int loginCount = 0;
                int documentCount = 0;
                int eventRegistrationCount = 0;

                // Fetch data
                string loginQuery = "SELECT TotalVisits FROM users WHERE ID = @UserId";
                SqlParameter[] loginParams = { new SqlParameter("@UserId", Globals.CurrentLoggedInUserID) };
                object loginResult = DBConnection.getConnectionInstance().executeScalar(loginQuery, loginParams);
                loginCount = loginResult != DBNull.Value ? Convert.ToInt32(loginResult) : 0;

                string documentQuery = "SELECT COUNT(*) FROM document WHERE UploadedBy = @UserId";
                SqlParameter[] documentParams = { new SqlParameter("@UserId", Globals.CurrentLoggedInUserID) };
                object documentResult = DBConnection.getConnectionInstance().executeScalar(documentQuery, documentParams);
                documentCount = documentResult != DBNull.Value ? Convert.ToInt32(documentResult) : 0;

                string eventQuery = "SELECT COUNT(*) FROM event_registration WHERE MemberID = @UserId";
                SqlParameter[] eventParams = { new SqlParameter("@UserId", Globals.CurrentLoggedInUserID) };
                object eventResult = DBConnection.getConnectionInstance().executeScalar(eventQuery, eventParams);
                eventRegistrationCount = eventResult != DBNull.Value ? Convert.ToInt32(eventResult) : 0;

                // Create and configure the chart
                Chart activityChart = new Chart
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.White
                };

                ChartArea chartArea = new ChartArea
                {
                    Name = "ActivityChartArea",
                    BackColor = Color.White,
                    AxisX =
            {
                Interval = 1,
                LineColor = Color.Black,
                LabelStyle = { Font = new Font("Segoe UI", 10), ForeColor = Color.Black }
            },
                    AxisY =
            {
                LineColor = Color.Black,
                LabelStyle = { Font = new Font("Segoe UI", 10), ForeColor = Color.Black }
            }
                };
                activityChart.ChartAreas.Add(chartArea);

                // Create a data series with dynamic chart type
                Series series = new Series
                {
                    Name = "ActivitySeries",
                    ChartArea = "ActivityChartArea",
                    IsValueShownAsLabel = true,
                    Color = Color.IndianRed,
                    BorderWidth = 2
                };

                // Set chart type dynamically
                switch (chartType)
                {
                    case "Bar":
                        series.ChartType = SeriesChartType.Bar;
                        break;
                    case "Pie":
                        series.ChartType = SeriesChartType.Pie;
                        break;
                    case "Line":
                        series.ChartType = SeriesChartType.Line;
                        break;
                    case "Area":
                        series.ChartType = SeriesChartType.Area;
                        break;
                    default: // Default to Column
                        series.ChartType = SeriesChartType.Column;
                        break;
                }

                // Add data points
                series.Points.AddXY("Logins", loginCount);
                series.Points.AddXY("Documents", documentCount);
                series.Points.AddXY("Event Reg.", eventRegistrationCount);

                // Add series to chart
                activityChart.Series.Add(series);

                // Add chart to panel
                chatPnl.Controls.Add(activityChart);

                // Force rendering
                activityChart.Refresh();
                chatPnl.Refresh();

                Console.WriteLine("Activity chart rendered successfully.");
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
