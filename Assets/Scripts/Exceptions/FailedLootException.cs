using System;

namespace Game.Exceptions
{
    class FailedLootException : Exception
    {
        public FailedLootException(string message) :
            base(message)
        { }
    }
}
