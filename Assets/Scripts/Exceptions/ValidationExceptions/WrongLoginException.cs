using System;

namespace Game.Exceptions.ValidationExceptions
{
    public class WrongLoginException : Exception
    {
        public WrongLoginException(string message) :
            base(message)
        { }
    }
}
