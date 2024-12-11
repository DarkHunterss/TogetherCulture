using System;
using System.Data;
using System.Data.SqlClient;
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
            LoadMemberships(Globals.CurrentLoggedInUserID);
        }

        private void backMembershipBtn_Click(object sender, EventArgs e)
        {
            _mainWindow.backMembershipBtn_Click();
        }

        private void LoadMemberships(int userId)
        {
            try
            {
                // Clear existing controls from the table layout
                tableLayoutPanel1.Controls.Clear();

                // Fetch the user's current membership ID
                string membershipQuery = "SELECT MembershipID FROM users WHERE ID = @UserId";
                SqlParameter[] userParams = { new SqlParameter("@UserId", userId) };
                object membershipIdObj = DBConnection.getConnectionInstance().executeScalar(membershipQuery, userParams);
                int currentMembershipId = membershipIdObj != DBNull.Value ? Convert.ToInt32(membershipIdObj) : 0;

                // Query to fetch memberships
                string membershipQueryAll = "SELECT ID, Name, Benefits, MonthlyFee FROM membership WHERE IsActive = 1";
                DataTable membershipTable = DBConnection.getConnectionInstance().executeQuery(membershipQueryAll);

                int recordCount = membershipTable.Rows.Count;

                if (recordCount == 0)
                {
                    Label noMembershipLabel = new Label
                    {
                        Text = "No memberships available.",
                        Font = new Font("Segoe UI", 14F, FontStyle.Bold),
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
                        int membershipId = Convert.ToInt32(row["ID"]);
                        string name = row["Name"].ToString();
                        string benefits = row["Benefits"].ToString();
                        decimal monthlyFee = Convert.ToDecimal(row["MonthlyFee"]);

                        bool isDisabled = membershipId <= currentMembershipId;

                        var membershipPanel = CreateMembershipPanel(name, benefits, monthlyFee, membershipId, userId, isDisabled);
                        tableLayoutPanel1.Controls.Add(membershipPanel, column % 3, column / 3);
                        column++;
                    }

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

        private Panel CreateMembershipPanel(string name, string benefits, decimal monthlyFee, int membershipId, int userId, bool isDisabled = false)
        {
            Panel panel = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(300, 420),
                Margin = new Padding(15),
                BackColor = isDisabled ? Color.LightGray : Color.White // Disabled panels are grayed out
            };

            Label nameLabel = new Label
            {
                Text = name,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40
            };

            Label feeLabel = new Label
            {
                Text = $"£{monthlyFee:0.00} / month",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 30
            };

            Label benefitsLabel = new Label
            {
                Text = FormatBenefits(benefits),
                Font = new Font("Segoe UI", 10F),
                TextAlign = ContentAlignment.TopCenter,
                Dock = DockStyle.Fill,
                Padding = new Padding(10, 5, 10, 5)
            };

            Button signUpButton = new Button
            {
                Text = "Sign up now",
                Font = new Font("Segoe UI Semibold", 12F, FontStyle.Regular),
                FlatStyle = FlatStyle.Flat,
                ForeColor = isDisabled ? Color.LightGray : Color.White,
                Dock = DockStyle.Bottom,
                Height = 40,
                Cursor = isDisabled ? Cursors.Default : Cursors.Hand,
                BackColor = isDisabled ? Color.DarkGray : Color.IndianRed,
                Enabled = !isDisabled // Disable button for completed memberships
            };

            signUpButton.Click += (sender, e) =>
            {
                using (PaymentDialog paymentDialog = new PaymentDialog(membershipId, userId))
                {
                    if (paymentDialog.ShowDialog() == DialogResult.OK)
                    {
                        // After payment, reload the panel with disabled status
                        LoadMemberships(userId);
                    }
                }
            };

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
