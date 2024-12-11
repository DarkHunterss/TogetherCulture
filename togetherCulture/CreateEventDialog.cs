using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace togetherCulture
{
    public partial class CreateEventDialog : Form
    {
        private int _createdByUserId;

        public CreateEventDialog(int createdByUserId)
        {
            InitializeComponent();
            _createdByUserId = createdByUserId;
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text) || string.IsNullOrWhiteSpace(locationTextBox.Text))
            {
                MessageBox.Show("Please fill out all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = "INSERT INTO event_list (Name, Description, Date, Location, CreatedBy) " +
                               "VALUES (@Name, @Description, @Date, @Location, @CreatedBy)";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@Name", nameTextBox.Text.Trim()),
                    new SqlParameter("@Description", descriptionTextBox.Text.Trim()),
                    new SqlParameter("@Date", datePicker.Value),
                    new SqlParameter("@Location", locationTextBox.Text.Trim()),
                    new SqlParameter("@CreatedBy", _createdByUserId)
                };

                DBConnection.getConnectionInstance().executeNonQuery(query, parameters);
                MessageBox.Show("Event created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating event: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void CreateEventDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
