using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.Contracts;

namespace Persistence.Repositories;

public abstract class RepositoryBase<T>(QotdContext qotdContext) : IRepositoryBase<T> where T : class
{
    protected QotdContext QotdContext = qotdContext;

    public void Create(T entity) => QotdContext.Set<T>().Add(entity);
    public void Delete(T entity) => QotdContext.Set<T>().Remove(entity);
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => QotdContext.Set<T>().Where(expression);
    public IQueryable<T> GetAll(bool trackChanges)
    {
        return !trackChanges
            ? QotdContext.Set<T>().AsNoTracking()
            : QotdContext.Set<T>();
    }

    public void Update(T entity) => QotdContext.Set<T>().Update(entity);
}