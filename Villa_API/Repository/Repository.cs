﻿using Microsoft.EntityFrameworkCore;
using static Villa_API.Repository.IRepository.IRepository;
using System.Linq.Expressions;
using System.Linq;
using Villa_API.Data;

namespace Villa_API.Repository
{
   
        public class Repository<T> : IRepository<T> where T : class
        {
            private readonly ApplicationDbContext _db;
		 
		internal DbSet<T> dbSet;
            public Repository(ApplicationDbContext db)
            {
                _db = db;
			//_db.VillaNumbers.Include(u => u.Villa).ToList();
			this.dbSet = _db.Set<T>();
            }
            public async Task CreateAsync(T entity)
            {
                await dbSet.AddAsync(entity);
                await SaveAsync();
            }
		//"Villa,VillaSpecial"
		public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null)
        { 
				IQueryable<T> query = dbSet;
                if (!tracked)
                {
                    query = query.AsNoTracking();
                }
                if (filter != null)
                {
                    query = query.Where(filter);
                }
			if (includeProperties != null)
			{
				foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
			return await query.FirstOrDefaultAsync();
            }
		public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
		{
                IQueryable<T> query = dbSet;
                if (filter != null)
                {
                    query = query.Where(filter);
                }
			if (includeProperties != null)
			{
				foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
			return await query.ToListAsync();
            }
            public async Task RemoveAsync(T entity)
            {
                dbSet.Remove(entity);
                await SaveAsync();
            }
            public async Task SaveAsync()
            {
                await _db.SaveChangesAsync();
            }

        }
    }

