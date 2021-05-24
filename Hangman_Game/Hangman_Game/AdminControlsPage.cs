using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Hangman_Game
{
    public partial class AdminControlsPage : Form
    {
        // This string contains words database path.
        string wordsTxtPath = @"..\..\words.txt";
        // This string array contains words.
        private string[] _words = File.ReadAllLines(@"..\..\words.txt");

        public AdminControlsPage()
        {
            InitializeComponent();
        }

        #region Private methods.

        /// <summary>
        /// Reset to listview.
        /// </summary>
        private void ResetToListView()
        {
            // clear to list view.
            wordsListView.Items.Clear();

            // update to words array.
            _words = File.ReadAllLines(@"..\..\words.txt");

            // check to all words.
            for (int i = 0; i < _words.Length; i++)
            {
                wordsListView.Items.Add(_words[i]);
            }

            // new word text box clear.
            newWordTextBox.Text = "";
        }

        /// <summary>
        /// Check to new word.
        /// </summary>
        /// <returns></returns>
        private bool CheckToNewWord()
        {

            // if ne word is null or empty.
            if (string.IsNullOrEmpty(newWordTextBox.Text))
            {
                MessageBox.Show("Lütfen bir kelime girin.");
                return false;
            }

            // check to words on all characters.
            foreach (char character in newWordTextBox.Text)
            {

                // if the word is not one.
                if (char.IsWhiteSpace(character))
                {
                    MessageBox.Show("lütfen tek bir kelime girin.");
                    newWordTextBox.Text = "";
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Delete word.
        /// </summary>
        private void DeleteWord()
        {

            try
            {
                // reset to words array.
                _words = File.ReadAllLines(@"..\..\words.txt");

                // reset to words database.
                File.Delete(wordsTxtPath);
                FileStream filecreate = new FileStream(wordsTxtPath, FileMode.OpenOrCreate, FileAccess.Write);
                filecreate.Close();

                // delete selected word in user details.
                for (int i = 0; i < _words.Length; i++)
                {
                    if (_words[i] == wordsListView.SelectedItems[0].Text)
                    {
                        continue;
                    }
                    else
                    {
                        // write to database.
                        File.AppendAllText(wordsTxtPath, _words[i] + "\n", Encoding.UTF8);
                    }

                }

                // call to 'Reset to listview' method.
                ResetToListView();

            }
            catch
            {
                MessageBox.Show("Lütfen silmek için bir eleman seçiniz.");
            }

        }

        #endregion

        /// <summary>
        /// Admin controls page load method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdminControlsPage_Load(object sender, EventArgs e)
        {
            // wordsLisView feature.
            wordsListView.View = View.Details;
            wordsListView.GridLines = true;

            // add the column.
            wordsListView.Columns.Add("", 250);

            // call to 'Reset list view' method.
            ResetToListView();

        }

        #region Button methods.

        /// <summary>
        /// Add button method. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            // check to new word.
            if (CheckToNewWord() == true)
            {
                // Write to databese. 
                File.AppendAllText(wordsTxtPath, newWordTextBox.Text + "\n", Encoding.UTF8);
                ResetToListView();
            }
        }

        /// <summary>
        /// Delete button method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            // call to 'Delete word' method.
            DeleteWord();
        }

        /// <summary>
        /// Back button method. Go to Welcome page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backButton_Click(object sender, EventArgs e)
        {
            WelcomePage welcomePage = new WelcomePage();
            welcomePage.Show();
            this.Hide();
        }

        #endregion 
    }
}
