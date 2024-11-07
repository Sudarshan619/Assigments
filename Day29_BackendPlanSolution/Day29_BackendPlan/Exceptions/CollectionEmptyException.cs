using System.Runtime.Serialization;

namespace Day29_BackendPlan.Exceptions
{
    [Serializable]
    public class CollectionEmptyException : Exception
    {
        string msg;
        public CollectionEmptyException(string message)
        {
            msg = message;
        }

        public override string Message => base.Message;
    }
}