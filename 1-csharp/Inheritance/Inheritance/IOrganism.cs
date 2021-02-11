using System;
using System.Collections.Generic;
using System.Text;

namespace Inheritance
{
    public interface IOrganism
    {
        int NumberOfLegs { get; }
        bool CanFly { get; }
        string Kind { get; }
        string Name { get; }

        // it wouldn't make sense to have an access modifier on an interface member that was different from the whole interface's access.
        // for this reason you don't put access modifiers on interface members.

        // don't get confused by the syntax in interfaces that looks like auto-properties
        // these are not auto-properties, all those four lines mean is, the implementing class
        //     must have a property with at least a getter. could be full, could be auto
    }
}
