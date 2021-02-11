

namespace rockpaperscissors
{
    class RoundResult
    {
        public Move PlayerMove { get; }
        public Move ComputerMove { get; }
        public Result Result 
        {
            get
            {
                // Calculate whether the player beat the computer or not here

                if ((this.PlayerMove == Move.Rock && this.ComputerMove == Move.Scissors) || 
                    (this.PlayerMove == Move.Paper && this.ComputerMove == Move.Rock) || 
                    (this.PlayerMove == Move.Scissors && this.ComputerMove == Move.Paper)) 
                {
                    return Result.Win;
                }
                else if (this.PlayerMove == this.ComputerMove)
                {
                    return Result.Draw;
                }
                else
                {
                    return Result.Lose;
                }
            }
        }

        public RoundResult(Move playerMove, Move computerMove)
        {
            this.PlayerMove = playerMove;
            this.ComputerMove = computerMove;
        }



    }
}