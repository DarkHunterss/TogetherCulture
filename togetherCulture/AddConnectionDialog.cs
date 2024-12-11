using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace togetherCulture
{
    public partial class AddConnectionDialog : Form
    {
        private int _userId;

        public AddConnectionDialog(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            // Validate input
            if (typeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(descriptionTextBox.Text))
            {
                MessageBox.Show("Please enter a description.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Insert into database
            try
            {
                string query = "INSERT INTO connections_board (UserID, Type, Description, Date, Status) " +
                               "VALUES (@UserID, @Type, @Description, @Date, @Status)";
                SqlParameter[] parameters = {
                    new SqlParameter("@UserID", _userId),
                    new SqlParameter("@Type", typeComboBox.SelectedItem.ToString()),
                    new SqlParameter("@Description", descriptionTextBox.Text.Trim()),
                    new SqlParameter("@Date", DateTime.Now),
                    new SqlParameter("@Status", "Active")
                };

                int rowsAffected = DBConnection.getConnectionInstance().executeNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add connection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
