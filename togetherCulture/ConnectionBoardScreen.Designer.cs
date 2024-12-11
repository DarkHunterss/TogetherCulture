namespace togetherCulture
{
    partial class ConnectionBoardScreen
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.mainPnl = new System.Windows.Forms.Panel();
            this.requestsBtn = new System.Windows.Forms.Button();
            this.offersBtn = new System.Windows.Forms.Button();
            this.addBtn = new System.Windows.Forms.Button();
            this.contentPnl = new System.Windows.Forms.FlowLayoutPanel();
            this.mainPnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 32);
            this.label1.TabIndex = 40;
            this.label1.Text = "Connection board";
            // 
            // mainPnl
            // 
            this.mainPnl.Controls.Add(this.addBtn);
            this.mainPnl.Controls.Add(this.offersBtn);
            this.mainPnl.Controls.Add(this.requestsBtn);
            this.mainPnl.Controls.Add(this.contentPnl);
            this.mainPnl.Location = new System.Drawing.Point(0, 49);
            this.mainPnl.Name = "mainPnl";
            this.mainPnl.Size = new System.Drawing.Size(1006, 589);
            this.mainPnl.TabIndex = 42;
            // 
            // requestsBtn
            // 
            this.requestsBtn.BackColor = System.Drawing.Color.IndianRed;
            this.requestsBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.requestsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.requestsBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.requestsBtn.ForeColor = System.Drawing.Color.White;
            this.requestsBtn.Location = new System.Drawing.Point(0, 3);
            this.requestsBtn.Name = "requestsBtn";
            this.requestsBtn.Size = new System.Drawing.Size(186, 40);
            this.requestsBtn.TabIndex = 0;
            this.requestsBtn.Text = "Requests";
            this.requestsBtn.UseVisualStyleBackColor = false;
            this.requestsBtn.Click += new System.EventHandler(this.requestsBtn_Click);
            // 
            // offersBtn
            // 
            this.offersBtn.BackColor = System.Drawing.Color.DarkGray;
            this.offersBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.offersBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.offersBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.offersBtn.ForeColor = System.Drawing.Color.White;
            this.offersBtn.Location = new System.Drawing.Point(192, 3);
            this.offersBtn.Name = "offersBtn";
            this.offersBtn.Size = new System.Drawing.Size(183, 40);
            this.offersBtn.TabIndex = 1;
            this.offersBtn.Text = "Offers";
            this.offersBtn.UseVisualStyleBackColor = false;
            this.offersBtn.Click += new System.EventHandler(this.offersBtn_Click);
            // 
            // addBtn
            // 
            this.addBtn.BackColor = System.Drawing.Color.Gray;
            this.addBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addBtn.ForeColor = System.Drawing.Color.White;
            this.addBtn.Location = new System.Drawing.Point(911, 3);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(95, 40);
            this.addBtn.TabIndex = 2;
            this.addBtn.Text = "+";
            this.addBtn.UseVisualStyleBackColor = false;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // contentPnl
            // 
            this.contentPnl.AutoScroll = true;
            this.contentPnl.BackColor = System.Drawing.SystemColors.ControlLight;
            this.contentPnl.Location = new System.Drawing.Point(0, 49);
            this.contentPnl.Name = "contentPnl";
            this.contentPnl.Size = new System.Drawing.Size(1006, 540);
            this.contentPnl.TabIndex = 3;
            // 
            // ConnectionBoardScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPnl);
            this.Controls.Add(this.label1);
            this.Name = "ConnectionBoardScreen";
            this.Size = new System.Drawing.Size(1006, 638);
            this.mainPnl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel mainPnl;
        private System.Windows.Forms.Button requestsBtn;
        private System.Windows.Forms.Button offersBtn;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.FlowLayoutPanel contentPnl;
    }
}
