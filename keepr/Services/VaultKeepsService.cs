using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vaultKeepsRepo;
        private readonly VaultsService _vaultService;
        private readonly KeepsService _keepService;
        private readonly VaultsRepository _vaultRepo;

        public VaultKeepsService(VaultKeepsRepository vaultKeepsRepo, VaultsService vaultService, KeepsService keepService, VaultsRepository vaultRepo)
        {
            _vaultKeepsRepo = vaultKeepsRepo;
            _vaultService = vaultService;
            _keepService = keepService;
            _vaultRepo = vaultRepo;
        }



        //creates many to many relationship
        internal VaultKeep Create(VaultKeep newVaultKeep)
        {
            //checks user
            Vault original = _vaultRepo.GetOne(newVaultKeep.VaultId);
            if(original.CreatorId != newVaultKeep.CreatorId)
            {
                throw new Exception ("You are not the owner of this vault");
            }
            //Adds one kept count
            _keepService.AddKept(newVaultKeep);
            return _vaultKeepsRepo.Create(newVaultKeep);
        }
        




        // deletes the keep from the vault
        // internal ActionResult<string> Delete(int id, Account user)
        // {
        //     VaultKeep original = _vaultKeepsRepo.GetById(id);
        //     if (original.CreatorId != user.Id){
        //         throw new Exception($"You cant remove that");
        //     }

        //     return _vaultKeepsRepo.Delete(id);
            
        // }
        internal ActionResult<string> Delete(int id, Account user) 
        { 
            VaultKeep original = _vaultKeepsRepo.GetById(id); 
            if (original.CreatorId != user.Id) 
            { 
                throw new Exception("You cannot remove that"); 
            } 
            return _vaultKeepsRepo.Delete(id); 
            
        } 

        // Get keeps by vault id
        internal List<VaultKeepViewModel> GetKeepsByVaultId(int id, string userId)
        {
            Vault original = _vaultService.GetOne(id, userId);
            List<VaultKeepViewModel> keeps = _vaultKeepsRepo.GetKeepsByVaultId(id);
            return keeps;
        }
    }
}