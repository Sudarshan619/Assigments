using System.Runtime.Serialization;

namespace Day14_PizzaRepository.Exceptions
{
    [Serializable]
    internal class CannotUpdateWithNoImagesException : Exception
    {
        public CannotUpdateWithNoImagesException()
        {
        }

        public CannotUpdateWithNoImagesException(string? message) : base(message)
        {
        }

        public CannotUpdateWithNoImagesException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CannotUpdateWithNoImagesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}