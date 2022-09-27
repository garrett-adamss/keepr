using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;

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
        internal int Create(VaultKeep newVaultKeep)
        {
            string sql = @"
                INSERT INTO vaultkeeps
                (creatorId, vaultId, keepId)
                VALUES
                (@creatorId, @vaultId, @keepId);
                SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
            return id;
        }
        //gets keeps by vault id
        internal List<VaultKeepViewModel> GetKeepsByVaultId(int vaultId)
        {
            string sql = @"
                SELECT 
                vk.*,
                k.*,
                a.*,
                FROM vaultkeeps vk
                JOIN keeps k ON vk.keepId = k.id
                JOIN accounts a ON k.creatorId = a.id
                WHERE vk.vaultId = @vaultId;
            ";
            List<VaultKeepViewModel> keeps = _db.Query<VaultKeep, VaultKeepViewModel, Account, VaultKeepViewModel>(sql, (vk, k, a)=>
            {
                k.CreatorId = a;
                k.VaultKeepId = vk.Id;
                return k;
            }, new { vaultId }).ToList();
            return keeps;
        }

        internal void Delete(int id)
        {
            string sql =@"
            DELETE FROM vaultkeeps WHERE id = @id;
            ";
            _db.Execute(sql, new { id });
        }
    }
}