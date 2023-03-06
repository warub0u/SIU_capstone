namespace LoginAPI.Exceptions
{
    public class UserIsBlockedException: Exception
    {
        public UserIsBlockedException() { }
        public UserIsBlockedException(string message) : base(message) { }
    }
}
