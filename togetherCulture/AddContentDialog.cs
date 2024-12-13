using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace togetherCulture
{
    public partial class AddContentDialog : Form
    {
        public AddContentDialog()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select a file";
                openFileDialog.Filter = "All Files|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePathTextBox.Text = openFileDialog.FileName;
                }
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                string title = titleTextBox.Text.Trim();
                string description = descriptionTextBox.Text.Trim();
                string contentType = contentTypeComboBox.SelectedItem?.ToString();
                string filePath = filePathTextBox.Text.Trim();

                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(contentType) || string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("The selected file does not exist.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string savePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DigitalContent", Path.GetFileName(filePath));
                if (!Directory.Exists(Path.GetDirectoryName(savePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                }

                File.Copy(filePath, savePath, true);

                string query = @"
                    INSERT INTO digital_content (Title, Description, ContentType, FilePath, UploadDate, CreatedBy)
                    VALUES (@Title, @Description, @ContentType, @FilePath, @UploadDate, @CreatedBy)";

                SqlParameter[] parameters = {
                    new SqlParameter("@Title", title),
                    new SqlParameter("@Description", description),
                    new SqlParameter("@ContentType", contentType),
                    new SqlParameter("@FilePath", savePath),
                    new SqlParameter("@UploadDate", DateTime.Now),
                    new SqlParameter("@CreatedBy", Globals.CurrentLoggedInUserID)
                };

                int rowsAffected = DBConnection.getConnectionInstance().executeNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Content added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add content.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding content: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void contentTypeLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
