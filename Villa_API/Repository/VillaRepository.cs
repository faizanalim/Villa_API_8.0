
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Villa_API.Data;
using Villa_API.Models;
using Villa_API.Repository;
using Villa_API.Repository.IRepository;

namespace MagicVilla_VillaAPI.Repository
{
     public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }



        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Villas.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}