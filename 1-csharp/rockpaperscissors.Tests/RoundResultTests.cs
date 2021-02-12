using System;
using System.Collections.Generic;
using Xunit;

namespace rockpaperscissors.Tests
{
    public class RoundResultTests
    {
        // three reasons "this file can't talk to that file"
        // 1. access level
        // 2. project reference
        // 3. namespace (using directive)

        [Fact]
        public void IdenticalMovesShouldResultInADraw()
        {
            // arrange
            var roundResult = new RoundResult(Move.Paper, Move.Paper);

            // act

            // assert
            throw new NotImplementedException();
        }

        //[Fact]
        //public void Test()
        //{
        //    // arrange
        //    var outputter = new FakeOutputter();
        //    var game = new Game(outputter);

        //    // act
        //    game.PlayRound();
        //}
    }

    // a better way of doing this is a mocking framework like Moq
    public class FakeOutputter : IOutputter
    {
        public List<string> History { get; set; } = new List<string>();

        public void Write()
        {
            History.Add(null);
        }

        public void Write(string s)
        {
            History.Add(s);
        }
    }
}
