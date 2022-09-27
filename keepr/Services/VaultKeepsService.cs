using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vaultKeepsRepo;
        private readonly VaultsService _vaultService;
        private readonly KeepsService _keepService;

        public VaultKeepsService(VaultKeepsRepository vaultKeepsRepo, VaultsService vaultService, KeepsService keepService)
        {
            _vaultKeepsRepo = vaultKeepsRepo;
            _vaultService = vaultService;
            _keepService = keepService;
        }

        //creates many to many relationship
        internal VaultKeep Create(VaultKeep newVaultKeep, string userId)
        {
            //checks user
            Vault vault = _vaultService.GetById(newVaultKeep.VaultId);
            if(vault.CreatorId != userId)
            {
                throw new Exception ("You are not the owner of this vault");
            }
            //creates a vault keep
            int id = _vaultKeepsRepo.Create(newVaultKeep);
            //Adds one kept count
            _keepService.AddKept(newVaultKeep);
            //gets original keep
            VaultKeepViewModel vaultKeep = _keepService.GetViewModelById(newVaultKeep.KeepId);
            // attaches vaultkeep id to the keep for the view model
            vaultKeep.VaultKeepId = id;
            newVaultKeep.Id = id;
            // returns to the user
            return newVaultKeep;
        }
        // deletes the keep from the vault
        internal string Delete(int id, Account user)
        {
            VaultKeepViewModel original = _keepService.GetViewModelById(id);
            if (original.CreatorId != user.Id){
                throw new Exception($"You cant remove that");
            }
            _vaultKeepsRepo.Delete(id);
            return $"keep was removed from the vault";

        }

        // Get keeps by vault id
        internal List<VaultKeepViewModel> GetKeepsByVaultId(int vaultId)
        {
            return _vaultKeepsRepo.GetKeepsByVaultId(vaultId);
        }
    }
}