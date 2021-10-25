using System;
using System.Collections.Generic;
using System.Linq;

namespace ttt_v3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Variables, board 
            string[] board = { " ", " ", " ", " ", " ", " ", " ", " ", " " };
            StartGame(board);

        }
        // Printing board after each update, but only will be called in player method. 
        static void ShowBoard(string[] board)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();
            Console.WriteLine(board[0] + "|" + board[1] + "|" + board[2]);
            Console.WriteLine("------");
            Console.WriteLine(board[3] + "|" + board[4] + "|" + board[5]);
            Console.WriteLine("------");
            Console.WriteLine(board[6] + "|" + board[7] + "|" + board[8]);
            Console.WriteLine("------");
            Console.ResetColor();
        }
        // taking input from player, run simple datacheck, update the game state and call Machine method. 
        static void Player(string[] board)
        {
            ShowBoard(board);
            PrintMessage(ConsoleColor.Yellow, "Choose a spot 0-8: ");
            string playerInput = Console.ReadLine();
            int playerMove;
            if (board.Contains(" "))
            {
                // Simple datacheck
                if (!int.TryParse(playerInput, out playerMove))
                {
                    PrintMessage(ConsoleColor.Red, "Only Numerics! press a key to try a gain..");
                    Console.ReadLine();
                    Player(board);
                }
                else
                {
                    if (playerMove > board.Length)
                    {
                        PrintMessage(ConsoleColor.Red, "Out of range.. press a key to try again");
                        Console.ReadLine();
                        Player(board);
                    }
                    else
                    {
                        if (board[playerMove] != " ")
                        {
                            PrintMessage(ConsoleColor.Red, "Spot is take, press a key to try again");
                            Console.ReadLine();
                            Player(board);
                        }
                        else
                        {
                            board[playerMove] = "X";
                            ShowBoard(board);
                            if(CheckWin(board)  == 1)
                            {
                                PrintMessage(ConsoleColor.Blue, "You won... press a key to continue..");
                                Console.ReadLine();
                                StartGame(board);
                            }
                            Machine(board);

                        }
                    }
                }
            }
            else
            {
                PrintMessage(ConsoleColor.Blue, "Game is tie, press a key to continue..");
                Console.ReadLine();
                StartGame(board);
            }
        }
        // checking for available spots, choose random number only from available spots to avoid recrusion depth erro.
        static void Machine(string[] board)
        {
            int machineMove;

            if (board.Contains(" "))
            {
                List<int> availableSpots = new List<int>();
                for(int i = 0; i < board.Length; i++)
                {
                    if(board[i] == " " && board[i] != "X" && board[i] != "O")
                    {
                        if(!availableSpots.Contains(i))
                        {
                            availableSpots.Add(i);
                            Console.WriteLine(i);
                            //Console.ReadLine();
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                Random random = new Random();
                int index = random.Next(availableSpots.Count);
                machineMove = availableSpots.ElementAt(index);

                Console.WriteLine("Random Spot: {0}", machineMove);
                //Console.ReadLine();
                if(board[machineMove] != " ")
                {
                    Machine(board);
                }
                else
                {
                    board[machineMove] = "O";
                    if(CheckWin(board) == 1)
                    {
                        PrintMessage(ConsoleColor.Red, "You lost! press a key to continue..");
                        Console.ReadLine();
                        StartGame(board);
                    }
                    Player(board);
                }
                
            }

            if(!board.Contains(" "))
            {
                PrintMessage(ConsoleColor.Yellow, "Game is tie press a key to continue.. ");
                Console.ReadLine();
                StartGame(board);
            }
        }
        
        // Returns 1 if any of the winning possibelities occured after each update.
        static int CheckWin(string[] board)
        {
            if (board[0] == board[1] && board[0] == board[2] && board[0] != " ")
            {
                return 1;
            }
            else if (board[3] == board[4] && board[3] == board[5] && board[3] != " ")
            {
                return 1;
            }
            else if (board[6] == board[7] && board[6] == board[8] && board[6] != " ")
            {
                return 1;
            }
            else if (board[0] == board[3] && board[0] == board[6] && board[0] != " ")
            {
                return 1;
            }
            else if (board[1] == board[4] && board[1] == board[7] && board[1] != " ")
            {
                return 1;
            }
            else if (board[2] == board[5] && board[2] == board[8] && board[2] != " ")
            {
                return 1;
            }
            else if (board[0] == board[4] && board[0] == board[8] && board[0] != " ")
            {
                return 1;
            }
            else if (board[2] == board[4] && board[2] == board[6] && board[2] != " ")
            {
                return 1;
            }
            return 0;
        }
        // Basically i tried to get random number for machine moves but after i got recrusion depth error 
        // so i stop using it!
        static int GetRandNumber()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 8);
            return randomNumber;
        }
        // simple method to print colorful messages 
        static void PrintMessage(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        
        // Reseting the game board, choosing the first move for player or computer.
        static void StartGame(string[] board)
        {
            // Cleanup the board
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = " ";
            }

            PrintMessage(ConsoleColor.Blue, "Wanna play a new game? Y/N: ");
            var answer_1 = Console.ReadLine().ToUpper();
            if (answer_1 == "Y")
            {
                PrintMessage(ConsoleColor.Blue, "Want to make a first move? Y/N: ");
                var answer_2 = Console.ReadLine().ToUpper();
                if (answer_2 == "Y")
                {
                    Player(board);
                }
                else
                {
                    Machine(board);
                }
            }
            else
            {
                PrintMessage(ConsoleColor.Blue, "Press a key to exit..");
                Console.ReadLine();
                return;
            }

        }
    }      
}
            

        
    

