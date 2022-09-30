using System;
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

        internal Vault Create(Vault newVault)
        {
            return _vaultsRepo.Create(newVault);
        }

        internal Vault GetOne(int id, string userId)
        {
            Vault vault = _vaultsRepo.GetOne(id);

            if (vault == null)
            {
                if (vault.IsPrivate == true)
                {
                    throw new Exception($"No vault at: id = {id} ");
                }
                return vault;
            }
                if (vault.IsPrivate == true && vault.CreatorId != userId)
                {
                    throw new Exception("You don't have access to that vault");
                }
            return vault;
        }

        internal Vault Update(Vault update, Account user)
        {
            Vault original = GetOne(update.Id, user.Id);
            if (original.CreatorId != user.Id)
            {
                throw new Exception($"Can not update {original.Name} you are not the creator");
            }
            original.Name = update.Name ?? original.Name;
            original.Description = update.Description ?? original.Description;
            original.IsPrivate = update.IsPrivate ?? original.IsPrivate;

            return _vaultsRepo.Update(original);
        }

        internal Vault GetById(int id)
        {
            Vault vault = _vaultsRepo.GetById(id);
            if (vault.IsPrivate == true || vault == null)
            {
                throw new Exception("sorry that vault doesnt exist");
            }
            return vault;
        }

        internal string Delete(int id, Account user)
        {
            Vault original = GetOne(id, user.Id);
            if (original.CreatorId != user.Id)
            {
                throw new Exception($"You can't delete {original.Name}, you're not the owner");
            }
            _vaultsRepo.Delete(id);
            return $"Deleted {original.Name}.";
        }
    }
}