using System;
using rockpaperscissors;


namespace testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var outputter = new Outputter();

            // Create a game of RockPaperScissors
            Game game = new Game(outputter);
            
            // Play three rounds
            for (int i = 0; i < 3; i++)
            {
                game.PlayRound();
            }

            // Summarize overall results
            game.SummarizeHistory();

        }
    }
}
