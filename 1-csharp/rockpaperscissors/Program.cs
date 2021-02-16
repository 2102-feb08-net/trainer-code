using System;
using System.Collections.Generic;
using System.Linq;
using rockpaperscissors;


namespace testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var outputter = new Outputter();

            // OOP - combine data and behavior into objects
            // FP (functional programming) - treat behavior as just another kind of data.
            // a delegate is a function viewed as just another piece of data we can move around.

            var listOfMoves = new List<Move> { Move.Paper, Move.Scissors, Move.Rock };
            var random = new Random();

            var strategies = new List<Func<Move>>
            {
                AlwaysScissors,
                () => {
                    return Move.Rock;
                },
                () => Move.Paper,
                () => listOfMoves[random.Next(3)]
            };

            // give the user some choices as to what type of computer he wants to play against
            var computer = new Computer(strategies[3]);

            // Create a game of RockPaperScissors
            Game game = new Game(outputter, "Bob", computer);
            
            // Play three rounds
            for (int i = 0; i < 3; i++)
            {
                game.PlayRound();
            }

            // Summarize overall results
            game.SummarizeHistory();

        }

        // delegate types are implicitly convertible if they have the same parameter types and return type (signature).

        static Move AlwaysScissors()
        {
            return Move.Scissors;
        }

        // in C#, whenever you have a method body or a lambda expression body (with braces)
        // if the body is JUST "return (whatever);"
        // you can instead use "expression body" syntax like this.    (the normal way is called "block body")
        static Move AlwaysScissorsAlso() => Move.Scissors;
    }
}
