namespace LoginAPI.Exceptions
{
    public class UsernameNotProvidedException: Exception
    {
        public UsernameNotProvidedException() { }
        public UsernameNotProvidedException(string message) : base(message) { }
    }
}
