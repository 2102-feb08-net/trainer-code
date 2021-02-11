

namespace rockpaperscissors
{
    public class Computer : Player
    {
        

        public Computer() : base("Computer1")
        {
        }


        public override Move GetMove()
        {
            return Move.Paper;
        }
    }
}