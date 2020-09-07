using AuthenticationRepository;
using System;
using Xunit;

namespace AuthenticationRepositoryTest
{
    public class AuthenticationContextTest
    {
        [Fact]
        public void Constructor_Success()
        {
            new AuthenticationContext("ConnectionString", true);
        }

        [Fact]
        public void Constructor_Fail_ConnectionStringIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AuthenticationContext(null, true));
        }

        [Fact]
        public void Constructor_Fail_ConnectionStringIsEmptyString()
        {
            Assert.Throws<ArgumentNullException>(() => new AuthenticationContext(string.Empty, true));
        }

        [Fact]
        public void Constructor_Fail_UseConsoleLoggerIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AuthenticationContext("ConnectionString", null));
        }
    }
}
