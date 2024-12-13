using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace togetherCulture
{
    public partial class DocumentsScreen : UserControl
    {
        public DocumentsScreen()
        {
            InitializeComponent();
            LoadDocuments();
        }

        private void LoadDocuments()
        {
            mainPnl.Controls.Clear();

            try
            {
                string query = @"
                    SELECT d.ID, d.FileName, d.FilePath, d.Date, u.Username
                    FROM document d
                    JOIN users u ON d.UploadedBy = u.ID";

                DataTable documents = DBConnection.getConnectionInstance().executeQuery(query);

                if (documents.Rows.Count == 0)
                {
                    Label noDocumentsLabel = new Label
                    {
                        Text = "No documents found.",
                        Font = new Font("Segoe UI", 14, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill
                    };
                    mainPnl.Controls.Add(noDocumentsLabel);
                }
                else
                {
                    int yOffset = 0; // Vertical spacing
                    foreach (DataRow row in documents.Rows)
                    {
                        int documentId = Convert.ToInt32(row["ID"]);
                        string uploadedBy = row["Username"].ToString();
                        string fileName = row["FileName"].ToString();
                        string filePath = row["FilePath"].ToString();
                        DateTime date = Convert.ToDateTime(row["Date"]);

                        // Add each document panel to the main panel
                        Panel documentPanel = AddDocumentToPanel(documentId, uploadedBy, fileName, filePath, date, yOffset);
                        mainPnl.Controls.Add(documentPanel);

                        yOffset += documentPanel.Height + 10; // Adjust spacing between panels
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading documents: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel AddDocumentToPanel(int documentId, string uploadedBy, string fileName, string strFilePath, DateTime date, int yOffset)
        {
            Panel documentPanel = new Panel
            {
                BackColor = Color.White,
                Size = new Size(970, 100),
                Location = new Point(10, yOffset), // Adjusted dynamically based on yOffset
                Margin = new Padding(5),
                Padding = new Padding(10)
            };

            Label fileNameLabel = new Label
            {
                Text = $"File name: {fileName}",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(400, 25)
            };

            Label usernameLabel = new Label
            {
                Text = $"Uploaded By: {uploadedBy}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(10, 40),
                Size = new Size(400, 20)
            };

            Label dateLabel = new Label
            {
                Text = $"Date: {date.ToShortDateString()}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(10, 65),
                Size = new Size(200, 20)
            };

            Button downloadButton = new Button
            {
                Text = "Download",
                Font = new Font("Segoe UI Semibold", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                ForeColor = Color.White,
                BackColor = Color.IndianRed,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(110, 40),
                Location = new Point(850, 45),
                Tag = strFilePath// Tag to identify the document
            };

            downloadButton.Click += (sender, e) =>
            {
                try
                {
                    // Retrieve the file path from the button's Tag
                    string filePath = downloadButton.Tag.ToString();

                    // Open a SaveFileDialog to allow the user to choose the download location
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.FileName = Path.GetFileName(filePath); // Default file name
                        saveFileDialog.Filter = "All Files (*.*)|*.*"; // Allow all file types

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string destinationPath = saveFileDialog.FileName;

                            // Copy the file to the chosen location
                            File.Copy(filePath, destinationPath, overwrite: true);

                            MessageBox.Show("File downloaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error downloading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            documentPanel.Controls.Add(fileNameLabel);
            documentPanel.Controls.Add(usernameLabel);
            documentPanel.Controls.Add(dateLabel);
            documentPanel.Controls.Add(downloadButton);

            if (Globals.CurrentLoggedInUserRole == "Admin")
            {
                Button deleteDocumentButton = new Button
                {
                    Text = "Remove",
                    Font = new Font("Segoe UI Semibold", 10, FontStyle.Bold),
                    Cursor = Cursors.Hand,
                    ForeColor = Color.White,
                    BackColor = Color.DarkGray,
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(110, 40),
                    Location = new Point(735, 45),
                    Tag = documentId
                };

                deleteDocumentButton.Click += (sender, e) =>
                {
                    var confirmResult = MessageBox.Show($"Are you sure you want to delete this document?",
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (confirmResult == DialogResult.Yes)
                    {
                        RemoveDocument(documentId);
                    }
                };

                documentPanel.Controls.Add(deleteDocumentButton);
            }

            return documentPanel;
        }

        private void RemoveDocument(int documentId)
        {
            try
            {
                string deleteQuery = "DELETE FROM document WHERE ID = @DocumentID";
                SqlParameter[] parameters = { new SqlParameter("@DocumentID", documentId) };
                int rowsAffected = DBConnection.getConnectionInstance().executeNonQuery(deleteQuery, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Document removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDocuments(); // Refresh the document list
                }
                else
                {
                    MessageBox.Show("Failed to remove the document.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void addBtn_Click(object sender, EventArgs e)
        {
            if (Globals.CurrentLoggedInUserRole == "User") // Assuming "User" is the role for normal users
            {
                MessageBox.Show("You cannot add documents. Contact admin to do so.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Open the AddConnectionDialog for privileged users
            using (AddDocumentDialog addForm = new AddDocumentDialog(Globals.CurrentLoggedInUserID))
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    LoadDocuments();
                }
            }
        }

    }
}
