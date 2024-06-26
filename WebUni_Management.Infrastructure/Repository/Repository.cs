﻿using Microsoft.EntityFrameworkCore;
using WebUni_Management.Data;

namespace WebUni_Management.Infrastructure.Repository
{
	public class Repository : IRepository
    {
        private readonly ApplicationDbContext context;

        public Repository(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task AddAsync<T>(T entity) where T : class
        {
            await context.Set<T>().AddAsync(entity);
        }
        public IQueryable<T> All<T>() where T : class
        {
            return GetDbSet<T>();
        }

        private DbSet<T> GetDbSet<T>() where T : class
        {
            return context.Set<T>();
        }

        public IQueryable<T> AllReadOnly<T>() where T : class
        {
            return GetDbSet<T>().AsNoTracking();
        }

        public async Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            await GetDbSet<T>().AddRangeAsync(entities);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

		public async Task<T?> GetById<T>(object id) where T : class
		{
			return await GetDbSet<T>().FindAsync(id);
		}

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            GetDbSet<T>().Remove(entity);

            await SaveChangesAsync();
        }
    }
}
