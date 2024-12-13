using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace togetherCulture
{
    public partial class ConnectionBoardScreen : UserControl
    {
        private string currentTab = "requests";
        public ConnectionBoardScreen()
        {
            InitializeComponent();
            LoadRequests(); // Load requests by default
        }

        private void LoadRequests()
        {
            contentPnl.Controls.Clear();

            string query = @"
                SELECT cb.ID, cb.Description, cb.Date, u.Username 
                FROM connections_board cb
                JOIN users u ON cb.UserID = u.ID
                WHERE cb.Type = 'Request'";
            DataTable requests = DBConnection.getConnectionInstance().executeQuery(query);

            if (requests.Rows.Count == 0)
            {
                Label noEventsLabel = new Label
                {
                    Text = "No Requests available.",
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                };
                contentPnl.Controls.Add(noEventsLabel);
                return;
            }

            foreach (DataRow row in requests.Rows)
            {
                AddConnectionToPanel(Convert.ToInt32(row["ID"]), row["Description"].ToString(), Convert.ToDateTime(row["Date"]), row["Username"].ToString());
            }
        } 

        private void LoadOffers()
        {
            contentPnl.Controls.Clear();

            string query = @"
                SELECT cb.ID, cb.Description, cb.Date, u.Username 
                FROM connections_board cb
                JOIN users u ON cb.UserID = u.ID
                WHERE cb.Type = 'Offer'";
            DataTable offers = DBConnection.getConnectionInstance().executeQuery(query);

            if (offers.Rows.Count == 0)
            {
                Label noEventsLabel = new Label
                {
                    Text = "No Offers available.",
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                };
                contentPnl.Controls.Add(noEventsLabel);
                return;
            }

            foreach (DataRow row in offers.Rows)
            {
                AddConnectionToPanel(Convert.ToInt32(row["ID"]), row["Description"].ToString(), Convert.ToDateTime(row["Date"]), row["Username"].ToString());
            }
        }


        private void AddConnectionToPanel(int connectionId, string description, DateTime date, string username)
        {
            Panel connectionPanel = new Panel
            {
                BackColor = Color.White,
                Size = new Size(970, 150),
                Margin = new Padding(5),
                Padding = new Padding(10)
            };

            Label descriptionLabel = new Label
            {
                Text = description,
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                Location = new Point(10, 10),
                Size = new Size(800, 40)
            };

            Label dateLabel = new Label
            {
                Text = $"Posted on: {date.ToShortDateString()}",
                Font = new Font("Segoe UI", 12, FontStyle.Italic),
                Location = new Point(10, 110),
                Size = new Size(170, 30)
            };

            if (Globals.CurrentLoggedInUserRole == "Admin")
            {
                Label usernameLabel = new Label
                {
                    Text = $"by: {username}",
                    Font = new Font("Segoe UI", 12, FontStyle.Italic),
                    Location = new Point(190, 110),
                    Size = new Size(100, 30)
                };
                connectionPanel.Controls.Add(usernameLabel);
            }
            else
            {
                Label usernameLabel = new Label
                {
                    Text = $"by: {username}",
                    Font = new Font("Segoe UI", 12, FontStyle.Italic),
                    Location = new Point(650, 110),
                    Size = new Size(100, 30)
                };

                usernameLabel.Location = new Point(
                    connectionPanel.Width - usernameLabel.PreferredWidth - 10,
                    dateLabel.Top
                );

                connectionPanel.Controls.Add(usernameLabel);
            }

            // Add Remove button (only for admins)
            if (Globals.CurrentLoggedInUserRole == "Admin")
            {
                Button removeButton = new Button
                {
                    Text = "Remove",
                    Font = new Font("Segoe UI semibold", 12),
                    ForeColor = Color.White,
                    BackColor = Color.DarkGray,
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(110, 40),
                    Location = new Point(840, 90),
                    Tag = connectionId,
                    Cursor = Cursors.Hand,
                };


                removeButton.Click += (sender, e) =>
                {
                    var confirmResult = MessageBox.Show($"Are you sure you want to remove the connection?",
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (confirmResult == DialogResult.Yes)
                    {
                        RemoveConnection(connectionId);
                    }
                };

                connectionPanel.Controls.Add(removeButton);
            }


            // Add controls to the panel
            connectionPanel.Controls.Add(descriptionLabel);
            connectionPanel.Controls.Add(dateLabel);
            

            // Add the panel to the main container
            contentPnl.Controls.Add(connectionPanel);
        }

        private void SetActiveTab(Button activeButton)
        {
            // Reset colors for all buttons
            requestsBtn.BackColor = Color.DarkGray;
            offersBtn.BackColor = Color.DarkGray;

            // Set the active tab color to IndianRed
            activeButton.BackColor = Color.IndianRed;

            // Load the appropriate data for the active tab
            if (activeButton == requestsBtn)
            {
                currentTab = "requests";
                LoadRequests();
            }
            else if (activeButton == offersBtn)
            {
                currentTab = "offers";
                LoadOffers();
            }
        }

        public Button GetActiveTab()
        {
            return requestsBtn;
        }

        private void RemoveConnection(int connectionId)
        {
            try
            {
                string deleteQuery = "DELETE FROM connections_board WHERE ID = @ConnectionID";
                SqlParameter[] parameters = { new SqlParameter("@ConnectionID", connectionId) };

                int rowsAffected = DBConnection.getConnectionInstance().executeNonQuery(deleteQuery, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Event removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (currentTab == "requests")
                    {
                        LoadRequests();
                    }
                    else if (currentTab == "offers")
                    {
                        LoadOffers();
                    }
                }
                else
                {
                    MessageBox.Show("Failed to remove the event.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing the event: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void requestsBtn_Click(object sender, EventArgs e)
        {
            SetActiveTab(requestsBtn);
        }

        private void offersBtn_Click(object sender, EventArgs e)
        {
            SetActiveTab(offersBtn);
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (Globals.CurrentLoggedInUserRole == "User") // Assuming "User" is the role for normal users
            {
                MessageBox.Show("You cannot add connections. Buy a Membership first.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Open the AddConnectionDialog for privileged users
            using (AddConnectionDialog addForm = new AddConnectionDialog(Globals.CurrentLoggedInUserID))
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    // Refresh the current view (requests/offers) after adding
                    if (requestsBtn.BackColor == Color.IndianRed)
                        LoadRequests();
                    else
                        LoadOffers();
                }
            }
        }
    }
}
