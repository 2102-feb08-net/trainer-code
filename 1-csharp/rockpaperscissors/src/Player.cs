using System;

namespace rockpaperscissors
{
    public class Player
    {
        public string Name { get; }

        public Player(string playerName)
        {
            Name = playerName;
        }


        public virtual Move GetMove()
        {
            /*
                Valid Inputs:  (any capitalization)
                    Rock, Paper, Scissors
                    R, P, S
            */
            Move move = Move.None;

            do
            {
                Console.WriteLine("What move do you make?");
                string input = Console.ReadLine().ToLower();

                if (input == "rock" || input == "r")
                {
                    move = Move.Rock;
                }
                else if (input == "paper" || input == "p")
                {
                    move = Move.Paper;
                }
                else if (input == "scissors" || input == "s")
                {
                    move = Move.Scissors;
                }
                else
                {
                    Console.WriteLine("Input unacceptable! Please enter one of the following: Rock, Paper, Scissors");
                }
            } while (move == Move.None);
            
            return move;
        }
    }
}