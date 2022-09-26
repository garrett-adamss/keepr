using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _vaultsRepo;

        public VaultsService(VaultsRepository vaultsRepo)
        {
            _vaultsRepo = vaultsRepo;
        }

        internal Vault Create(Vault newVault, Account user)
        {
            return _vaultsRepo.Create(newVault);
        }

        // internal List<Vault> GetAll()
        // {
        //     return _vaultsRepo.GetAll();
        // }
    }
}