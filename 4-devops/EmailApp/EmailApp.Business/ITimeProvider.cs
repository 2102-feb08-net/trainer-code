using System;

namespace EmailApp.Business
{
    public interface ITimeProvider
    {
        DateTimeOffset CurrentTime { get; }
    }
}