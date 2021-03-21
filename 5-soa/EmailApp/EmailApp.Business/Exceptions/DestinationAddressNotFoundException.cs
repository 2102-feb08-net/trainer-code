using System;

namespace EmailApp.Business.Exceptions
{
    public class OriginatorAddressNotFoundException : Exception
    {
        public OriginatorAddressNotFoundException()
        { }

        public OriginatorAddressNotFoundException(string message)
            : base(message)
        { }

        public OriginatorAddressNotFoundException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
