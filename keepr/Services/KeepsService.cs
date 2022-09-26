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
        internal Keep Create(Keep keepData, Account user)
        {
            return _keepsRepo.Create(keepData);  
        }

        internal List<Keep> GetAll()
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
                throw new Exception($"You are not the creator of this {original.Name}");
            }
            original.Name = update.Name ?? original.Name;
            original.Description = update.Description ?? original.Description;
            original.Img = update.Img ?? original.Img;

            return _keepsRepo.Update(original);
        }

        internal string Delete(int id, Account user)
        {
            Keep original = GetOne(id, user.Id);
            if(original.CreatorId != user.Id)
            {
                throw new Exception($"cannot delete {original.Name}, it's not yours");
            }
            _keepsRepo.Delete(id);
            return $"{original.Name} was deleted.";
        }
    }
}