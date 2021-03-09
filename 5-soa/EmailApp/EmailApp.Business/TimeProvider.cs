using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailApp.Business
{
    public class TimeProvider : ITimeProvider
    {
        public DateTimeOffset CurrentTime => DateTime.Now;
    }
}
