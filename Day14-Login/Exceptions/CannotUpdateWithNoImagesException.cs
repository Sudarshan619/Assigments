using System.Runtime.Serialization;

namespace Day14_Login.Exceptions
{
    [Serializable]
    internal class CannotUpdateWithNoImagesException : Exception
    {
        public CannotUpdateWithNoImagesException()
        {
        }

    }
}