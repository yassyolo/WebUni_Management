﻿namespace WebUni_Management.Infrastructure.Repository
{
	public interface IRepository
    {
        IQueryable<T> All<T>() where T : class;

        IQueryable<T> AllReadOnly<T>() where T : class;

        Task AddAsync<T>(T entity) where T : class;

        Task<int> SaveChangesAsync();

		Task<T?> GetById<T>(object id) where T : class;

        Task DeleteAsync<T>(T entity) where T : class;
    }
}
