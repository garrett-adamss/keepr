using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }
        // Functions start here

        internal List<Keep> GetAll()
        {
            string sql = @"
                SELECT 
                k.*,
                a.*
                FROM keeps k
                JOIN accounts a ON k.creatorId = a.id
                GROUP BY (k.id)
            ";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }).ToList();

        }

        internal Keep Create(Keep keepData)
        {
            string sql = @"
            INSERT INTO keeps
            (creatorId, name, description, img)
            VALUES
            (@creatorId, @name, @description, @img);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, keepData);
            keepData.Id = id;
            return keepData;
        }

        // TODO test
        internal Keep GetOne(int id)
        { 
            string sql = @"
                SELECT 
                k.*,
                a.*
                FROM keeps k
                JOIN accounts a ON k.creatorId = a.id
                WHERE k.id = @id
            ";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile)=>
            {
                keep.Creator = profile;
                return keep;
            }, new { id }).FirstOrDefault();
        }

        internal VaultKeepViewModel GetViewModelById(int id)
        {
            string sql = @"
            SELECT * FROM keeps
            WHERE id = @id;
            ";
            VaultKeepViewModel keep = _db.Query<VaultKeepViewModel>(sql, new { id }).FirstOrDefault();
            return keep;
        }

        internal Keep Update(Keep update)
        {   
            string sql = @"
            UPDATE keeps SET
            name = @name,
            description = @description,
            img = @img,
            views = @views,
            kept = @kept
            WHERE id = @id;
            ";
            _db.Execute(sql, update);
            return update;
        }

        internal void Delete(int id)
        {
            string sql = @"
            DELETE FROM keeps WHERE id = @id;
            ";
            _db.Execute(sql, new { id });
        }
    }
}