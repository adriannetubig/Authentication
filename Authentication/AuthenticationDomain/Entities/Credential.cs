using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace AuthenticationDomain.Entities
{
    public class Credential: Entity
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool IsActive { get; private set; }

        protected Credential()
        {
        }

        private Credential(string username, string password)
        {
            Username = username;
            Password = BCrypt.Net.BCrypt.HashPassword(password);
            IsActive = true;
        }

        public static Result<Credential> Create(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                return Result.Failure<Credential>("Username Required");
            else if (username.Length < 6)
                return Result.Failure<Credential>("Minimum of 6 characters for Username");
            else if (username.Length > 15)
                return Result.Failure<Credential>("Maximum of 15 characters for Username");
            else if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"))
                return Result.Failure<Credential>("Alphanumeric characters and underscore are allowed for Username");

            if (string.IsNullOrEmpty(password))
                return Result.Failure<Credential>("Password Required");

            return Result.Success(new Credential(username, password));

        }

        public bool Authenticate(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            if (!IsActive)
                return false;

            return BCrypt.Net.BCrypt.Verify(password, Password);
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
