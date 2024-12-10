using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static togetherCulture.Utility;


namespace togetherCulture
{
    public partial class ContactUsDialog : Form
    {
        public string UserName { get; private set; }
        public string UserEmail { get; private set; }
        public string UserMessage { get; private set; }

        public ContactUsDialog()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void InitializeComponent()
        {
            this.labelName = new Label();
            this.textBoxName = new TextBox();
            this.labelEmail = new Label();
            this.textBoxEmail = new TextBox();
            this.labelMessage = new Label();
            this.textBoxMessage = new TextBox();
            this.buttonSubmit = new Button();
            this.buttonCancel = new Button();

            this.SuspendLayout();

            // Label for Name
            this.labelName.AutoSize = true;
            this.labelName.Text = "Name:";
            this.labelName.Location = new System.Drawing.Point(20, 20);

            // TextBox for Name
            this.textBoxName.Location = new System.Drawing.Point(80, 20);
            this.textBoxName.Width = 200;

            // Label for Email
            this.labelEmail.AutoSize = true;
            this.labelEmail.Text = "Email:";
            this.labelEmail.Location = new System.Drawing.Point(20, 60);

            // TextBox for Email
            this.textBoxEmail.Location = new System.Drawing.Point(80, 60);
            this.textBoxEmail.Width = 200;

            // Label for Message
            this.labelMessage.AutoSize = true;
            this.labelMessage.Text = "Message:";
            this.labelMessage.Location = new System.Drawing.Point(20, 100);

            // TextBox for Message
            this.textBoxMessage.Location = new System.Drawing.Point(80, 100);
            this.textBoxMessage.Width = 200;
            this.textBoxMessage.Height = 100;
            this.textBoxMessage.Multiline = true;

            // Submit Button
            this.buttonSubmit.Text = "Submit";
            this.buttonSubmit.Location = new System.Drawing.Point(80, 220);
            this.buttonSubmit.Click += new EventHandler(ButtonSubmit_Click);

            // Cancel Button
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Location = new System.Drawing.Point(180, 220);
            this.buttonCancel.Click += new EventHandler(ButtonCancel_Click);

            // Add controls to the form
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.textBoxEmail);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.buttonCancel);

            // Set Dialog properties
            this.ClientSize = new System.Drawing.Size(320, 280);
            this.Text = "Contact Us";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.AcceptButton = this.buttonSubmit;
            this.CancelButton = this.buttonCancel;
            this.ResumeLayout(false);
        }

        private void ButtonSubmit_Click(object sender, EventArgs e)
        {
            // Retrieve input values
            UserName = textBoxName.Text;
            UserEmail = textBoxEmail.Text;
            UserMessage = textBoxMessage.Text;

            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(UserEmail) || string.IsNullOrWhiteSpace(UserMessage))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsValidEmail(UserEmail))
            {
                ShowDialogMessage("Invalid email address.", "Error");
                return;
            }

            // Close dialog with OK result
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            // Close dialog with Cancel result
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Controls
        private Label labelName;
        private TextBox textBoxName;
        private Label labelEmail;
        private TextBox textBoxEmail;
        private Label labelMessage;
        private TextBox textBoxMessage;
        private Button buttonSubmit;
        private Button buttonCancel;
    }
}
