using AuthenticationRepository;
using AuthenticationRepository.Repositories;
using System;
using Xunit;

namespace AuthenticationRepositoryTest.Repositories
{
    public class CredentialRepositoryTest
    {
        [Fact]
        public void Constructor_Success()
        {
            var authenticationContext = new AuthenticationContext("connectionString", true);
            new CredentialRepository(authenticationContext);
        }

        [Fact]
        public void Constructor_Fail_ContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new CredentialRepository(null));
        }
    }
}
