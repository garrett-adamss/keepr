using System;
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
                GROUP BY (k.id)
            ";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile)=>
            {
                keep.Creator = profile;
                return keep;
            }, new { id }).FirstOrDefault();
        }   

        internal void Update(Keep keep)
        {
            throw new NotImplementedException();
        }

    }
}