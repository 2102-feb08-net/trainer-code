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
        private IOutputter _outputter;

        public Game(IOutputter outputter) : this(outputter, "Player1")
        {
        }

        public Game(IOutputter outputter, string playerName)
        {
            this.history = new List<RoundResult>();
            this.player = new Player(playerName);
            this.computer = new Computer();
            _outputter = outputter;
        }

        public Game(IOutputter outputter, string playerName, Computer computer)
        {
            this.history = new List<RoundResult>();
            this.player = new Player(playerName);
            this.computer = computer;
            _outputter = outputter;
        }

        public static void CastingDemo()
        {
            // putting a more-specifically-typed value in a more generally-typed container
            IOutputter o = new Outputter(); // upcasting
            HelperMethod(new Outputter()); // upcasting
            // upcasting is safe, implicit (you don't have to cast with () )

            Outputter o2 = (Outputter)o; // downcasting
            // because the compiler can't know for sure that this will work,
            //    C# says, you have to declare your certainty that it should work with () operater.
            //    if you were wrong, it will throw an exception at runtime.

            // downcasting is explicit

            // other kinds of casting, like numeric conversions.
            double x = 5.5;
            int a = (int)x; // int can't represent every possible double, so data could be lost if we want this to happen
                                  // (it doesn't round-to-nearest-integer, it truncates, discards the decimal places)
            double y = a; // double can represent every possible int, so this is safe, so it's an implicit conversion

            // boxing and unboxing

            // value types behave in one way when you pass them to a new variable (copy the value)
            // reference types behave in a different way (copy the reference)

            int x2 = 3;
            object o3 = x2; // the value is wrapped in a object on the heap, so that o3 can have the reference-type semantics we expect.
                // that's called boxing.
            int y2 = (int)o3; // unboxing the value.

            // C# supports operator overloading
            // you can make your class work with +, ==, [], etc
            // you can even define your own custom implicit casts and explicit casts.
        }

        public static void HelperMethod(IOutputter o)
        {
            Outputter o2 = (Outputter)o; // downcasting
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void PlayRound()
        {
            // Get Player's Move
            Move playerMove = player.GetMove();

            // Get Computer's Move
            Move computerMove = computer.GetMove();

            // Generate Result
            RoundResult result = new RoundResult(playerMove, computerMove);

            _outputter.Write($"{result.PlayerMove} vs. {result.ComputerMove}!");
            if (result.Result == Result.Win)
            {
                _outputter.Write($"{player.Name} has won this round!");
            }
            else if (result.Result == Result.Lose)
            {
                _outputter.Write($"{computer.Name} has won this round!");
            }
            else if (result.Result == Result.Draw)
            {
                _outputter.Write($"{player.Name} and {computer.Name} have met in a draw this round!");
            }
            else
            {
                throw(new SystemException("Round result invalid?!"));
            }

            // Save Result
            history.Add(result);
            _outputter.Write();
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