using System;
using System.Collections.Generic;
using System.Data;
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
            throw new NotImplementedException();
        }

        internal Keep GetOne(int id, string userId)
        {
            throw new NotImplementedException();
        }

        internal void Update(Keep keep)
        {
            throw new NotImplementedException();
        }

    }
}