using System;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ResetGame();
        }
        
        string MessageBoxCaption = "Tic-Tac-Toe";

        int playerWins = 0;
        int cpuWins = 0;
        int draws = 0;

        // Set up game board
        string[,] board = new string[3, 3];

        // Player X turn at start
        bool playerXTurn = true;

        private void button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Initialize row and column variables to -1
            int row = -1;
            int col = -1;

            // Determine row and column based on button clicked
            switch (clickedButton.Name)
            {
                case "button1": row = 0; col = 0; break;
                case "button2": row = 0; col = 1; break;
                case "button3": row = 0; col = 2; break;
                case "button4": row = 1; col = 0; break;
                case "button5": row = 1; col = 1; break;
                case "button6": row = 1; col = 2; break;
                case "button7": row = 2; col = 0; break;
                case "button8": row = 2; col = 1; break;
                case "button9": row = 2; col = 2; break;
            }

            // Check if the cell is already occupied
            if (board[row, col] != null)
            {
                return; // Do nothing if the cell is already filled
            }

            // Player's move
            if (playerXTurn)
            {
                board[row, col] = "X";
                clickedButton.Text = "X";
                playerXTurn = false; // Switch to O's turn
                CheckForWinner();
                CpuMove();
            }
        }

        private async void CpuMove()
        {
            Random rnd = new Random();
            int row, col;

            // Basic CPU logic (find an empty spot)
            do
            {
                row = rnd.Next(0, 3);
                col = rnd.Next(0, 3);
            } while (board[row, col] != null);

            board[row, col] = "O";

            // Find the button control by name and assign text to "O", wait 500 ms
            await Task.Delay(500);
            if (this.Controls.Find("button" + ((row * 3) + col + 1).ToString(), true)[0] is Button cpuButton)
            {
                cpuButton.Text = "O";
            }

            playerXTurn = true; // Switch back to X's turn
            CheckForWinner();
        }

        private void CheckForWinner()
        {
            if (button1.Text == "X" && button2.Text == "X" && button3.Text == "X"
            || button4.Text == "X" && button5.Text == "X" && button6.Text == "X"
            || button7.Text == "X" && button9.Text == "X" && button8.Text == "X"
            || button1.Text == "X" && button4.Text == "X" && button7.Text == "X"
            || button2.Text == "X" && button5.Text == "X" && button8.Text == "X"
            || button3.Text == "X" && button6.Text == "X" && button9.Text == "X"
            || button1.Text == "X" && button5.Text == "X" && button9.Text == "X"
            || button3.Text == "X" && button5.Text == "X" && button7.Text == "X")
            {
                DeclareWinner("X");
                playerWins++;
                label1.Text = $"Player \"X\":\n{playerWins} wins";
                return;
            }
            else if (button1.Text == "O" && button2.Text == "O" && button3.Text == "O"
            || button4.Text == "O" && button5.Text == "O" && button6.Text == "O"
            || button7.Text == "O" && button9.Text == "O" && button8.Text == "O"
            || button1.Text == "O" && button4.Text == "O" && button7.Text == "O"
            || button2.Text == "O" && button5.Text == "O" && button8.Text == "O"
            || button3.Text == "O" && button6.Text == "O" && button9.Text == "O"
            || button1.Text == "O" && button5.Text == "O" && button9.Text == "O"
            || button3.Text == "O" && button5.Text == "O" && button7.Text == "O")
            {
                DeclareWinner("O");
                cpuWins++;
                label2.Text = $"CPU \"O\":\n{cpuWins} wins";
                return;
            }
            else if (IsBoardFull())
            {
                DeclareWinner("Draw");
                draws++;
                label3.Text = $"Draws:\n{draws}";
                return;
            }
        }

        private bool IsBoardFull()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button && control.Name.StartsWith("button"))
                {
                    if (string.IsNullOrEmpty(control.Text))
                    {
                        return false; // Board is not full
                    }
                }
            }
            return true; // Board is full
        }

        private void DeclareWinner(string winner)
        {
            if (winner == "Draw")
            {
                
                MessageBox.Show("It's a draw!", MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Player " + winner + " wins!", MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ResetGame();
        }

        private void ResetGame()
        {
            board = new string[3, 3];
            playerXTurn = true;

            // Clear all buttons
            foreach (Control control in this.Controls)
            {
                if (control is Button)
                {
                    string buttonNumberString = control.Name.Substring(6); // Get button numbers
                    if (int.TryParse(buttonNumberString, out int buttonNumber) && buttonNumber >= 1 && buttonNumber <= 9)
                    {
                        ((Button)control).Text = "";
                    }
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

    }
}