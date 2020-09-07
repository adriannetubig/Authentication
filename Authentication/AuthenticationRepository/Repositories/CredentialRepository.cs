using AuthenticationDomain.Entities;
using AuthenticationRepository.Interfaces;
using System;
using System.Linq;

namespace AuthenticationRepository.Repositories
{
    public class CredentialRepository: ICredentialRepository
    {
        private readonly AuthenticationContext _authenticationContext;

        public CredentialRepository(AuthenticationContext authenticationContext)
        {
            if (authenticationContext == null)
                throw new ArgumentNullException("AuthenticationContext Required");

            _authenticationContext = authenticationContext;
        }

        public Credential RetrieveByUsername(string username)
        {
            return _authenticationContext.Credentials.FirstOrDefault(a => a.Username == username);
        }
    }
}
