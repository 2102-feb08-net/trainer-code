using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp
{
    // access modifiers
    // public, private
    // internal: visible within the same project (assembly), invisible outside it.
    //      internal is the default for classes (and other non-nested types like interfaces)

    public class Outputter
    {
        public void Output(string data)
        {
            Console.WriteLine(data);
        }
    }
}
