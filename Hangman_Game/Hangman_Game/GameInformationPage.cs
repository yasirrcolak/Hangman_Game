using System;
using System.IO;
using System.Windows.Forms;

namespace Hangman_Game
{
    public partial class GameInformationPage : Form
    {
        // Current player name.
        public static string playerName { get; set; }
        // Random word generated based on the number of characters chosen by the user.
        public static string selectedWord { get; set; }
        // This string array contains words.
        private string[] _words = File.ReadAllLines(@"..\..\words.txt");
        // This string array contains 'how many letters are in each word'.
        private string[] _wordsOfDigits = new string[20];
        // All words with the number of characters the user has selected.
        string[] selectedCountWords;

        public GameInformationPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Check to Player informations. and returns true of false.
        /// </summary>
        /// <returns></returns>
        private bool CheckToPlayerInformations()
        {
            if (playerNameTextBox.Text == "")
            {
                MessageBox.Show("Lütfen isminizi giriniz.");
                return false;
            }
            else if (numberOfCharacterComboBox.Text == "")
            {
                MessageBox.Show("Lütfen kelime harf sayısı bilgisini işaretleyin.");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// divide words by characters and write to words of digits string array.
        /// </summary>
        private void DivideToWords()
        {
            // has it been added to array before.
            int counter;

            // check all the words
            for (int i = 0; i < _words.Length; i++)
            {
                counter = 0;

                // check all element to 'words of digits' array.
                for (int j = 0; j < _wordsOfDigits.Length; j++)
                {
                    if (_words[i].Length == Convert.ToInt32(_wordsOfDigits[j]))
                    {
                        counter++;
                    }
                    else
                    {
                        continue;
                    }
                }

                // add if that value is not in the array
                if (counter == 0)
                {
                    _wordsOfDigits[i] = _words[i].Length.ToString();

                }

            }

            // write the contents of the 'words of digits' array in the 'number of character' comboBox.
            for (int i = 0; i < _wordsOfDigits.Length; i++)
            {
                if (_wordsOfDigits[i] != null)
                {
                    numberOfCharacterComboBox.Items.Add(_wordsOfDigits[i]);
                }
            }


        }

        /// <summary>
        /// Choose one of the words containing the number of randomly selected characters.
        /// </summary>
        private void RandomWord()
        {
            selectedCountWords = new string[_words.Length];
            int count = 0;

            // check all the words
            for (int i = 0; i < _words.Length; i++)
            {
                // word has a selected number of characters
                if (_words[i].Length == Convert.ToInt32(numberOfCharacterComboBox.Text))
                {
                    if (!string.IsNullOrEmpty(_words[i]))
                    {
                        // add word to 'select count words' array.
                        selectedCountWords[count] = _words[i];
                        count++;
                    }
                }
            }

            // new random int variable.
            Random r = new Random();
            int random = r.Next(0, selectedCountWords.Length - 1);

            // set selected word to random word.
            selectedWord = selectedCountWords[random];

            // try again if random is null
            if (string.IsNullOrEmpty(selectedWord))
            {
                RandomWord();
            }

        }

        /// <summary>
        /// Game Informations Page load method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameInformationPage_Load(object sender, EventArgs e)
        {
            DivideToWords();
        }

        /// <summary>
        /// Start button method. Go to Game Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startButton_Click(object sender, EventArgs e)
        {
            if (CheckToPlayerInformations() == true)
            {
                playerName = playerNameTextBox.Text;
                RandomWord();

                GamePage gamePage = new GamePage();
                gamePage.Show();
                this.Hide();
            }
        }

        /// <summary>
        /// Back button method. Go to Welome Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backButton_Click(object sender, EventArgs e)
        {
            WelcomePage welcomePage = new WelcomePage();
            welcomePage.Show();
            this.Hide();
        }
    }
}
