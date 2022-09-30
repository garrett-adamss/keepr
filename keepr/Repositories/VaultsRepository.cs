using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Vault Create(Vault vaultData)
        {
            string sql = @"
            INSERT INTO vaults
            (creatorId, name, description, IsPrivate)
            VALUES
            (@creatorId, @name, @description, @IsPrivate);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, vaultData);
            vaultData.Id = id;
            return vaultData;

        }

        internal Vault GetOne(int id)
        {
            string sql = @"
            SELECT 
             v.*,
             a.*
             FROM vaults v
             JOIN accounts a on v.creatorId = a.id
             WHERE v.id = @id
            ";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile)=>
            {
                vault.Creator = profile;
                return vault;
            }, new { id }).FirstOrDefault();
        }

        internal Vault Update(Vault update)
        {
            string sql = @"
            UPDATE vaults SET 
            name = @name,
            description = @description,
            isPrivate = @isPrivate
            WHERE id = @id;
            ";
            _db.Execute(sql, update);
            return update;
        }

        internal Vault GetById(int id)
        {
            string sql = @"
            SELECT * FROM vaults
            WHERE id = @id;
            ";
            Vault vault = _db.Query<Vault>(sql, new { id }).FirstOrDefault();
            return vault;
        }

        internal void Delete(int id)
        {
            string sql = @"
                DELETE FROM vaults WHERE id = @id;
            ";
            _db.Execute(sql, new{ id });
        }
    }
}