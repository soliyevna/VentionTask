using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VentionTask.Domain.Models;

namespace VentionTask.DAL.Interfaces;
public interface IRepository<TEntity> where TEntity : User
{
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression = null);
    IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null, bool isTracking = true);
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> UpdateAsync(int id, TEntity entity);
    public void DeleteAsync(int id);
    Task<bool> SaveAsync();
    public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

}
