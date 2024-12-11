namespace togetherCulture
{
    partial class EventsScreen
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.createEventBtn = new System.Windows.Forms.Button();
            this.eventPnl = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 32);
            this.label1.TabIndex = 43;
            this.label1.Text = "Events";
            // 
            // createEventBtn
            // 
            this.createEventBtn.BackColor = System.Drawing.Color.IndianRed;
            this.createEventBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.createEventBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createEventBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createEventBtn.ForeColor = System.Drawing.Color.White;
            this.createEventBtn.Location = new System.Drawing.Point(815, 0);
            this.createEventBtn.Margin = new System.Windows.Forms.Padding(0, 0, 10, 5);
            this.createEventBtn.Name = "createEventBtn";
            this.createEventBtn.Size = new System.Drawing.Size(167, 46);
            this.createEventBtn.TabIndex = 45;
            this.createEventBtn.Text = "Create event";
            this.createEventBtn.UseVisualStyleBackColor = false;
            this.createEventBtn.Click += new System.EventHandler(this.createEventBtn_Click);
            // 
            // eventPnl
            // 
            this.eventPnl.Location = new System.Drawing.Point(0, 54);
            this.eventPnl.Name = "eventPnl";
            this.eventPnl.Size = new System.Drawing.Size(1006, 584);
            this.eventPnl.TabIndex = 46;
            // 
            // EventsScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.eventPnl);
            this.Controls.Add(this.createEventBtn);
            this.Controls.Add(this.label1);
            this.Name = "EventsScreen";
            this.Size = new System.Drawing.Size(1006, 638);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button createEventBtn;
        private System.Windows.Forms.Panel eventPnl;
    }
}
