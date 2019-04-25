using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItmoJobsFetchingBot
{
    public class WrongCommandException : Exception
    {
        public WrongCommandException(string message) : base(message) { }
    }
    public class WrongArgumentException : Exception
    {
        public WrongArgumentException(string message) : base(message) { }
    }
}
    