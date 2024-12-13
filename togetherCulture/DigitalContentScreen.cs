using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace togetherCulture
{
    public partial class DigitalContentScreen : UserControl
    {
        public DigitalContentScreen()
        {
            InitializeComponent();
            LoadDigitalContent();

            // Show add button only if the user is an Admin
            addBtn.Visible = Globals.CurrentLoggedInUserRole == "Admin";
        }

        private void LoadDigitalContent()
        {
            mainPnl.Controls.Clear();

            try
            {
                string query = @"
                    SELECT dc.ID, dc.Title, dc.Description, dc.ContentType, dc.FilePath, dc.UploadDate, u.Username AS UploadedBy
                    FROM digital_content dc
                    JOIN users u ON dc.CreatedBy = u.ID";

                DataTable digitalContent = DBConnection.getConnectionInstance().executeQuery(query);

                if (digitalContent.Rows.Count == 0)
                {
                    Label noContentLabel = new Label
                    {
                        Text = "No digital content available.",
                        Font = new Font("Segoe UI", 14, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill
                    };
                    mainPnl.Controls.Add(noContentLabel);
                }
                else
                {
                    foreach (DataRow row in digitalContent.Rows)
                    {
                        int contentId = Convert.ToInt32(row["ID"]);
                        string title = row["Title"].ToString();
                        string description = row["Description"].ToString();
                        string contentType = row["ContentType"].ToString();
                        string filePath = row["FilePath"].ToString();
                        string uploadedBy = row["UploadedBy"].ToString();
                        DateTime uploadDate = Convert.ToDateTime(row["UploadDate"]);

                        AddContentToPanel(contentId, title, description, contentType, filePath, uploadedBy, uploadDate);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading digital content: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddContentToPanel(int contentId, string title, string description, string contentType, string filePath, string uploadedBy, DateTime uploadDate)
        {
            Panel contentPanel = new Panel
            {
                BackColor = Color.White,
                Size = new Size(970, 150),
                Margin = new Padding(5),
                Padding = new Padding(10)
            };

            Label titleLabel = new Label
            {
                Text = $"Title: {title}",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(600, 25)
            };

            Label descriptionLabel = new Label
            {
                Text = $"Description: {description}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(10, 40),
                Size = new Size(600, 40)
            };

            Label contentTypeLabel = new Label
            {
                Text = $"Type: {contentType}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(10, 90),
                Size = new Size(200, 20)
            };

            Label uploadInfoLabel = new Label
            {
                Text = $"Uploaded by: {uploadedBy} | Date: {uploadDate.ToShortDateString()}",
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                Location = new Point(10, 120),
                Size = new Size(600, 20)
            };

            Button downloadButton = new Button
            {
                Text = "Download",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(110, 40),
                Cursor = Cursors.Hand,
                Location = new Point(850, 95),
                Tag = filePath // Attach file path to button for easy access
            };

            downloadButton.Click += (sender, e) =>
            {
                string file = downloadButton.Tag.ToString();
                DownloadContent(file);
            };

            contentPanel.Controls.Add(titleLabel);
            contentPanel.Controls.Add(descriptionLabel);
            contentPanel.Controls.Add(contentTypeLabel);
            contentPanel.Controls.Add(uploadInfoLabel);
            contentPanel.Controls.Add(downloadButton);

            // Only Admins can remove content
            if (Globals.CurrentLoggedInUserRole == "Admin")
            {
                Button removeButton = new Button
                {
                    Text = "Remove",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    BackColor = Color.DarkGray,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(110, 40),
                    Cursor = Cursors.Hand,
                    Location = new Point(730, 95),
                    Tag = contentId // Attach content ID for removal
                };

                removeButton.Click += (sender, e) =>
                {
                    var confirmResult = MessageBox.Show($"Are you sure you want to remove \"{title}\"?",
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (confirmResult == DialogResult.Yes)
                    {
                        RemoveContent(contentId);
                    }
                };


                contentPanel.Controls.Add(removeButton);
            }

            // Calculate Y position for the new panel based on the count of existing controls
            int currentY = mainPnl.Controls.Count * (contentPanel.Height + contentPanel.Margin.Vertical);

            // Set the location of the event panel
            contentPanel.Location = new Point(10, currentY);

            mainPnl.Controls.Add(contentPanel);
        }

        private void DownloadContent(string filePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath))
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        FileName = System.IO.Path.GetFileName(filePath),
                        Filter = "All Files|*.*"
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        System.IO.File.Copy(filePath, saveFileDialog.FileName, true);
                        MessageBox.Show("File downloaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("File not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveContent(int contentId)
        {
            try
            {
                string deleteQuery = "DELETE FROM digital_content WHERE ID = @ContentID";
                SqlParameter[] parameters = { new SqlParameter("@ContentID", contentId) };

                int rowsAffected = DBConnection.getConnectionInstance().executeNonQuery(deleteQuery, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Content removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDigitalContent(); // Refresh content
                }
                else
                {
                    MessageBox.Show("Failed to remove content.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing content: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (Globals.CurrentLoggedInUserRole != "Admin")
            {
                MessageBox.Show("Only admins can add digital content.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (AddContentDialog addContentDialog = new AddContentDialog())
            {
                if (addContentDialog.ShowDialog() == DialogResult.OK)
                {
                    LoadDigitalContent(); // Refresh content
                }
            }
        }
    }
}
