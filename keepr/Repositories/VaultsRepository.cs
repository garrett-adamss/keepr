using System.Collections.Generic;
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

        internal List<Vault> GetAll()
        {
            string sql = @"
            SELECT
            v.*,
            a.*
            FROM vault v
            JOIN accounts a ON v.creatorId = a.id
            GROUP BY (v.id)
            ";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.Creator = profile;
                return vault;
            }).ToList();
        }
    }
}