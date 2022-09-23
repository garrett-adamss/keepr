using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _keepsRepo;

        public KeepsService(KeepsRepository keepsRepo)
        {
            _keepsRepo = keepsRepo;
        }

        internal List<Keep> GetAll(string id)
        {
            return _keepsRepo.GetAll();
        }

        internal Keep GetOne(int id, string userId)
        {
            Keep keep = _keepsRepo.GetOne(id, userId);
            if(keep == null){
                throw new Exception("no keep by that id");
            }
            keep.Views++;
            _keepsRepo.Update(keep);
            return keep;
        }

        // Functions Start Here
    }
}