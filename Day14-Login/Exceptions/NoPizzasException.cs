using System.Runtime.Serialization;

namespace Day14_Login.Exceptions
{
    [Serializable]
    internal class NoPizzasException : Exception
    {
        public NoPizzasException()
        {
        }

        public NoPizzasException(string? message) : base(message)
        {
        }

        public NoPizzasException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoPizzasException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}