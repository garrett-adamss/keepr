using System.Data;
using keepr.Models;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace keepr.Repositories
{
    public class AccountsRepository
    {
        private readonly IDbConnection _db;

        public AccountsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Account GetByEmail(string userEmail)
        {
            string sql = "SELECT * FROM accounts WHERE email = @userEmail";
            return _db.QueryFirstOrDefault<Account>(sql, new { userEmail });
        }

        internal Account GetById(string id)
        {
            string sql = "SELECT * FROM accounts WHERE id = @id";
            return _db.QueryFirstOrDefault<Account>(sql, new { id });
        }

        internal Account Create(Account newAccount)
        {
            string sql = @"
            INSERT INTO accounts
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)";
            _db.Execute(sql, newAccount);
            return newAccount;
        }

        internal Account Edit(Account update)
        {
            string sql = @"
            UPDATE accounts
            SET 
              name = @Name,
              picture = @Picture
            WHERE id = @Id;";
            _db.Execute(sql, update);
            return update;
        }

        internal List<Keep> GetKeepsByUserId(string id)
        {
            string sql = @"
            SELECT 
            k.*,
            a.*
            FROM keeps k 
            JOIN accounts a ON k.creatorId = a.id
            WHERE k.creatorId = @id
            ";
            return _db.Query<Keep, Profile, Keep>(sql, (k, profile)=>
            {
                k.Creator = profile;
                return k;
            }, new { id }).ToList();
        }
        internal List<Vault> GetVaultsByUserId(string id)
        {
            string sql = @"
            SELECT
            v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId= a.id
            WHERE v.creatorId = @id
            ";
            return _db.Query<Vault, Profile, Vault>(sql, (v, profile)=>
            {
                v.Creator = profile;
                return v;
            },new { id }).ToList();
        }

    }
}
