using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace togetherCulture
{
    public partial class UserPreferenceScreen : Form
    {
        private readonly List<string> interests = new List<string> { "Sharing", "Learning", "Caring", "Working", "Happening" };
        private List<ComboBox> rankingComboBoxes = new List<ComboBox>();

        public UserPreferenceScreen()
        {
            InitializeComponent();
            LoadInterestOptions();
        }

        private void LoadInterestOptions()
        {
            foreach (var interest in interests)
            {
                // Panel for each interest
                Panel interestPanel = new Panel
                {
                    Size = new System.Drawing.Size(300, 70),
                    Margin = new Padding(10),
                    BackColor = System.Drawing.Color.LightGray,
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
                };

                // Interest Label
                Label interestLabel = new Label
                {
                    Text = interest,
                    Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                    AutoSize = true,
                    Location = new System.Drawing.Point(10, 10)
                };

                // Ranking ComboBox
                ComboBox rankingComboBox = new ComboBox
                {
                    Font = new System.Drawing.Font("Segoe UI", 10F),
                    Location = new System.Drawing.Point(10, 40),
                    Size = new System.Drawing.Size(240, 25)
                };
                rankingComboBox.Items.AddRange(new string[] { "1", "2", "3" });
                rankingComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                rankingComboBoxes.Add(rankingComboBox);

                // Add controls to panel
                interestPanel.Controls.Add(interestLabel);
                interestPanel.Controls.Add(rankingComboBox);

                // Add panel to FlowLayoutPanel
                flowLayoutPanelInterests.Controls.Add(interestPanel);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate intension field
                if (string.IsNullOrEmpty(textBoxIntention.Text))
                {
                    MessageBox.Show("Please insert your intention.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                // Validate rankings
                var selectedRanks = rankingComboBoxes.Select(c => c.SelectedItem?.ToString()).ToList();
                if (selectedRanks.Contains(null))
                {
                    MessageBox.Show("Please rank all interests.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                // Save preferences to the database
                string interestString = string.Join(", ", interests.Zip(selectedRanks, (interest, rank) => $"{interest}:{rank}"));
                string intention = textBoxIntention.Text;

                string query = "INSERT INTO user_preference (UserID, Interest, Intention) VALUES (@UserID, @Interest, @Intention)";
                SqlParameter[] parameters = {
                    new SqlParameter("@UserID", Globals.CurrentLoggedInUserID),
                    new SqlParameter("@Interest", interestString),
                    new SqlParameter("@Intention", intention)
                };

                DBConnection.getConnectionInstance().executeNonQuery(query, parameters);

                // Set DialogResult to OK and close the UserPreferenceScreen
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving preferences: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
