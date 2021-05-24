using System;
using System.Windows.Forms;

namespace Hangman_Game
{
    public partial class AdminLoginPage : Form
    {
        public AdminLoginPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Back button method. Go to Welcome Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backButton_Click(object sender, EventArgs e)
        {
            WelcomePage welcomePage = new WelcomePage();
            welcomePage.Show();
            this.Hide();
        }

        /// <summary>
        /// Login Button method. Go to Admin Controls Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginButton_Click(object sender, EventArgs e)
        {
            // if logging user is admin
            if (adminNameTextBox.Text == "admin" && adminPasswordTextBox.Text == "admin")
            {
                WelcomePage.currentUser = "admin";

                AdminControlsPage adminControlsPage = new AdminControlsPage();
                adminControlsPage.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı giriş.\nLütfen tekrar deneyiniz.");
            }

        }
    }
}
