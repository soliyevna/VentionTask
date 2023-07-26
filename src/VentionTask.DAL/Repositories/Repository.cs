using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VentionTask.DAL.AppDbContexts;
using VentionTask.DAL.Interfaces;
using VentionTask.Domain.Models;
namespace VentionTask.DAL.Repositories;
public class Repository<TEntity> : IRepository<TEntity> where TEntity : User
{
    protected readonly AppDbContext appDbContext;
    protected DbSet<TEntity> dbSet;
    public Repository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
        dbSet = this.appDbContext.Set<TEntity>();
    }
    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        EntityEntry<TEntity> entry = await dbSet.AddAsync(entity);
        await appDbContext.SaveChangesAsync();
        return entry.Entity;
    }
    public async void DeleteAsync(int id)
    {
        var entity = await dbSet.FindAsync(id);
        if (entity is not null)
            dbSet.Remove(entity);
    }
    public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null, bool isTracking = true)
    {
        IQueryable<TEntity> dbQuery = expression is null ? dbSet : dbSet.Where(expression);
        if (!isTracking)
        {
            dbQuery = dbQuery.AsNoTracking();
        }
        return dbQuery;
    }
    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression = null)
    {
        IQueryable<TEntity> dbQuery = expression is null ? dbSet : dbSet.Where(expression);
        return await dbQuery.FirstOrDefaultAsync();
    }
    public virtual async Task<TEntity> UpdateAsync(int id, TEntity entity)
    {
        entity.Id = id;
        appDbContext.Update(entity);
        return entity;
    }
    public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
    {
        return appDbContext.Entry(entity);
    }
    public async Task<bool> SaveAsync()
    {
        return await appDbContext.SaveChangesAsync() > 0;
    }
}