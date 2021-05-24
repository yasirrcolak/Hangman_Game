using System;
using System.IO;
using System.Windows.Forms;

namespace Hangman_Game
{
    public partial class HighScoresPage : Form
    {
        // This string contains player name and player score database path.
        string scoreBoardTxtPath = @"..\..\scoreBoard.txt";
        // This string array contains player details.
        private string[] _playerDetails = File.ReadAllLines(@"..\..\scoreBoard.txt");
        // This string array contains a player details.
        private string[] _playerNamesAndScores;

        public HighScoresPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method prints all players and scores on the screen.
        /// </summary>
        private void PrintScores()
        {
            // if current user is admin.
            if (WelcomePage.currentUser == "admin")
            {
                resetbutton.Visible = true;
            }
            else
            {
                resetbutton.Visible = false;
            }

            // listview configurations.
            scoreBoardListView.View = View.Details;
            scoreBoardListView.GridLines = true;

            // add columns.
            scoreBoardListView.Columns.Add("Skor", 220);
            scoreBoardListView.Columns.Add("Oyuncu", 220);

            // add all players.
            for (int i = 0; i < _playerDetails.Length; i++)
            {
                _playerNamesAndScores = _playerDetails[i].Split('#');
                scoreBoardListView.Items.Add(_playerNamesAndScores[1]);
                scoreBoardListView.Items[i].SubItems.Add(_playerNamesAndScores[0]);
            }

            // sorting player with score.
            scoreBoardListView.Sorting = SortOrder.Descending;
        }

        /// <summary>
        /// High Scores Page Load method. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HighScoresPage_Load(object sender, EventArgs e)
        {
            PrintScores();
        }

        /// <summary>
        /// Reset button method. Delete all player details from database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetbutton_Click(object sender, EventArgs e)
        {
            File.Delete(scoreBoardTxtPath);
            FileStream filecreate = new FileStream(scoreBoardTxtPath, FileMode.OpenOrCreate, FileAccess.Write);
            filecreate.Close();
            scoreBoardListView.Items.Clear();
        }

        /// <summary>
        /// Back button mmethod. Go to Welcome Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backButton_Click(object sender, EventArgs e)
        {
            WelcomePage welcomePage = new WelcomePage();
            welcomePage.Show();
            this.Dispose();
        }

    }

}
