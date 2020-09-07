using AuthenticationRepository.Interfaces;
using System;

namespace AuthenticationService.Services
{
    public class CredentialService
    {
        private readonly ICredentialRepository _iCredentialRepository;
        public CredentialService(ICredentialRepository iCredentialRepository)
        {
            if (iCredentialRepository == null)
                throw new ArgumentNullException("ICredentialRepository Required");

            _iCredentialRepository = iCredentialRepository;
        }
    }
}
