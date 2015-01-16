using System;

namespace BetfairAPING.Authentication
{
    public class LoginCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string CertPath { get; set; }
        public string AppKey { get; set; }
        public string CredentialStore { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Username))
                throw new NullReferenceException("Username");
            if (string.IsNullOrEmpty(Password))
                throw new NullReferenceException("Password");
            if (string.IsNullOrEmpty(CertPath))
                throw new NullReferenceException("CertPath");
            if (string.IsNullOrEmpty(AppKey))
                throw new NullReferenceException("AppKey");
        }
    }
}