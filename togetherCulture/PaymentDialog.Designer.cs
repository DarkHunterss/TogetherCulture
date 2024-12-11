namespace togetherCulture
{
    partial class PaymentDialog
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelMembershipDetails = new System.Windows.Forms.Label();
            this.textBoxCardholderName = new System.Windows.Forms.TextBox();
            this.textBoxCardNumber = new System.Windows.Forms.TextBox();
            this.paymentTypeTxtBox = new System.Windows.Forms.TextBox();
            this.textBoxCVV = new System.Windows.Forms.TextBox();
            this.labelCardholderName = new System.Windows.Forms.Label();
            this.labelCardNumber = new System.Windows.Forms.Label();
            this.labelExpiryDate = new System.Windows.Forms.Label();
            this.labelCVV = new System.Windows.Forms.Label();
            this.payButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(16, 20);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(656, 40);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Confirm Your Payment";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMembershipDetails
            // 
            this.labelMembershipDetails.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMembershipDetails.Location = new System.Drawing.Point(12, 80);
            this.labelMembershipDetails.Name = "labelMembershipDetails";
            this.labelMembershipDetails.Size = new System.Drawing.Size(660, 80);
            this.labelMembershipDetails.TabIndex = 1;
            this.labelMembershipDetails.Text = "Membership Details Go Here";
            // 
            // textBoxCardholderName
            // 
            this.textBoxCardholderName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCardholderName.Location = new System.Drawing.Point(250, 180);
            this.textBoxCardholderName.Name = "textBoxCardholderName";
            this.textBoxCardholderName.Size = new System.Drawing.Size(422, 29);
            this.textBoxCardholderName.TabIndex = 3;
            // 
            // textBoxCardNumber
            // 
            this.textBoxCardNumber.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCardNumber.Location = new System.Drawing.Point(250, 220);
            this.textBoxCardNumber.Name = "textBoxCardNumber";
            this.textBoxCardNumber.Size = new System.Drawing.Size(422, 29);
            this.textBoxCardNumber.TabIndex = 5;
            // 
            // paymentTypeTxtBox
            // 
            this.paymentTypeTxtBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paymentTypeTxtBox.Location = new System.Drawing.Point(250, 260);
            this.paymentTypeTxtBox.Name = "paymentTypeTxtBox";
            this.paymentTypeTxtBox.Size = new System.Drawing.Size(190, 29);
            this.paymentTypeTxtBox.TabIndex = 7;
            // 
            // textBoxCVV
            // 
            this.textBoxCVV.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCVV.Location = new System.Drawing.Point(250, 300);
            this.textBoxCVV.Name = "textBoxCVV";
            this.textBoxCVV.Size = new System.Drawing.Size(100, 29);
            this.textBoxCVV.TabIndex = 9;
            // 
            // labelCardholderName
            // 
            this.labelCardholderName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCardholderName.Location = new System.Drawing.Point(12, 180);
            this.labelCardholderName.Name = "labelCardholderName";
            this.labelCardholderName.Size = new System.Drawing.Size(208, 25);
            this.labelCardholderName.TabIndex = 2;
            this.labelCardholderName.Text = "Cardholder Name:";
            // 
            // labelCardNumber
            // 
            this.labelCardNumber.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCardNumber.Location = new System.Drawing.Point(12, 220);
            this.labelCardNumber.Name = "labelCardNumber";
            this.labelCardNumber.Size = new System.Drawing.Size(208, 25);
            this.labelCardNumber.TabIndex = 4;
            this.labelCardNumber.Text = "Card Number:";
            // 
            // labelExpiryDate
            // 
            this.labelExpiryDate.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExpiryDate.Location = new System.Drawing.Point(12, 260);
            this.labelExpiryDate.Name = "labelExpiryDate";
            this.labelExpiryDate.Size = new System.Drawing.Size(208, 25);
            this.labelExpiryDate.TabIndex = 6;
            this.labelExpiryDate.Text = "Card Type";
            // 
            // labelCVV
            // 
            this.labelCVV.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCVV.Location = new System.Drawing.Point(12, 300);
            this.labelCVV.Name = "labelCVV";
            this.labelCVV.Size = new System.Drawing.Size(208, 25);
            this.labelCVV.TabIndex = 8;
            this.labelCVV.Text = "CVV:";
            // 
            // payButton
            // 
            this.payButton.BackColor = System.Drawing.Color.IndianRed;
            this.payButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.payButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.payButton.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.payButton.ForeColor = System.Drawing.Color.White;
            this.payButton.Location = new System.Drawing.Point(346, 379);
            this.payButton.Name = "payButton";
            this.payButton.Size = new System.Drawing.Size(160, 40);
            this.payButton.TabIndex = 10;
            this.payButton.Text = "Pay now";
            this.payButton.UseVisualStyleBackColor = false;
            this.payButton.Click += new System.EventHandler(this.payButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.DarkGray;
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(512, 379);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(160, 40);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // PaymentDialog
            // 
            this.ClientSize = new System.Drawing.Size(684, 441);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelMembershipDetails);
            this.Controls.Add(this.labelCardholderName);
            this.Controls.Add(this.textBoxCardholderName);
            this.Controls.Add(this.labelCardNumber);
            this.Controls.Add(this.textBoxCardNumber);
            this.Controls.Add(this.labelExpiryDate);
            this.Controls.Add(this.paymentTypeTxtBox);
            this.Controls.Add(this.labelCVV);
            this.Controls.Add(this.textBoxCVV);
            this.Controls.Add(this.payButton);
            this.Controls.Add(this.cancelButton);
            this.Name = "PaymentDialog";
            this.Text = "Payment";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelMembershipDetails;
        private System.Windows.Forms.TextBox textBoxCardholderName;
        private System.Windows.Forms.TextBox textBoxCardNumber;
        private System.Windows.Forms.TextBox paymentTypeTxtBox;
        private System.Windows.Forms.TextBox textBoxCVV;
        private System.Windows.Forms.Label labelCardholderName;
        private System.Windows.Forms.Label labelCardNumber;
        private System.Windows.Forms.Label labelExpiryDate;
        private System.Windows.Forms.Label labelCVV;
        private System.Windows.Forms.Button payButton;
        private System.Windows.Forms.Button cancelButton;
    }
}
