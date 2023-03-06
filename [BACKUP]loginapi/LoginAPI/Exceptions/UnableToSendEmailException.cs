namespace LoginAPI.Exceptions
{
    public class UnableToSendEmailException: Exception
    {
        public UnableToSendEmailException() { }
        public UnableToSendEmailException(string message):base(message) { }

    }
}
