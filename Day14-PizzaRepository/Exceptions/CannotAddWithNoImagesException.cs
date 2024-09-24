using System.Runtime.Serialization;

namespace Day14_PizzaRepository.Exceptions
{
    [Serializable]
    internal class CannotAddWithNoImagesException : Exception
    {
        public CannotAddWithNoImagesException()
        {
        }
    }
}