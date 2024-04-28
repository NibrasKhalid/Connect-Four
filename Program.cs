using System; // Importing the System namespace which contains fundamental classes and base classes

namespace ConnectFour // Creating a namespace named ConnectFour to encapsulate the code
{
    class Program // Declaring a class named Program
    {
        static char[,] board = new char[6, 7]; // Declaring a 2D array to represent the game board
        static char currentPlayer = 'X'; // Declaring a variable to store the current player, initialized as 'X'
        static char computerPlayer = 'O'; // Declaring a variable to store the computer player, initialized as 'O'
        static int playerWins = 0; // Declaring a variable to store the number of player wins, initialized as 0
        static int computerWins = 0; // Declaring a variable to store the number of computer wins, initialized as 0
        static int ties = 0; // Declaring a variable to store the number of ties, initialized as 0

        static void Main(string[] args) // Declaring the entry point of the program
        {
            bool playAgain = true; // Declaring a variable to control whether to play the game again, initialized as true
            while (playAgain) // Starting a loop to play the game multiple times
            {
                InitializeBoard(); // Calling a method to initialize the game board
                DisplayBoard(); // Calling a method to display the game board
                char difficulty = GetDifficultyLevel(); // Calling a method to get the difficulty level from the player
                PlayGame(difficulty); // Calling a method to start the game with the chosen difficulty
                UpdateStats(); // Calling a method to update game statistics
                playAgain = AskToPlayAgain(); // Calling a method to ask if the player wants to play again and updating the playAgain variable accordingly
            }
        }

        static void InitializeBoard() // Declaring a method to initialize the game board
        {
            for (int i = 0; i < 6; i++) // Looping through rows of the game board
            {
                for (int j = 0; j < 7; j++) // Looping through columns of the game board
                {
                    board[i, j] = ' '; // Assigning empty space to each cell of the game board
                }
            }
        }

        static void DisplayBoard() // Declaring a method to display the game board
        {
            Console.Clear(); // Clearing the console
            for (int i = 5; i >= 0; i--) // Looping through rows of the game board in reverse order (from bottom to top)
            {
                for (int j = 0; j < 7; j++) // Looping through columns of the game board
                {
                    Console.Write("| " + board[i, j] + " "); // Displaying the content of each cell of the game board with formatting
                }
                Console.WriteLine("|"); // Displaying the end of each row of the game board
            }
            Console.WriteLine("  1   2   3   4   5   6   7 "); // Displaying column numbers
            Console.WriteLine(); // Displaying an empty line for spacing
        }


        static char GetDifficultyLevel() // Declaring a method to get the difficulty level from the player
        {
            Console.WriteLine("Choose difficulty level:"); // Displaying a message to prompt the player to choose difficulty level
            Console.WriteLine("1. Easy"); // Displaying options for difficulty level
            Console.WriteLine("2. Hard");
            char choice = Console.ReadKey().KeyChar; // Reading the player's input
            while (choice != '1' && choice != '2') // Starting a loop to validate the player's input
            {
                Console.WriteLine("\nInvalid choice. Please enter 1 for Easy or 2 for Hard."); // Displaying an error message for invalid input
                choice = Console.ReadKey().KeyChar; // Reading the player's input again
                Console.WriteLine(choice); // Displaying the player's input
            }
            return (choice == '1') ? 'E' : 'H'; // Returning the chosen difficulty level ('E' for Easy, 'H' for Hard)
        }

        static void PlayGame(char difficulty) // Declaring a method to start the game
        {
            bool gameWon = false; // Declaring a variable to track whether the game is won
            bool boardFull = false; // Declaring a variable to track whether the game board is full

            while (!gameWon && !boardFull) // Starting a loop to continue the game until it's won or the board is full
            {
                if (currentPlayer == 'X') // Checking if it's the player's turn
                {
                    int column = GetPlayerMove(); // Calling a method to get the player's move
                    PlacePiece(column); // Calling a method to place the player's piece on the board
                }
                else // If it's the computer's turn
                {
                    int column = (difficulty == 'E') ? GetComputerMoveEasy() : GetComputerMoveHard(); // Calling a method to get the computer's move based on the difficulty level
                    PlacePiece(column); // Calling a method to place the computer's piece on the board
                }

                DisplayBoard(); // Calling a method to display the updated game board

                if (CheckForWin()) // Checking if the game is won
                {
                    Console.WriteLine(currentPlayer == 'X' ? "You win!" : "Computer win!"); // Displaying the winner
                    gameWon = true; // Updating the gameWon variable
                }
                else if (IsBoardFull()) // Checking if the game board is full
                {
                    Console.WriteLine("It's a draw! The board is full."); // Displaying a message for a draw
                    boardFull = true; // Updating the boardFull variable
                }

                currentPlayer = (currentPlayer == 'X') ? 'O' : 'X'; // Updating the current player after each turn
            }
        }


        static int GetPlayerMove() // Declaring a method to get the player's move
        {
            int column = -1; // Declaring a variable to store the chosen column index, initialized as -1
            while (column < 1 || column > 7) // Starting a loop to validate the player's input
            {
                Console.Write("\nYou are X"); // Displaying a message to indicate the player's symbol
                Console.Write("\nYour turn, enter a column (1-7): "); // Displaying a message to prompt the player to enter a column number
                if (!int.TryParse(Console.ReadLine(), out column)) // Parsing the player's input as an integer
                {
                    Console.WriteLine("\nInvalid input. Please enter a number."); // Displaying an error message for invalid input
                    continue; // Skipping to the next iteration of the loop
                }
                if (column < 1 || column > 7) // Checking if the column number is out of range
                {
                    Console.WriteLine("\nColumn number must be between 1 and 7."); // Displaying an error message for out-of-range input
                }
                else if (IsColumnFull(column - 1)) // Checking if the chosen column is full
                {
                    Console.WriteLine("\nColumn is full. Choose another column."); // Displaying an error message for a full column
                    column = -1; // Resetting the column variable to trigger re-entry
                }
            }
            return column - 1; // Returning the chosen column index (adjusted for zero-based indexing)
        }

