using System;
using EmailApp.Business;
using Xunit;

namespace EmailApp.UnitTests
{
    public class InboxCleanerTests
    {
        [Fact]
        public void Constructor_Accepts_Null()
        {
            var cleaner = new InboxCleaner(null);
        }
    }
}
