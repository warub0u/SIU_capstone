namespace LoginAPI.Models
{
    public class UpdateUserPW
    {
        // DATA MODEL FOR REQUEST
        public string UserName { get; set; }

        public string Old_pw { get; set; }

        public string New_pw { get; set; }

    }
}
