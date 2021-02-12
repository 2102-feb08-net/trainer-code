using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rockpaperscissors
{
    public class Outputter : IOutputter
    {
        public void Write(string s)
        {
            Console.WriteLine(s);
        }

        public void Write()
        {
            Console.WriteLine();
        }
    }
}
