using System.Runtime.Serialization;

namespace Day29_BackendPlan.Exceptions
{
    [Serializable]
    public class CouldNotAddException : Exception
    {
        string msg;
        public CouldNotAddException(string message)
        {
            msg = message;
        }

        public override string Message => base.Message;
    }
}