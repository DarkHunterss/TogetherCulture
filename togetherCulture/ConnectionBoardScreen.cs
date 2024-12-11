using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace togetherCulture
{
    public partial class ConnectionBoardScreen : UserControl
    {
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
                ShowNoRecordsMessage("No requests available.");
                return;
            }

            foreach (DataRow row in requests.Rows)
            {
                AddRequestToPanel(row["Description"].ToString(), Convert.ToDateTime(row["Date"]), row["Username"].ToString());
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
                ShowNoRecordsMessage("No offers available.");
                return;
            }

            foreach (DataRow row in offers.Rows)
            {
                AddRequestToPanel(row["Description"].ToString(), Convert.ToDateTime(row["Date"]), row["Username"].ToString());
            }
        }


        private void ShowNoRecordsMessage(string message)
        {
            // Create a label for the "No Records" message
            Label noRecordsLabel = new Label
            {
                Text = message,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            // Clear the panel and add the label
            contentPnl.Controls.Clear();
            contentPnl.Controls.Add(noRecordsLabel);
        }


        private void AddRequestToPanel(string description, DateTime date, string username)
        {
            Panel requestPanel = new Panel
            {
                BackColor = Color.White,
                Size = new Size(995, 120),
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
                LoadRequests();
            }
            else if (activeButton == offersBtn)
            {
                LoadOffers();
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
            using (AddConnectionDialog addForm = new AddConnectionDialog(Globals.CurrentLoggedInUserID))
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    // Refresh the current view (requests/offers) after adding
                    if (requestsBtn.BackColor == Color.IndianRed)
                    {
                        LoadRequests();
                    }
                    else if (offersBtn.BackColor == Color.IndianRed)
                    {
                        LoadOffers();
                    }
                }
            }
        }
    }
}
