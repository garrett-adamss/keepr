using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        // creates a many to many relationship  
        internal VaultKeep Create(VaultKeep newVaultKeep)
        {
            string sql = @"
                INSERT INTO vaultkeeps
                (creatorId, vaultId, keepId)
                VALUES
                (@creatorId, @vaultId, @keepId);
                SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
            newVaultKeep.Id = id;
            return newVaultKeep;
        }
        //gets keeps by vault id
        internal List<VaultKeepViewModel> GetKeepsByVaultId(int vaultId)
        {
            string sql = @"
                SELECT 
                vk.*,
                k.*,
                a.*
                FROM vaultkeeps vk
                JOIN keeps k ON vk.keepId = k.id
                JOIN accounts a ON k.creatorId = a.id
                WHERE vk.vaultId = @vaultId;
            ";
            List<VaultKeepViewModel> keeps = _db.Query<VaultKeep, VaultKeepViewModel, Account, VaultKeepViewModel>(sql, (vk, k, a) =>
            {
                k.Creator = a;
                k.VaultKeepId = vk.Id;
                return k;
            }, new { vaultId }).ToList();
            return keeps;
        }
        internal VaultKeep GetById(int id)
        {
            string sql = @" 
            SELECT * FROM vaultKeeps 
            WHERE id = @id; 
            ";
            VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { id }).FirstOrDefault();
            return vaultKeep;
        }

        internal ActionResult<string> Delete(int id)
        {
            string sql = @"
            DELETE FROM vaultkeeps WHERE id = @id;
            ";
            _db.Execute(sql, new { id });
            return "deleted";
        }
        }
}