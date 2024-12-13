using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace togetherCulture
{
    public partial class EventsScreen : UserControl
    {
        public EventsScreen()
        {
            InitializeComponent();
            LoadEvents(); // Load events when the screen is initialized

            eventPnl.AutoScroll = true;
        }

        private void LoadEvents()
        {
            eventPnl.Controls.Clear();

            try
            {
                string query = "SELECT ID, Name, Description, Date, Location, AttendanceCount FROM event_list";
                DataTable events = DBConnection.getConnectionInstance().executeQuery(query);

                if (events.Rows.Count == 0)
                {
                    Label noEventsLabel = new Label
                    {
                        Text = "No events available.",
                        Font = new Font("Segoe UI", 14, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill
                    };
                    eventPnl.Controls.Add(noEventsLabel);
                }
                else
                {
                    foreach (DataRow row in events.Rows)
                    {
                        int eventId = Convert.ToInt32(row["ID"]);
                        string name = row["Name"].ToString();
                        string description = row["Description"].ToString();
                        DateTime date = Convert.ToDateTime(row["Date"]);
                        string location = row["Location"].ToString();

                        AddEventToPanel(eventId, name, description, date, location);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading events: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AddEventToPanel(int eventId, string name, string description, DateTime date, string location)
        {
            // Create a panel for the event
            Panel eventPanel = new Panel
            {
                BackColor = Color.White,
                Size = new Size(970, 150),
                Margin = new Padding(5),
                Padding = new Padding(10)
            };

            // Add event name
            Label nameLabel = new Label
            {
                Text = name,
                Font = new Font("Segoe UI semibold", 14),
                Location = new Point(10, 10),
                Size = new Size(600, 25)
            };

            // Add event description
            Label descriptionLabel = new Label
            {
                Text = description,
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                Location = new Point(10, 45),
                Size = new Size(600, 40)
            };

            // Add event date
            Label dateLabel = new Label
            {
                Text = $"Date: {date.ToShortDateString()}",
                Font = new Font("Segoe UI", 12, FontStyle.Italic),
                Location = new Point(830, 10),
                Size = new Size(200, 20)
            };

            // Add event location
            Label locationLabel = new Label
            {
                Text = $"Location: {location}",
                Font = new Font("Segoe UI", 12, FontStyle.Italic),
                Location = new Point(10, 110),
                Size = new Size(200, 20)
            };

            // Determine if the user is already registered for the event
            bool isRegistered = IsUserRegistered(eventId);

            // Add Register/Unregister button
            Button registerButton = new Button
            {
                Text = isRegistered ? "Unregister" : "Register",
                Font = new Font("Segoe UI semibold", 12),
                ForeColor = Color.White,
                BackColor = Color.IndianRed,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(110, 40),
                Location = new Point(850, 95),
                Cursor = Cursors.Hand,
                Tag = eventId
            };

            registerButton.Click += (sender, e) =>
            {
                if (registerButton.Text == "Register")
                {
                    RegisterForEvent(eventId);
                    registerButton.Text = "Unregister";
                }
                else
                {
                    UnregisterFromEvent(eventId);
                    registerButton.Text = "Register";
                }
            };

            eventPanel.Controls.Add(registerButton);

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
                    Location = new Point(730, 95),
                    Tag = eventId,
                    Cursor = Cursors.Hand,
                };

                removeButton.Click += (sender, e) =>
                {
                    var confirmResult = MessageBox.Show($"Are you sure you want to remove the event \"{name}\"?",
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (confirmResult == DialogResult.Yes)
                    {
                        RemoveEvent(eventId);
                    }
                };

                eventPanel.Controls.Add(removeButton);
            }

            // Add all labels to the event panel
            eventPanel.Controls.Add(nameLabel);
            eventPanel.Controls.Add(descriptionLabel);
            eventPanel.Controls.Add(dateLabel);
            eventPanel.Controls.Add(locationLabel);

            // Calculate Y position for the new panel based on the count of existing controls
            int currentY = eventPnl.Controls.Count * (eventPanel.Height + eventPanel.Margin.Vertical);

            // Set the location of the event panel
            eventPanel.Location = new Point(10, currentY);

            // Add the event panel to the main panel
            eventPnl.Controls.Add(eventPanel);
        }

        private bool IsUserRegistered(int eventId)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM event_registration WHERE MemberID = @MemberID AND EventID = @EventID";
                SqlParameter[] parameters = {
            new SqlParameter("@MemberID", Globals.CurrentLoggedInUserID),
            new SqlParameter("@EventID", eventId)
        };
                object result = DBConnection.getConnectionInstance().executeScalar(query, parameters);

                return result != DBNull.Value && Convert.ToInt32(result) > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking registration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void UnregisterFromEvent(int eventId)
        {
            try
            {
                string query = "DELETE FROM event_registration WHERE MemberID = @MemberID AND EventID = @EventID";
                SqlParameter[] parameters = {
                    new SqlParameter("@MemberID", Globals.CurrentLoggedInUserID),
                    new SqlParameter("@EventID", eventId)
                };

                int rowsAffected = DBConnection.getConnectionInstance().executeNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Successfully unregistered from the event.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to unregister from the event.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error unregistering from the event: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void RegisterForEvent(int eventId)
        {
            try
            {
                string query = @"
            INSERT INTO event_registration (MemberID, EventID, Date)
            VALUES (@MemberID, @EventID, @Date)";

                SqlParameter[] parameters = {
                    new SqlParameter("@MemberID", Globals.CurrentLoggedInUserID),
                    new SqlParameter("@EventID", eventId),
                    new SqlParameter("@Date", DateTime.Now)
                };

                int rowsAffected = DBConnection.getConnectionInstance().executeNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Successfully registered for the event!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to register for the event.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error registering for the event: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveEvent(int eventId)
        {
            try
            {
                string deleteQuery = "DELETE FROM event_list WHERE ID = @EventID";
                SqlParameter[] parameters = { new SqlParameter("@EventID", eventId) };

                int rowsAffected = DBConnection.getConnectionInstance().executeNonQuery(deleteQuery, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Event removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEvents(); // Reload events
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



        private void createEventBtn_Click(object sender, EventArgs e)
        {
            // Example for handling the create event button click
            if (Globals.CurrentLoggedInUserRole != "Admin")
            {
                MessageBox.Show("Only admins can create events.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (CreateEventDialog createEventDialog = new CreateEventDialog(Globals.CurrentLoggedInUserID))
            {
                if (createEventDialog.ShowDialog() == DialogResult.OK)
                {
                    // Reload events after creating a new event
                    LoadEvents();
                }
            }
        }
    }
}
