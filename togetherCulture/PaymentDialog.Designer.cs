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
            this.labelOrderSummary = new System.Windows.Forms.Label();
            this.labelSubtotal = new System.Windows.Forms.Label();
            this.labelTotal = new System.Windows.Forms.Label();
            this.labelShippingAddress = new System.Windows.Forms.Label();
            this.buttonChooseAddress = new System.Windows.Forms.Button();
            this.labelPayment = new System.Windows.Forms.Label();
            this.labelPaymentInfo = new System.Windows.Forms.Label();
            this.labelContactInfo = new System.Windows.Forms.Label();
            this.buttonPay = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelOrderSummary
            // 
            this.labelOrderSummary.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelOrderSummary.Location = new System.Drawing.Point(20, 20);
            this.labelOrderSummary.Name = "labelOrderSummary";
            this.labelOrderSummary.Size = new System.Drawing.Size(660, 30);
            this.labelOrderSummary.TabIndex = 0;
            this.labelOrderSummary.Text = "Order Summary";
            // 
            // labelSubtotal
            // 
            this.labelSubtotal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelSubtotal.Location = new System.Drawing.Point(20, 60);
            this.labelSubtotal.Name = "labelSubtotal";
            this.labelSubtotal.Size = new System.Drawing.Size(200, 20);
            this.labelSubtotal.TabIndex = 1;
            this.labelSubtotal.Text = "Subtotal: $12.99";
            // 
            // labelTotal
            // 
            this.labelTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelTotal.Location = new System.Drawing.Point(20, 90);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(200, 20);
            this.labelTotal.TabIndex = 2;
            this.labelTotal.Text = "Total: $12.99";
            // 
            // labelShippingAddress
            // 
            this.labelShippingAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelShippingAddress.Location = new System.Drawing.Point(20, 130);
            this.labelShippingAddress.Name = "labelShippingAddress";
            this.labelShippingAddress.Size = new System.Drawing.Size(200, 20);
            this.labelShippingAddress.TabIndex = 3;
            this.labelShippingAddress.Text = "Shipping Address:";
            // 
            // buttonChooseAddress
            // 
            this.buttonChooseAddress.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonChooseAddress.Location = new System.Drawing.Point(226, 124);
            this.buttonChooseAddress.Name = "buttonChooseAddress";
            this.buttonChooseAddress.Size = new System.Drawing.Size(100, 30);
            this.buttonChooseAddress.TabIndex = 4;
            this.buttonChooseAddress.Text = "Choose";
            this.buttonChooseAddress.UseVisualStyleBackColor = true;
            this.buttonChooseAddress.Click += new System.EventHandler(this.ButtonChooseAddress_Click);
            // 
            // labelPayment
            // 
            this.labelPayment.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelPayment.Location = new System.Drawing.Point(20, 180);
            this.labelPayment.Name = "labelPayment";
            this.labelPayment.Size = new System.Drawing.Size(200, 20);
            this.labelPayment.TabIndex = 5;
            this.labelPayment.Text = "Payment:";
            // 
            // labelPaymentInfo
            // 
            this.labelPaymentInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelPaymentInfo.Location = new System.Drawing.Point(160, 180);
            this.labelPaymentInfo.Name = "labelPaymentInfo";
            this.labelPaymentInfo.Size = new System.Drawing.Size(200, 20);
            this.labelPaymentInfo.TabIndex = 6;
            this.labelPaymentInfo.Text = "MasterCard **** 1234";
            // 
            // labelContactInfo
            // 
            this.labelContactInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelContactInfo.Location = new System.Drawing.Point(20, 230);
            this.labelContactInfo.Name = "labelContactInfo";
            this.labelContactInfo.Size = new System.Drawing.Size(200, 20);
            this.labelContactInfo.TabIndex = 7;
            this.labelContactInfo.Text = "Contact Info: john@example.com";
            // 
            // buttonPay
            // 
            this.buttonPay.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.buttonPay.Location = new System.Drawing.Point(400, 400);
            this.buttonPay.Name = "buttonPay";
            this.buttonPay.Size = new System.Drawing.Size(120, 40);
            this.buttonPay.TabIndex = 8;
            this.buttonPay.Text = "Pay";
            this.buttonPay.UseVisualStyleBackColor = true;
            this.buttonPay.Click += new System.EventHandler(this.ButtonPay_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.buttonCancel.Location = new System.Drawing.Point(550, 400);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(120, 40);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // PaymentDialog
            // 
            this.ClientSize = new System.Drawing.Size(700, 480);
            this.Controls.Add(this.labelOrderSummary);
            this.Controls.Add(this.labelSubtotal);
            this.Controls.Add(this.labelTotal);
            this.Controls.Add(this.labelShippingAddress);
            this.Controls.Add(this.buttonChooseAddress);
            this.Controls.Add(this.labelPayment);
            this.Controls.Add(this.labelPaymentInfo);
            this.Controls.Add(this.labelContactInfo);
            this.Controls.Add(this.buttonPay);
            this.Controls.Add(this.buttonCancel);
            this.Name = "PaymentDialog";
            this.Text = "Payment Dialog";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelOrderSummary;
        private System.Windows.Forms.Label labelSubtotal;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Label labelShippingAddress;
        private System.Windows.Forms.Button buttonChooseAddress;
        private System.Windows.Forms.Label labelPayment;
        private System.Windows.Forms.Label labelPaymentInfo;
        private System.Windows.Forms.Label labelContactInfo;
        private System.Windows.Forms.Button buttonPay;
        private System.Windows.Forms.Button buttonCancel;
    }
}
