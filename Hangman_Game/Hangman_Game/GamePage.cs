using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Hangman_Game
{
    public partial class GamePage : Form
    {

        // This string array contains all characters.
        string[] characters =
        {
            "a","b","c","ç","d","e","f","g","h","ı","i","j","k","l",
            "m","n","o","ö","p","r","s","ş","t","u","ü","v","y","z",
            "A","B","C","Ç","D","E","F","G","H","I","İ","J","K","L",
            "M","N","O","Ö","P","R","S","Ş","T","U","Ü","V","Y","Z"
        };
        // This string contains score board database path.
        string scoreBoardTxtPath = @"..\..\scoreBoard.txt";
        // This string array contains guessed characters.
        string[] guessedCharacters = new string[15];
        // how many try character.
        int guessedCharacterIndex = 0;
        // This label array contains dynamic create labels.
        Label[] labelArray;
        // This variable contains lives numberç
        private int _lives = 10;
        // this variable contains remaining time.
        private int _remainingTime = 60;
        // this variable contains point.
        private int _point = 0;

        public GamePage()
        {
            InitializeComponent();
        }

        #region Private Methods.

        /// <summary>
        /// Word Guess method.
        /// </summary>
        private void WordGuess()
        {
            // check to guessed word.
            if (wordTextBox.Text == "")
            {
                MessageBox.Show("Lütfen bir kelime girin.");
            }
            else if (wordTextBox.Text == GameInformationPage.selectedWord)
            {

                // if the number tried is correct.
                MessageBox.Show("Tebrikler bildiniz.\nPuanınız: " + _point.ToString() + "\nYüksek skorlara kaydedildi.");

                // call to Game over method.
                GameOver();

                // point and player name write to databese. 
                File.AppendAllText(scoreBoardTxtPath, "\n" + GameInformationPage.playerName + "#" + _point.ToString(), Encoding.UTF8);

            }
            else
            {

                // if the number tried is not correct.
                MessageBox.Show("Yanlış cevap ");

                // word text box clear.
                wordTextBox.Text = "";

                // lower lives. 
                _lives--;

                // if the number of rights runs out
                if (_lives == 0)
                {
                    MessageBox.Show("Hak sayısı bitti\nKaybettin.\nYeni oyuna başla");

                    // call to Game over method.
                    GameOver();
                }

                livesLabel.Text = _lives.ToString();
            }
        }

        /// <summary>
        /// Charactter Guess method.
        /// </summary>
        private void CharacterGuess()
        {

            // check to guessed character.
            if (characterTextBox.Text == "")
            {
                MessageBox.Show("Lütfen bir harf girin.");
            }
            else if (characterTextBox.Text.Length > 1)
            {
                MessageBox.Show("Lütfen yalnızca bir harf girin.");
            }
            else
            {

                bool ctrl = false;

                // check to guessed character to characters array.
                for (int i = 0; i < characters.Length; i++)
                {
                    // if correctly character guessed.
                    if (characterTextBox.Text == characters[i])
                    {
                        ctrl = true;
                        break;
                    }
                }

                // if not correctly character guessed.
                if (ctrl == false)
                {
                    MessageBox.Show("Lütfen yalnızca harf girin.");
                    return;
                }

                // if this character has been tried before
                for (int i = 0; i < guessedCharacters.Length; i++)
                {
                    if (characterTextBox.Text == guessedCharacters[i])
                    {
                        MessageBox.Show("'" + characterTextBox.Text + "' harfini zaten denediniz.");
                        return;
                    }
                }


                // this variable contains character index in selected word.
                int index = -1;
                // this variable contains true or false to guessed character. 
                bool control = false;

                // check to words on all characters.
                foreach (char character in GameInformationPage.selectedWord)
                {
                    index++;

                    // if the number tried is correct.
                    if (character == Convert.ToChar(characterTextBox.Text))
                    {
                        // call to 'Open label' method.
                        OpenLabel(index);
                        // set the control is true. 
                        control = true;
                        // add ten point.
                        _point += 10;
                        // guessed character write to 'guessed characters' array.
                        guessedCharacters[guessedCharacterIndex] = characterTextBox.Text;

                        guessedCharacterIndex++;

                        // if true all guessed character.
                        if (_point == GameInformationPage.selectedWord.Length * 10)
                        {
                            MessageBox.Show("Tebrikler bildiniz.\nPuanınız: " + _point.ToString() + "\nYüksek skorlara kaydedildi.");

                            // call to 'Game over' method.
                            GameOver();

                            // Write to databese. 
                            File.AppendAllText(scoreBoardTxtPath, "\n" + GameInformationPage.playerName + "#" + _point.ToString(), Encoding.UTF8);

                        }
                    }
                }

                // if the number tried is not correct.
                if (control == false)
                {
                    // lower lives.
                    _lives--;

                    livesLabel.Text = _lives.ToString();

                    // if the number of rights runs out.
                    if (_lives == 0)
                    {
                        MessageBox.Show("Hak sayısı bitti\nKaybettin.\nYeni oyuna başla");

                        // call to 'Game over' method.
                        GameOver();
                    }
                }
            }

            // clear to character text box.
            characterTextBox.Text = "";

        }

        /// <summary>
        /// Game over method.
        /// </summary>
        private void GameOver()
        {

            timer.Stop();

            // all guess buttons enabled to false.
            characterGuessButton.Enabled = false;
            wordGuessButton.Enabled = false;
        }

        /// <summary>
        /// Create labels method.
        /// Selected words of count. Create labesl on the screen.
        /// </summary>
        /// <param name="digit"></param>
        private void CreateLabels(int digit)
        {
            // Clears labels on the screen.
            flowLayoutPanel.Controls.Clear();

            labelArray = new Label[digit];

            // create all label.
            for (int i = 0; i < digit; i++)
            {
                Label newLabel = new Label();
                newLabel.Text = "-";
                newLabel.Font = new Font(newLabel.Font.Name, 35);
                newLabel.AutoSize = true;
                newLabel.BackColor = Color.White;
                labelArray[i] = newLabel;
                flowLayoutPanel.Controls.Add(newLabel);
            }

        }


        /// <summary>
        /// Open label method.
        /// if correcly guessed character. This label openned.
        /// </summary>
        /// <param name="index"></param>
        private void OpenLabel(int index)
        {
            labelArray[index].Text = characterTextBox.Text;
        }

        #endregion

        /// <summary>
        /// Game Page load method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GamePage_Load(object sender, EventArgs e)
        {
            timer.Start();

            // call to 'Create Labels' method.
            CreateLabels(GameInformationPage.selectedWord.Length);  // hata hala var aq. !!!! 

            // write lives to screen.
            livesLabel.Text = _lives.ToString();

            //            MessageBox.Show(GameInformationPage.selectedWord);
        }

        #region Button Methods.

        /// <summary>
        /// Character Guessed Button Method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void characterGuessButton_Click(object sender, EventArgs e)
        {
            // call to 'Character guess' method.
            CharacterGuess();
        }

        /// <summary>
        /// Word guess button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wordGuessButton_Click(object sender, EventArgs e)
        {
            // call to 'Word guess' method. 
            WordGuess();
        }

        /// <summary>
        /// Quit button method. Go to Welcome page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quitButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
            WelcomePage welcomePage = new WelcomePage();
            welcomePage.Show();
            this.Hide();
        }

        #endregion

        private void timer_Tick(object sender, EventArgs e)
        {
            _remainingTime--;

            // if time runs out.
            if (_remainingTime == 0)
            {
                timeLabel.Text = _remainingTime.ToString();

                timer.Stop();

                MessageBox.Show("Süren bitti\nKaybettin");

                // call to 'Game over' method.
                GameOver();
            }
            else
            {
                timeLabel.Text = _remainingTime.ToString();
            }

        }

    }
}
