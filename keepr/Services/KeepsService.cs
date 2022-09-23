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
            Keep keep = _keepsRepo.GetOne(id);
            if (keep == null)
            {
                throw new Exception("no keep by that id");
            }
            keep.Views++;
            _keepsRepo.Update(keep);
            return keep;
        }

        internal Keep Update(Keep update, Account user)
        {
            Keep original = GetOne(update.Id, user.Id);
            if (original.CreatorId != user.Id)
            {
                throw new Exception("You are not hte creator of this keep");
            }
            original.Name = update.Name ?? original.Name;
            original.Description = update.Description ?? original.Description;
            original.Img = update.Img ?? original.Img;
            original.Views = update.Views ?? original.Views;
            original.Kept = update.Kept ?? original.Kept;
            
        }

        // Functions Start Here
    }
}