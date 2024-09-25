using System.Runtime.Serialization;

namespace Day16_PizzaStore.Exceptions
{
    internal class CollectionEmptyException : Exception
    {
        string message;
        public CollectionEmptyException()
        {
            message = "Collection is empty";
        }

        public CollectionEmptyException(string? entity)
        {
            message = $"Collection of {entity} is empty";
        }
        public override string Message => message;

    }
}