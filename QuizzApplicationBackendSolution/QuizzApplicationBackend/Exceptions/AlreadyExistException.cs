using Microsoft.VisualBasic;
using System.Runtime.Serialization;

namespace QuizzApplicationBackend.Exceptions
{
    [Serializable]
    internal class AlreadyExistException : Exception
    {
        string msg;
        public AlreadyExistException(string message)
        {
            msg = message;
        }
        
        public override string  Message => base.Message;
       
    }
}