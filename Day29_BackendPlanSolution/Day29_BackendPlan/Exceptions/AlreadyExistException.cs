using System.Runtime.Serialization;

namespace Day29_BackendPlan.Exceptions
{
    [Serializable]
    public class AlreadyExistException : Exception
    {
        string msg;
        public AlreadyExistException(string message)
        {
            msg = message;
        }

        public override string Message => base.Message;
    }
}