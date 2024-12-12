﻿using System;
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
                SELECT cb.Description, cb.Date, u.Username 
                FROM connections_board cb
                JOIN users u ON cb.UserID = u.ID
                WHERE cb.Type = 'Request'";
            DataTable requests = DBConnection.getConnectionInstance().executeQuery(query);

            if (requests.Rows.Count == 0)
            {
                Label noEventsLabel = new Label
                {
                    Text = "No events available.",
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                };
                contentPnl.Controls.Add(noEventsLabel);
                return;
            }

            foreach (DataRow row in requests.Rows)
            {
                AddRequestToPanel(Convert.ToInt32(row["ID"]), row["Description"].ToString(), Convert.ToDateTime(row["Date"]), row["Username"].ToString());
            }
        }

        private void LoadOffers()
        {
            contentPnl.Controls.Clear();

            string query = @"
                SELECT cb.Description, cb.Date, u.Username 
                FROM connections_board cb
                JOIN users u ON cb.UserID = u.ID
                WHERE cb.Type = 'Offer'";
            DataTable offers = DBConnection.getConnectionInstance().executeQuery(query);

            if (offers.Rows.Count == 0)
            {
                Label noEventsLabel = new Label
                {
                    Text = "No events available.",
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                };
                contentPnl.Controls.Add(noEventsLabel);
                return;
            }

            foreach (DataRow row in offers.Rows)
            {
                AddRequestToPanel(Convert.ToInt32(row["ID"]), row["Description"].ToString(), Convert.ToDateTime(row["Date"]), row["Username"].ToString());
            }
        }


        private void AddRequestToPanel(int reqOffId, string description, DateTime date, string username)
        {
            Panel requestPanel = new Panel
            {
                BackColor = Color.White,
                Size = new Size(975, 120),
                Margin = new Padding(5)
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
                Location = new Point(10, 90),
                Size = new Size(800, 20)
            };

            Label usernameLabel = new Label
            {
                Text = $"Posted by: {username}",
                Font = new Font("Segoe UI", 12, FontStyle.Italic),
                TextAlign = ContentAlignment.TopRight,
                AutoSize = true
            };

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
                    Location = new Point(850, 95),
                    Tag = reqOffId,
                    Cursor = Cursors.Hand,
                };

                removeButton.Click += (sender, e) =>
                {
                    var confirmResult = MessageBox.Show($"Are you sure you want to remove the event?",
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (confirmResult == DialogResult.Yes)
                    {
                        RemoveRequestOffer(reqOffId);
                    }
                };

                requestPanel.Controls.Add(removeButton);
            }


            // Align the username label to the right side of the panel
            usernameLabel.Location = new Point(
                requestPanel.Width - usernameLabel.PreferredWidth - 10,
                dateLabel.Top
            );

            // Add controls to the panel
            requestPanel.Controls.Add(descriptionLabel);
            requestPanel.Controls.Add(dateLabel);
            requestPanel.Controls.Add(usernameLabel);

            // Add the panel to the main container
            contentPnl.Controls.Add(requestPanel);
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

        private void RemoveRequestOffer(int reqOffId)
        {
            try
            {
                string deleteQuery = "DELETE FROM connection_board WHERE ID = @reqOffId";
                SqlParameter[] parameters = { new SqlParameter("@ID", reqOffId) };

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
