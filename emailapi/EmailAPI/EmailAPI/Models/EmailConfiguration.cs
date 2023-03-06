﻿namespace EmailAPI.Models
{
    public class EmailConfiguration
    {
        // DATA MODEL FOR EMAIL CONFIGURATION

        public string From { get; set; } = null!;
        public string SmtpServer { get; set; } = null!;
        public int Port { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

    }
}