        static int GetComputerMoveEasy() // Declaring a method to get a random move for the computer (easy difficulty)
        {
            Random random = new Random(); // Creating a new instance of the Random class
            return random.Next(0, 7); // Returning a random column index within the range [0, 6]
        }

        static int GetComputerMoveHard() // Declaring a method to get the computer's move based on a hard difficulty level
        {
            // Not implemented in the provided code snippet
            // This part needs to be implemented to make the computer play strategically
            // For now, just return a random move
            return GetComputerMoveEasy(); // Returning a random move (for placeholder)
        }

        static bool IsColumnFull(int column) // Declaring a method to check if a column is full
        {
            for (int i = 5; i >= 0; i--) // Looping through rows of the game board in reverse order
            {
                if (board[i, column] == ' ') // Checking if the cell is empty
                {
                    return false; // Returning false if the column is not full
                }
            }
            return true; // Returning true if the column is full
        }

        static void PlacePiece(int column) // Declaring a method to place a piece on the board
        {
            for (int i = 0; i < 6; i++) // Looping through rows of the game board
            {
                if (board[i, column] == ' ') // Finding the first empty cell in the chosen column
                {
                    board[i, column] = currentPlayer; // Placing the current player's piece in the empty cell
                    break; // Exiting the loop after placing the piece
                }
            }
        }

        static bool IsBoardFull() // Declaring a method to check if the game board is full
        {
            for (int i = 0; i < 6; i++) // Looping through rows of the game board
            {
                for (int j = 0; j < 7; j++) // Looping through columns of the game board
                {
                    if (board[i, j] == ' ') // Checking if any cell is empty
                        return false; // Returning false if any cell is empty
                }
            }
            return true; // Returning true if all cells are filled (board is full)
        }

        static bool CheckForWin() // Declaring a method to check for win conditions
        {
            for (int i = 0; i < 6; i++) // Looping through rows of the game board
            {
                for (int j = 0; j < 4; j++) // Looping through columns of the game board (up to the 4th column)
                {
                    if (board[i, j] == currentPlayer && board[i, j + 1] == currentPlayer && // Checking for horizontal win
                        board[i, j + 2] == currentPlayer && board[i, j + 3] == currentPlayer)
                        return true; // Returning true if four consecutive cells are occupied by the current player's piece
                }
            }

            for (int i = 0; i < 3; i++) // Looping through rows of the game board (up to the 3rd row)
            {
                for (int j = 0; j < 7; j++) // Looping through columns of the game board
                {
                    if (board[i, j] == currentPlayer && board[i + 1, j] == currentPlayer && // Checking for vertical win
                        board[i + 2, j] == currentPlayer && board[i + 3, j] == currentPlayer)
                        return true; // Returning true if four consecutive cells are occupied by the current player's piece
                }
            }

            for (int i = 0; i < 3; i++) // Looping through rows of the game board (up to the 3rd row)
            {
                for (int j = 0; j < 4; j++) // Looping through columns of the game board (up to the 4th column)
                {
                    if (board[i, j] == currentPlayer && board[i + 1, j + 1] == currentPlayer && // Checking for diagonal win (top-left to bottom-right)
                        board[i + 2, j + 2] == currentPlayer && board[i + 3, j + 3] == currentPlayer)
                        return true; // Returning true if four consecutive cells are occupied by the current player's piece
                    if (board[i, j + 3] == currentPlayer && board[i + 1, j + 2] == currentPlayer && // Checking for diagonal win (top-right to bottom-left)
                        board[i + 2, j + 1] == currentPlayer && board[i + 3, j] == currentPlayer)
                        return true; // Returning true if four consecutive cells are occupied by the current player's piece
                }
            }

            return false; // Returning false if no win condition is detected
        }


        static void UpdateStats() // Declaring a method to update game statistics
        {
            if (currentPlayer == 'X') // Checking if the current player is the computer
                computerWins++; // Incrementing the computer's win count
            else
                playerWins++; // Incrementing the player's win count
        }

        static bool AskToPlayAgain() // Declaring a method to ask if the player wants to play again
        {
            Console.WriteLine("Player Wins: " + playerWins); // Displaying the player's win count
            Console.WriteLine("Computer Wins: " + computerWins); // Displaying the computer's win count
            Console.WriteLine("Ties: " + ties); // Displaying the tie count
            Console.Write("Do you want to play again? (yes/no): "); // Prompting the player to choose whether to play again
            string choice = Console.ReadLine().ToLower(); // Reading the player's input and converting it to lowercase
            while (choice != "yes" && choice != "no") // Starting a loop to validate the player's input
            {
                Console.Write("Invalid input. Please enter 'yes' or 'no': "); // Displaying an error message for invalid input
                choice = Console.ReadLine().ToLower(); // Reading the player's input again and converting it to lowercase
            }
            
            return choice == "yes"; // Returning true if the player wants to play again, otherwise returning false
        }
    }
}
