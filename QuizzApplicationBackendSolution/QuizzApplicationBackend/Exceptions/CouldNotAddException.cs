namespace TestProject1
{
    public class CouldNotAddException:Exception
    {
        string message = string.Empty;

        public CouldNotAddException(string msg)
        {
            message = msg;
        }

        public override string Message => message;
    }
}