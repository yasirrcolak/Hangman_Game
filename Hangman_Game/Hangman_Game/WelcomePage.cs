using System;
using System.Windows.Forms;

namespace Hangman_Game
{
    public partial class WelcomePage : Form
    {
        // this variable contains current user type.
        public static string currentUser = "player";

        public WelcomePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Play buton method. Go to Game Information Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playButton_Click(object sender, EventArgs e)
        {
            GameInformationPage gameInformationPage = new GameInformationPage();
            gameInformationPage.Show();
            this.Hide();
        }

        /// <summary>
        /// High scores button method. Go to High Scores Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void highScoresButton_Click(object sender, EventArgs e)
        {
            HighScoresPage highScoresPage = new HighScoresPage();
            highScoresPage.Show();
            this.Hide();
        }

        /// <summary>
        /// Admin Login buton method. Go to Admin Login Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void adminLoginButton_Click(object sender, EventArgs e)
        {
            AdminLoginPage adminLoginPage = new AdminLoginPage();
            adminLoginPage.Show();
            this.Hide();
        }

        /// <summary>
        /// Quit button method. Application exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quitButton_Click(object sender, EventArgs e)
        {
            // The user wants to exit the application. Close everything down.
            Application.Exit();
        }

    }
}
