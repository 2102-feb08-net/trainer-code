

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace rockpaperscissors
{
    // just like the rest of the file defines a class type, this one line defines a delegate type
    // names "NumberCombiner", with two int params, and returning int.
    public delegate int NumberCombiner(int x, int y);

    public class Computer : Player
    {
        private readonly Func<Move> _moveChooser;

        public Computer() : base("Computer1")
        {
        }

        // data types         |         values those data types can hold
        // ---------------------------------------------------
        //     int            |        3, -8
        //    string          |       null, "asdf", ""
        // (delegate type)    |    (delegates (functions/methods))
        //    - (ex. Func<T>) |    this.GetMove, () => Move.Paper
        //                                          ^ "lambda expression"

        // in the same way that class, struct, enum, interface are all categories of types in .NET,
        //  delegate is another category like those.

        public Computer(Func<Move> moveChooser) : this()
        {
            _moveChooser = moveChooser;
        }


        public override Move GetMove()
        {
            if (_moveChooser != null)
            {
                return _moveChooser();
            }
            return Move.Paper;
        }

        public static void DelegateStuff(NumberCombiner function)
        {
            Func<Move> function2 = () => Move.Paper;

            // Func<T> & friends are generic delegate types.
            // Func<T> is for functions that take no parameters and return T.
            // Func<T> is for functions that take no parameters and return T.
            // Func<T1, T> is for functions that take one parameter of type T1 and return T.

            // Action is for functions that take no parameters and return void.
            // Action<T1> is for functions that take one parameter of type T1 and return void.

            Action<string> print = x => Console.WriteLine(x);

            Func<int, int, int> add = (x, y) => x + y;

            NumberCombiner add2 = (x, y) => x + y;

            // Predicate<T> is basically the same as Func<T, bool>
            //Predicate<int>  

            var seven = add(3, 4);

            List<int> numbers = new List<int> { 1, 2, 3, 4, 6, 8, 4, 3 };
            IEnumerable<int> filtered = numbers.Where(x => x > 5);
            filtered = filtered.OrderByDescending(x => x); // re-order the sequence in descending order.

            List<int> filteredList = filtered.ToList(); // { 6, 8 }
            // only at that point just now, was the list filtered and sorted.


            List<string> strings = new List<string> { "asd", "bcd", "tffq" };
            strings.OrderBy(x => x.Length); // length order
            strings.OrderBy(x => x); // lexicographic order / dictionary order

            // LINQ:
            //    three kinds of functions in LINQ.
            //  1. the ones that return one distinct value. these functions process the sequence immediately.
            //      ex: Count, Min, Max, First
            //  2. the ones that return IEnumerable (a sequence). these functions use "deferred execution".
            //     ex: OrderBy, Where, Select, Skip, Take
            //  3. thes ones that return some concrete collection. these also process immediately.
            //     ex: ToList

            // it's usual in functional programming to, instead of MODIFYING data, return NEW data
            // so LINQ methods of the 2nd type never modify the original sequence. they always return a new one

            strings.OrderBy(x => x.Length) // deferred execution
                .Select(x => x[0]) // deferred execution
                .First();         // sort based on length, only needs to index into one of the strings, not all.
        }
    }
}