namespace LoginAPI.Exceptions
{
    public class OldPasswordIncorrectException: Exception
    {
        public OldPasswordIncorrectException() { }

        public OldPasswordIncorrectException(string message): base(message) { }

    }
}
