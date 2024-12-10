using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace togetherCulture
{
    public partial class MembershipScreen : UserControl
    {
        private MainWindow _mainWindow;

        public MembershipScreen(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            // Load memberships dynamically
            LoadMemberships();
        }

        private void backMembershipBtn_Click(object sender, EventArgs e)
        {
            _mainWindow.backMembershipBtn_Click();
        }

        private void LoadMemberships()
        {
            try
            {
                // Clear existing controls from the table layout
                tableLayoutPanel1.Controls.Clear();

                // Query to fetch the first three active memberships
                string query = "SELECT TOP 3 Name, Benefits, MonthlyFee FROM membership WHERE IsActive = 1";

                // Fetch data using DBConnection
                DataTable membershipTable = DBConnection.getConnectionInstance().executeQuery(query);

                int recordCount = membershipTable.Rows.Count;

                if (recordCount == 0)
                {
                    // Display a message if no memberships are available
                    Label noMembershipLabel = new Label
                    {
                        Text = "No membership available at the moment.",
                        Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill
                    };
                    tableLayoutPanel1.Controls.Add(noMembershipLabel, 0, 0);
                    tableLayoutPanel1.ColumnCount = 1;
                    tableLayoutPanel1.RowCount = 1;
                }
                else
                {
                    int column = 0;

                    foreach (DataRow row in membershipTable.Rows)
                    {
                        // Fetch data from the DataTable
                        string name = row["Name"].ToString();
                        string benefits = row["Benefits"].ToString();
                        decimal monthlyFee = Convert.ToDecimal(row["MonthlyFee"]);

                        // Create and add membership panel to the table layout
                        var membershipPanel = CreateMembershipPanel(name, benefits, monthlyFee);
                        tableLayoutPanel1.Controls.Add(membershipPanel, column % 3, column / 3);
                        column++;
                    }

                    // Adjust layout if memberships are available
                    AdjustTableLayout(recordCount);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading memberships: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AdjustTableLayout(int recordCount)
        {
            int columns = Math.Min(recordCount, 3); // Max 3 columns
            int rows = (int)Math.Ceiling((double)recordCount / columns);

            tableLayoutPanel1.ColumnCount = columns;
            tableLayoutPanel1.RowCount = rows;

            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();

            for (int i = 0; i < columns; i++)
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / columns));

            for (int i = 0; i < rows; i++)
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // Center the table layout panel
            tableLayoutPanel1.Anchor = AnchorStyles.None;
            tableLayoutPanel1.Location = new Point(
                (this.Width - tableLayoutPanel1.Width) / 2,
                (this.Height - tableLayoutPanel1.Height) / 2
            );
        }

        private Panel CreateMembershipPanel(string name, string benefits, decimal monthlyFee, string extraFee = "")
        {
            // Create a new panel
            Panel panel = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(300, 420),
                Margin = new Padding(15), // Adjusted for spacing between panels
                BackColor = Color.White // Set white background for panels
            };

            // Membership name
            Label nameLabel = new Label
            {
                Text = name,
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40 // Height adjusted for consistent spacing
            };

            // Membership fee
            Label feeLabel = new Label
            {
                Text = extraFee == string.Empty
                    ? $"£{monthlyFee:0.00} / month"
                    : $"£{monthlyFee:0.00} / month + {extraFee}",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 30 // Adjusted for better spacing
            };

            // Membership benefits
            Label benefitsLabel = new Label
            {
                Text = FormatBenefits(benefits), // Each benefit on a new line
                Font = new Font("Microsoft Sans Serif", 10F), // Larger font size for readability
                TextAlign = ContentAlignment.TopCenter, // Centered text
                Dock = DockStyle.Fill,
                Padding = new Padding(10, 5, 10, 5), // Padding for spacing
            };

            // Sign up button
            Button signUpButton = new Button
            {
                Text = "Sign up now",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                Dock = DockStyle.Bottom,
                Height = 40,
                Cursor = Cursors.Hand,
                BackColor = Color.LightGray, // Button background color
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.Black // Consistent text color
            };

            // Explicitly remove all border styling
            signUpButton.FlatAppearance.BorderSize = 0;
            signUpButton.FlatAppearance.MouseDownBackColor = Color.LightGray; // Match background
            signUpButton.FlatAppearance.MouseOverBackColor = Color.DarkGray; // Slight hover effect

            signUpButton.Click += (sender, e) =>
            {
                MessageBox.Show($"You have selected the {name} membership.", "Membership Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            // Add controls to the panel
            panel.Controls.Add(signUpButton);
            panel.Controls.Add(benefitsLabel);
            panel.Controls.Add(feeLabel);
            panel.Controls.Add(nameLabel);

            return panel;
        }


        private string FormatBenefits(string benefits)
        {
            // Split benefits by comma and add a new line for each benefit
            string[] benefitList = benefits.Split(',');
            return string.Join(Environment.NewLine + Environment.NewLine, benefitList.Select(b => b.Trim()));
        }


    }
}
