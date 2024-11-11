namespace QuizzApplicationBackend.Exceptions
{
    public class CouldNotAddException:Exception
    {
        string msg;

        public CouldNotAddException(string msg) {
            msg = msg;
        }

        public override string  Message => base.Message;
    }
}
