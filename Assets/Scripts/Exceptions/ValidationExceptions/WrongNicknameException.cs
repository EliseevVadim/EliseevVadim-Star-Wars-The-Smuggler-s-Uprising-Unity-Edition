using System;

namespace Game.Exceptions.ValidationExceptions
{
    public class WrongNicknameException : Exception
    {
        public WrongNicknameException(string message) :
            base(message)
        { }
    }
}
