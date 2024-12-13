using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace togetherCulture
{
    public partial class AddDocumentDialog : Form
    {
        private string _filePath;

        public AddDocumentDialog(int uploadedBy)
        {
            InitializeComponent();
            UploadedBy = uploadedBy;
        }

        private int UploadedBy { get; }

        private void uploadBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select a Document"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _filePath = openFileDialog.FileName;
                filePathLabel.Text = Path.GetFileName(_filePath);
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(fileNameTxtBox.Text) || string.IsNullOrEmpty(_filePath))
            {
                MessageBox.Show("Please provide a file name and upload a document.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = @"
                    INSERT INTO document (FileName, FilePath, Date, UploadedBy)
                    VALUES (@FileName, @FilePath, @Date, @UploadedBy)";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@FileName", fileNameTxtBox.Text.Trim()),
                    new SqlParameter("@FilePath", _filePath),
                    new SqlParameter("@Date", DateTime.Now),
                    new SqlParameter("@UploadedBy", UploadedBy)
                };

                int rowsAffected = DBConnection.getConnectionInstance().executeNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Document added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add the document.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void filePathLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
