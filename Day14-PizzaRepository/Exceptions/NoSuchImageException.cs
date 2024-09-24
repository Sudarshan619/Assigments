using System.Runtime.Serialization;

namespace Day14_PizzaRepository.Exceptions
{
    [Serializable]
    internal class NoSuchImageException : Exception
    {
        public NoSuchImageException()
        {
        }

        public NoSuchImageException(string? message) : base(message)
        {
        }

        public NoSuchImageException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoSuchImageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}