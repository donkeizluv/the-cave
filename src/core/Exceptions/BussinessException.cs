using System;

namespace CaveCore.Exceptions
{
    public class BussinessException : Exception
    {
        public BussinessException(string message) : base(message)
        {
            
        }
    }
}