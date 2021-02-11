using System;
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
    }
}
