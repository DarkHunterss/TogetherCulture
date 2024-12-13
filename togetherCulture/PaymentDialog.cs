using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static togetherCulture.Utility;

namespace togetherCulture
{
    public partial class PaymentDialog : Form
    {
        private int _membershipId;
        private int _userId;
        private decimal _paymentAmount; // Changed type to decimal for proper calculation
        private DashboardScreen _dashboardScreen;


        public PaymentDialog(int membershipId, int userId, DashboardScreen dashboardScreen)  
        {
            InitializeComponent();
            _membershipId = membershipId;
            _userId = userId;


            // Disable resizing
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            // Show screen in the center
            StartPosition = FormStartPosition.CenterScreen;

            LoadMembershipDetails();
            _dashboardScreen = dashboardScreen;
        }

        private void LoadMembershipDetails()
        {
            try
            {
                // Query to fetch membership details
                string query = "SELECT Name, MonthlyFee, Benefits FROM membership WHERE ID = @MembershipId";
                SqlParameter[] parameters = { new SqlParameter("@MembershipId", _membershipId) };
                DataTable membershipTable = DBConnection.getConnectionInstance().executeQuery(query, parameters);

                if (membershipTable.Rows.Count > 0)
                {
                    DataRow row = membershipTable.Rows[0];
                    string name = row["Name"].ToString();
                    _paymentAmount = Convert.ToDecimal(row["MonthlyFee"]); // Set payment amount
                    string benefits = row["Benefits"].ToString();

                    labelMembershipDetails.Text = $"Membership: {name}\n\n" +
                                                  $"Price: £{_paymentAmount:0.00} / month\n\n" +
                                                  $"Benefits:\n{FormatBenefits(benefits)}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading membership details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string FormatBenefits(string benefits)
        {
            string[] benefitList = benefits.Split(',');
            return string.Join("\n", benefitList.Select(b => $"- {b.Trim()}"));
        }

        private void payButton_Click(object sender, EventArgs e)
        {
            // Validate payment fields
            if (string.IsNullOrWhiteSpace(textBoxCardholderName.Text))
            {
                ShowDialogMessage("Please enter the cardholder's name.", "Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxCardNumber.Text) || textBoxCardNumber.Text.Length != 16 || !long.TryParse(textBoxCardNumber.Text, out _))
            {
                ShowDialogMessage("Please enter a valid 16-digit card number.", "Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(paymentTypeTxtBox.Text))
            {
                ShowDialogMessage("Please enter a valid card type.", "Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxCVV.Text) || textBoxCVV.Text.Length != 3 || !int.TryParse(textBoxCVV.Text, out _))
            {
                ShowDialogMessage("Please enter a valid 3-digit CVV.", "Error");
                return;
            }

            // Process payment
            try
            {
                // Update user's membership
                string updateQuery = "UPDATE users SET MembershipID = @MembershipID WHERE ID = @UserId";
                SqlParameter[] updateParams = {
                    new SqlParameter("@MembershipID", _membershipId),
                    new SqlParameter("@UserId", _userId)
                };

                var userManager = new UserManager();

                if (Globals.CurrentLoggedInUserRole != "Admin")
                {
                    userManager.UpdateUserRoleToMember(Globals.CurrentLoggedInUserID);
                }
                
                int rowsAffected = DBConnection.getConnectionInstance().executeNonQuery(updateQuery, updateParams);

                if (rowsAffected > 0)
                {
                    // Insert payment record
                    string insertPaymentQuery = "INSERT INTO payment (MemberID, Amount, PaymentDate, PaymentType) VALUES (@MemberID, @Amount, @PaymentDate, @PaymentType)";
                    SqlParameter[] paymentParams = {
                        new SqlParameter("@MemberID", _userId),
                        new SqlParameter("@Amount", _paymentAmount),
                        new SqlParameter("@PaymentDate", DateTime.Now),
                        new SqlParameter("@PaymentType", paymentTypeTxtBox.Text.Trim())
                    };

                    DBConnection.getConnectionInstance().executeNonQuery(insertPaymentQuery, paymentParams);

                    // Refresh the benefits overview when the payment is completed
                    _dashboardScreen.LoadBenefitsOverview();

                    // Notify user
                    ShowDialogMessage("Payment successful! Membership updated.", "Success");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    ShowDialogMessage("Something went wrong. Payment declined.", "Error");
                }
            }
            catch (Exception ex)
            {
                ShowDialogMessage("Error processing payment: " + ex.Message, "Error");
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


    }
}
