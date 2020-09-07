using AuthenticationDomain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AuthenticationDomainTest.Entities
{
    public class CredentialTest
    {
        private const string _username = "Username";
        private const string _password = "Password";

        [Fact]
        public void Create()
        {
            var credentialResult = Credential.Create(_username, _password);

            Assert.True(credentialResult.IsSuccess);
        }

        [Fact]
        public void Create_Success_IsActiveIsAlwaysTrue()
        {
            var credentialResult = Credential.Create(_username, _password);

            Assert.True(credentialResult.IsSuccess);
            Assert.True(credentialResult.Value.IsActive);
        }

        [Fact]
        public void Create_Success_UsernameShouldBeReflected()
        {
            var credentialResult = Credential.Create(_username, _password);

            Assert.True(credentialResult.IsSuccess);
            Assert.Equal(_username, credentialResult.Value.Username);
        }

        [Fact]
        public void Create_Fail_UserNameIsNull()
        {
            var credentialResult = Credential.Create(null, _password);

            Assert.True(credentialResult.IsFailure);
        }

        [Fact]
        public void Create_Fail_UserNameIsEmptyString()
        {
            var credentialResult = Credential.Create(string.Empty, _password);

            Assert.True(credentialResult.IsFailure);
        }

        [Fact]
        public void Create_Success_UsernameIsMinimumCharacter()
        {            
            var credentialResult = Credential.Create("userna", _password);

            Assert.True(credentialResult.IsSuccess);
        }

        [Fact]
        public void Create_Fail_UsernameIsBelowMinimumCharacter()
        {
            var credentialResult = Credential.Create("usern", _password);

            Assert.True(credentialResult.IsFailure);
        }

        [Fact]
        public void Create_Success_UsernameIsMaximumCharacter()
        {
            var credentialResult = Credential.Create("username9012345", _password);

            Assert.True(credentialResult.IsSuccess);
        }

        [Fact]
        public void Create_Fail_UsernameIsAboveMaximumCharacter()
        {
            var credentialResult = Credential.Create("username90123456", _password);

            Assert.True(credentialResult.IsFailure);
        }

        [Fact]
        public void Create_Success_UsernameProceedingUnderscore()
        {
            var credentialResult = Credential.Create("_username", _password);

            Assert.True(credentialResult.IsSuccess);
        }

        [Fact]
        public void Create_Success_UsernameUnderscore()
        {
            var credentialResult = Credential.Create("user_name", _password);

            Assert.True(credentialResult.IsSuccess);
        }

        [Fact]
        public void Create_Success_UsernameFollowingUnderscore()
        {
            var credentialResult = Credential.Create("username_", _password);

            Assert.True(credentialResult.IsSuccess);
        }

        [Fact]
        public void Create_Success_UsernameProceedingNumber()
        {
            var credentialResult = Credential.Create("0username", _password);

            Assert.True(credentialResult.IsSuccess);
        }

        [Fact]
        public void Create_Success_UsernameNumber()
        {
            var credentialResult = Credential.Create("user1name", _password);

            Assert.True(credentialResult.IsSuccess);
        }

        [Fact]
        public void Create_Success_UsernameFollowingNumber()
        {
            var credentialResult = Credential.Create("username5", _password);

            Assert.True(credentialResult.IsSuccess);
        }

        [Fact]
        public void Create_Fail_UsernameHasOtherSpecialCharacters()
        {
            Assert.True(Credential.Create("u`sername", _password).IsFailure);
            Assert.True(Credential.Create("u~sername", _password).IsFailure);
            Assert.True(Credential.Create("u!sername", _password).IsFailure);
            Assert.True(Credential.Create("u@sername", _password).IsFailure);
            Assert.True(Credential.Create("u#sername", _password).IsFailure);
            Assert.True(Credential.Create("u$sername", _password).IsFailure);
            Assert.True(Credential.Create("u%sername", _password).IsFailure);
            Assert.True(Credential.Create("u^sername", _password).IsFailure);
            Assert.True(Credential.Create("u&sername", _password).IsFailure);
            Assert.True(Credential.Create("u*sername", _password).IsFailure);
            Assert.True(Credential.Create("u(sername", _password).IsFailure);
            Assert.True(Credential.Create("u)sername", _password).IsFailure);
            Assert.True(Credential.Create("u-sername", _password).IsFailure);
            Assert.True(Credential.Create("u=sername", _password).IsFailure);
            Assert.True(Credential.Create("u+sername", _password).IsFailure);
            Assert.True(Credential.Create("u[sername", _password).IsFailure);
            Assert.True(Credential.Create("u{sername", _password).IsFailure);
            Assert.True(Credential.Create("u]sername", _password).IsFailure);
            Assert.True(Credential.Create("u}sername", _password).IsFailure);
            Assert.True(Credential.Create("u\\sername", _password).IsFailure);
            Assert.True(Credential.Create("u|sername", _password).IsFailure);
            Assert.True(Credential.Create("u;sername", _password).IsFailure);
            Assert.True(Credential.Create("u:sername", _password).IsFailure);
            Assert.True(Credential.Create("u'sername", _password).IsFailure);
            Assert.True(Credential.Create("u\"sername", _password).IsFailure);
            Assert.True(Credential.Create("u,sername", _password).IsFailure);
            Assert.True(Credential.Create("u<sername", _password).IsFailure);
            Assert.True(Credential.Create("u.sername", _password).IsFailure);
            Assert.True(Credential.Create("u>sername", _password).IsFailure);
            Assert.True(Credential.Create("u/sername", _password).IsFailure);
            Assert.True(Credential.Create("u?sername", _password).IsFailure);
        }

        [Fact]
        public void Create_Success_PasswordShouldBeEncrypted()
        {
            var credentialResult = Credential.Create(_username, _password);

            Assert.True(credentialResult.IsSuccess);
            Assert.NotEqual(credentialResult.Value.Password, _password);
        }

        [Fact]
        public void Create_Fail_PasswordIsNull()
        {
            var credentialResult = Credential.Create(_username, null);

            Assert.True(credentialResult.IsFailure);
        }

        [Fact]
        public void Create_Fail_PasswordIsEmptyString()
        {
            var credentialResult = Credential.Create(_username, string.Empty);

            Assert.True(credentialResult.IsFailure);
        }

        [Fact]
        public void Authenticate_Success()
        {
            var credential = Credential.Create(_username, _password).Value;
            var authenticate = credential.Authenticate(_password);

            Assert.True(authenticate);
        }

        [Fact]
        public void Authenticate_Fail_SuppliedPasswordIsNull()
        {
            var credential = Credential.Create(_username, _password).Value;
            var authenticate = credential.Authenticate(null);

            Assert.False(authenticate);
        }

        [Fact]
        public void Authenticate_Fail_SuppliedPasswordIsEmptyString()
        {
            var credential = Credential.Create(_username, _password).Value;
            var authenticate = credential.Authenticate(string.Empty);

            Assert.False(authenticate);
        }

        [Fact]
        public void Authenticate_Fail_DeactivatedCredential()
        {
            var credential = Credential.Create(_username, _password).Value;
            credential.Deactivate();
            var authenticate = credential.Authenticate(_password);

            Assert.False(authenticate);
        }
    }
}
