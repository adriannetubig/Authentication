using AuthenticationDomain.Entities;

namespace AuthenticationRepository.Interfaces
{
    public interface ICredentialRepository
    {
        Credential RetrieveByUsername(string username);
    }
}
