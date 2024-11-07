using System.Runtime.Serialization;

namespace Day29_BackendPlan.Exceptions
{
    [Serializable]
   
        public class NotFoundException : Exception
        {
            string msg;
            public NotFoundException(string message)
            {
                msg = message;
            }

            public override string Message => base.Message;
        }
    
}