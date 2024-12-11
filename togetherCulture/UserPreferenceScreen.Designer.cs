namespace togetherCulture
{
    partial class UserPreferenceScreen
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

        private void InitializeComponent()
        {
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelIntention = new System.Windows.Forms.Label();
            this.textBoxIntention = new System.Windows.Forms.TextBox();
            this.flowLayoutPanelInterests = new System.Windows.Forms.FlowLayoutPanel();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitle.Location = new System.Drawing.Point(20, 20);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(226, 30);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Set Your Preferences";
            // 
            // labelIntention
            // 
            this.labelIntention.AutoSize = true;
            this.labelIntention.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.labelIntention.Location = new System.Drawing.Point(20, 70);
            this.labelIntention.Name = "labelIntention";
            this.labelIntention.Size = new System.Drawing.Size(75, 21);
            this.labelIntention.TabIndex = 1;
            this.labelIntention.Text = "Intention:";
            // 
            // textBoxIntention
            // 
            this.textBoxIntention.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBoxIntention.Location = new System.Drawing.Point(120, 70);
            this.textBoxIntention.Name = "textBoxIntention";
            this.textBoxIntention.Size = new System.Drawing.Size(560, 25);
            this.textBoxIntention.TabIndex = 2;
            // 
            // flowLayoutPanelInterests
            // 
            this.flowLayoutPanelInterests.AutoScroll = true;
            this.flowLayoutPanelInterests.Location = new System.Drawing.Point(20, 120);
            this.flowLayoutPanelInterests.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelInterests.Name = "flowLayoutPanelInterests";
            this.flowLayoutPanelInterests.Size = new System.Drawing.Size(660, 276);
            this.flowLayoutPanelInterests.TabIndex = 3;
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.Color.IndianRed;
            this.saveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.ForeColor = System.Drawing.Color.White;
            this.saveButton.Location = new System.Drawing.Point(250, 428);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(216, 40);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save Preferences";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // UserPreferenceScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 480);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelIntention);
            this.Controls.Add(this.textBoxIntention);
            this.Controls.Add(this.flowLayoutPanelInterests);
            this.Controls.Add(this.saveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "UserPreferenceScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Preferences";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelIntention;
        private System.Windows.Forms.TextBox textBoxIntention;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelInterests;
        private System.Windows.Forms.Button saveButton;
    }
}
