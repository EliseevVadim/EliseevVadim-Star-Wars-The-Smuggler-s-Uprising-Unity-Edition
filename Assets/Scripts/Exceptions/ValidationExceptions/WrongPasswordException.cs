using System;

namespace Game.Exceptions.ValidationExceptions
{
    public class WrongPasswordException : Exception
    {
        public WrongPasswordException(string message) :
            base(message)
        { }
    }
}
