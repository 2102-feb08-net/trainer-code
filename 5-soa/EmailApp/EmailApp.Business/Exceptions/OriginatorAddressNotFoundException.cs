using System;

namespace EmailApp.Business.Exceptions
{
    public class DestinationAddressNotFoundException : Exception
    {
        public DestinationAddressNotFoundException()
        { }

        public DestinationAddressNotFoundException(string message)
            : base(message)
        { }

        public DestinationAddressNotFoundException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
