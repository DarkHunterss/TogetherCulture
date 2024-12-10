using System;
using System.Windows.Forms;

namespace togetherCulture
{
    public partial class PaymentDialog : Form
    {
        public PaymentDialog()
        {
            InitializeComponent();
        }

        private void ButtonChooseAddress_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Choose Address functionality coming soon!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ButtonPay_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Payment processing functionality coming soon!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
