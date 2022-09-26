using System.Data;
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
            (creatorId, name, description, isPrivate)
            VALUES
            (@creatorId, @name, @description, @isPrivate);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, vaultData);
            vaultData.Id = id;
            return vaultData;

        }

        // internal List<Vault> GetAll()
        // {
        //     string sql = @"
        //     SELECT
        //     v.*,
        //     a.*
        //     FROM vault v
        //     JOIN accounts a ON v.creatorId = a.id
        //     GROUP BY (v.id)
        //     ";
        //     return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        //     {
        //         vault.Creator = profile;
        //         return vault;
        //     }).ToList();
        // }
    }
}