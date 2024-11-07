using System.Runtime.Serialization;

namespace Day27_Webapi_EF.Repositories
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