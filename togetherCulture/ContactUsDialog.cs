using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static togetherCulture.Utility;

namespace togetherCulture
{
    public partial class ContactUsDialog : Form
    {
        // Import SendMessage from user32.dll to set placeholder text (cue banner)
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, string lParam);

        private const uint EM_SETCUEBANNER = 0x1501;

        public ContactUsDialog()
        {
            InitializeComponent();

            // Disable resizing
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            // Show screen in the center
            StartPosition = FormStartPosition.CenterScreen;

            // Set placeholders for the text boxes
            SetPlaceholder(textBoxName, "Name");
            SetPlaceholder(textBoxEmail, "Email");
            SetPlaceholder(textBoxMessage, "Message");
        }

        private void SetPlaceholder(TextBox textBox, string placeholderText)
        {
            // Use Windows API to set the placeholder text (cue banner)
            SendMessage(textBox.Handle, EM_SETCUEBANNER, (IntPtr)1, placeholderText);
        }

        private void ButtonSubmit_Click(object sender, EventArgs e)
        {
            // Validate fields
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                Utility.ShowDialogMessage("Name is required.", "Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxEmail.Text))
            {
                Utility.ShowDialogMessage("Email is required.", "Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxMessage.Text))
            {
                Utility.ShowDialogMessage("Message is required.", "Error");
                return;
            }

            // Simulate sending the message
            Utility.ShowDialogMessage("Your message has been sent successfully!", "Success");

            // Close the dialog
            this.Close();
        }
    }
}
