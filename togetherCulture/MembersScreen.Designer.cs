namespace togetherCulture
{
    partial class MembersScreen
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
            this.label1 = new System.Windows.Forms.Label();
            this.usersPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Members";
            // 
            // usersPanel
            // 
            this.usersPanel.AutoScroll = true;
            this.usersPanel.Location = new System.Drawing.Point(0, 50);
            this.usersPanel.Name = "usersPanel";
            this.usersPanel.Size = new System.Drawing.Size(991, 600);
            this.usersPanel.TabIndex = 1;
            // 
            // MembersScreen
            // 
            this.Controls.Add(this.usersPanel);
            this.Controls.Add(this.label1);
            this.Name = "MembersScreen";
            this.Size = new System.Drawing.Size(991, 649);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel usersPanel;
    }
}
