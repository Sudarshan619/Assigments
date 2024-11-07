namespace QuizzApplicationBackend.Exceptions
{
    public class NotFoundException:Exception
    {
        string message = string.Empty;

        public NotFoundException(string msg) {
            message = msg;
        }

        public override string Message => message;
    }
}
