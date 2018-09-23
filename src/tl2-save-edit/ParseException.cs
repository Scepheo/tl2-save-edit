using System;

namespace Tl2SaveEdit
{
    public class ParseException : Exception
    {
        public ParseException(string message) : base(message) { }
    }
}
