﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace togetherCulture
{
    public partial class MainWindow : Form
    {
        private ScreenController screenController;

        public MainWindow()
        {
            InitializeComponent();


            // Disable resizing
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            // Show screen in the center
            StartPosition = FormStartPosition.CenterScreen;

            // Initialize the ScreenController with the main panel
            screenController = new ScreenController(mainPanel);

            // Create and add UserControls to the ScreenController
            var dashboardScreen = new DashboardScreen();
            var connectionBoardScreen = new ConnectionBoardScreen();
            var documentsScreen = new DocumentsScreen();
            var digitalContentScreen = new DigitalContentScreen();
            var membersScreen = new MembersScreen();
            var eventsScreen = new EventsScreen();

            screenController.AddScreen("Dashboard", dashboardScreen);
            screenController.AddScreen("Connection board", connectionBoardScreen);
            screenController.AddScreen("Documents", documentsScreen);
            screenController.AddScreen("Digital content", digitalContentScreen);
            screenController.AddScreen("Members", membersScreen);
            screenController.AddScreen("Events", eventsScreen);


            // Show the default screen
            screenController.ShowScreen("Dashboard");
            HighlightSelectedTab(dashboardLbl);



        }

        private void HighlightSelectedTab(Label selectedLabel)
        {
            // Reset the background color of all buttons
            foreach (Control control in sideMenuPanel.Controls)
            {
                if (control is Label)
                {
                    control.BackColor = Color.Transparent; // Default tab color
                }
            }

            // Highlight the selected button
            selectedLabel.BackColor = Color.IndianRed; // Your chosen highlight color
        }

        private void ShowContactUsDialog()
        {
            using (ContactUsDialog contactDialog = new ContactUsDialog())
            {
                if (contactDialog.ShowDialog() == DialogResult.OK)
                {
                    string name = contactDialog.UserName;
                    string email = contactDialog.UserEmail;
                    string message = contactDialog.UserMessage;

                    // Handle submitted data
                    MessageBox.Show($"Thank you, {name}! Your message has been received.", "Confirmation");
                }
                else
                {
                    MessageBox.Show("Operation canceled.", "Canceled");
                }
            }
        }

        private void dashboardLbl_Click(object sender, EventArgs e)
        {
            Text = "Dashboard";
            screenController.ShowScreen("Dashboard");
            HighlightSelectedTab(dashboardLbl);
        }

        private void ConnectionBoardLbl_Click(object sender, EventArgs e)
        {
            Text = "Connnection board";
            screenController.ShowScreen("Connection board");
            HighlightSelectedTab(connectionBoardLbl);
        }

        private void documentLbl_Click(object sender, EventArgs e)
        {
            Text = "Documents";
            screenController.ShowScreen("Documents");
            HighlightSelectedTab(documentLbl);
        }

        private void digitalContentLbl_Click(object sender, EventArgs e)
        {
            Text = "Digital content";
            screenController.ShowScreen("Digital content");
            HighlightSelectedTab(digitalContentLbl);
        }

        private void membersLbl_Click(object sender, EventArgs e)
        {
            Text = "Members";
            screenController.ShowScreen("Members");
            HighlightSelectedTab(membersLbl);
        }

        private void eventsLbl_Click(object sender, EventArgs e)
        {
            Text = "Events";
            screenController.ShowScreen("Events");
            HighlightSelectedTab(eventsLbl);
        }

        private void contactUsBtn_Click(object sender, EventArgs e)
        {
            ShowContactUsDialog();
        }
    }
}
