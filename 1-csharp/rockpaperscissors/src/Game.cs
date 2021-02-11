using System;
using System.Collections.Generic;

/*
play rock-paper-scissors with the computer.
keep track of a history of wins and losses
have different difficulties/strategies for the computer to use against the player

Console.ReadLine()
*/


namespace rockpaperscissors
{
    public class Game
    {
        private List<RoundResult> history;
        private Player player;
        private Computer computer;


        public Game() : this("Player1")
        {
        }

        public Game(string playerName) 
        {
            this.history = new List<RoundResult>();
            this.player = new Player(playerName);
            this.computer = new Computer();
        }

        public void PlayRound()
        {
            // Get Player's Move
            Move playerMove = player.GetMove();

            // Get Computer's Move
            Move computerMove = computer.GetMove();

            // Generate Result
            RoundResult result = new RoundResult(playerMove, computerMove);

            Console.WriteLine($"{result.PlayerMove} vs. {result.ComputerMove}!");
            if (result.Result == Result.Win)
            {
                Console.WriteLine($"{player.Name} has won this round!");
            }
            else if (result.Result == Result.Lose)
            {
                Console.WriteLine($"{computer.Name} has won this round!");
            }
            else if (result.Result == Result.Draw)
            {
                Console.WriteLine($"{player.Name} and {computer.Name} have met in a draw this round!");
            }
            else
            {
                throw(new SystemException("Round result invalid?!"));
            }

            // Save Result
            history.Add(result);
            Console.WriteLine();
        }

        public void SummarizeHistory()
        {
            int wins = 0, losses = 0, draws = 0;

            foreach (RoundResult r in history)
            {
                if (r.Result == Result.Win)
                {
                    wins++;
                }
                else if (r.Result == Result.Lose)
                {
                    losses++;
                }
                else if (r.Result == Result.Draw)
                {
                    draws++;
                }
                else
                {
                    throw(new SystemException("Round result invalid?! 2: Electric Boogaloo"));
                }
            }

            Console.WriteLine($"{player.Name} has won against {computer.Name} {wins} times!");
            Console.WriteLine($"{player.Name} has lost against {computer.Name} {losses} times!");
            Console.WriteLine($"{player.Name} has met in a draw with {computer.Name} {draws} times!");
            Console.WriteLine("Good Game!");
        }
    }

    // Utility Enum for tracking moves
    public enum Move
    {
        Rock,
        Paper,
        Scissors,
        None
    }

    // Utility Enum for tracking results
    public enum Result
    {
        Win,
        Lose,
        Draw
    }
}