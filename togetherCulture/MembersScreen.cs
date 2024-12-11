using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace togetherCulture
{
    public partial class MembersScreen : UserControl
    {
        public MembersScreen()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            usersPanel.Controls.Clear();

            try
            {
                string query = @"
                    SELECT u.ID, u.Username, u.Email, r.Name
                    FROM users u
                    INNER JOIN role r ON u.RoleID = r.ID";

                DataTable users = DBConnection.getConnectionInstance().executeQuery(query);

                if (users.Rows.Count == 0)
                {
                    Label noUsersLabel = new Label
                    {
                        Text = "No users found.",
                        Font = new Font("Segoe UI", 14, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill
                    };
                    usersPanel.Controls.Add(noUsersLabel);
                }
                else
                {
                    foreach (DataRow row in users.Rows)
                    {
                        int userId = Convert.ToInt32(row["ID"]);
                        string username = row["Username"].ToString();
                        string email = row["Email"].ToString();
                        string role = row["Name"].ToString();

                        AddUserToPanel(userId, username, email, role);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddUserToPanel(int userId, string username, string email, string role)
        {
            Panel userPanel = new Panel
            {
                BackColor = Color.LightGray,
                Size = new Size(970, 100),
                Margin = new Padding(5),
                Padding = new Padding(10)
            };

            Label usernameLabel = new Label
            {
                Text = $"Username: {username}",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(400, 25)
            };

            Label emailLabel = new Label
            {
                Text = $"Email: {email}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(10, 40),
                Size = new Size(400, 20)
            };

            Label roleLabel = new Label
            {
                Text = $"Role: {role}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(10, 65),
                Size = new Size(200, 20)
            };

            userPanel.Controls.Add(usernameLabel);
            userPanel.Controls.Add(emailLabel);
            userPanel.Controls.Add(roleLabel);

            if (Globals.CurrentLoggedInUserRole == "Admin")
            {
                Button deleteUserButton = new Button
                {
                    Text = "Remove",
                    Font = new Font("Segoe UI semibold", 10, FontStyle.Bold),
                    Cursor = Cursors.Hand,
                    ForeColor = Color.White,
                    BackColor = Color.IndianRed,
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(100, 30),
                    Location = new Point(850, 35),
                    Tag = userId
                };

                deleteUserButton.Click += (sender, e) =>
                {
                    var confirmResult = MessageBox.Show($"Are you sure you want to remove {username}?",
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (confirmResult == DialogResult.Yes)
                    {
                        RemoveUser(userId);
                    }
                };

                userPanel.Controls.Add(deleteUserButton);
            }

            usersPanel.Controls.Add(userPanel);
        }

        private void RemoveUser(int userId)
        {
            try
            {
                // Check if the logged-in user is an admin
                if (Globals.CurrentLoggedInUserRole != "Admin")
                {
                    MessageBox.Show("You do not have permission to remove users.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if the user being removed is an admin
                string roleQuery = "SELECT RoleID FROM users WHERE ID = @UserID";
                SqlParameter[] parameters = { new SqlParameter("@UserID", userId) };
                object roleId = DBConnection.getConnectionInstance().executeScalar(roleQuery, parameters);

                if (roleId != null && Convert.ToInt32(roleId) == GetAdminRoleId())
                {
                    MessageBox.Show("Admins cannot remove other admins.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Proceed with the removal
                string deleteQuery = "DELETE FROM users WHERE ID = @UserID";
                int rowsAffected = DBConnection.getConnectionInstance().executeNonQuery(deleteQuery, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("User removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to remove the user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetAdminRoleId()
        {
            string query = "SELECT ID FROM role WHERE Name = 'Admin'";
            object result = DBConnection.getConnectionInstance().executeScalar(query);
            return result != null ? Convert.ToInt32(result) : -1;
        }

    }
}
