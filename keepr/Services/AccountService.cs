using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
    public class AccountService
    {
        private readonly AccountsRepository _repo;
        public AccountService(AccountsRepository repo)
        {
            _repo = repo;
        }

        internal string GetProfileEmailById(string id)
        {
            return _repo.GetById(id).Email;
        }
        internal Account GetProfileByEmail(string email)
        {
            return _repo.GetByEmail(email);
        }
        internal Account GetOrCreateProfile(Account userInfo)
        {
            Account profile = _repo.GetById(userInfo.Id);
            if (profile == null)
            {
                return _repo.Create(userInfo);
            }
            return profile;
        }

        internal Account Edit(Account editData, string userEmail)
        {
            Account original = GetProfileByEmail(userEmail);
            original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
            original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
            return _repo.Edit(original);
        }


        internal Account GetOne(string user)
        {
            return _repo.GetById(user);
        }

        internal List<Keep> GetKeeps(string id)
        {
            Account profile = GetOne(id);
            return _repo.GetKeepsByUserId(id);
        }

        internal List<Vault> GetVaults(string profileId, Account user)
        {
            // Account profile = GetOne(id);
            //Find all method on list vaults, find all vaults that are not private
            List<Vault> vaults = _repo.GetVaultsByUserId(profileId);

            if(user == null)
            {
            vaults = vaults.FindAll(v => v.IsPrivate == false);
            }

            
            return vaults;

            
        }
                internal List<Vault> GetVaultsAccount(string userId)
        {
            // Account profile = GetOne(id);
            //Find all method on list vaults, find all vaults that are not private
            List<Vault> vaults = _repo.GetVaultsByUserId(userId);

            // if(vault.CreatorId ==)
            return vaults;
            
            // return filteredVaults;

            
        }
    }
}