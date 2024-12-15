using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using Domain.Interfaces;

namespace Domain.Repositories;

    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext { get; set; }

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll(List<string> include)
        {
            IQueryable<T> query = this.RepositoryContext.Set<T>();

            if (include != null)
            {
                foreach (string prop in include)
                {
                    query = query.Include(prop);
                }
            }

            query = query.AsNoTracking();

            return query;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, List<string> include)
        {
            IQueryable<T> query = this.RepositoryContext.Set<T>();

            if (include != null)
            {
                foreach (string prop in include) {
                    query = query.Include(prop);
                }
            }

            query = query.Where(expression).AsNoTracking();

            return query;
        }
        
        public IQueryable<T> FindByConditionWithTracking(Expression<Func<T, bool>> expression, List<string> include)
        {
            IQueryable<T> query = this.RepositoryContext.Set<T>();

            if (include != null)
            {
                foreach (string prop in include)
                {
                    query = query.Include(prop);
                }
            }

            query = query.Where(expression);

            return query;
        }

        public int Create(T entity)
        {
            EntityEntry<T> result = this.RepositoryContext.Set<T>().Add(entity);

            try
            {
                return (int)result.Properties.First(ent => ent.Metadata.IsPrimaryKey()).CurrentValue;
            }
            catch
            {
                return 0;
            }
        }

        public void Update(T entity)
        {
            T existingEntity = this.RepositoryContext.Set<T>().Find(RepositoryContext.Entry(entity).Property("Id").CurrentValue);
            if (existingEntity != null)
            {
                RepositoryContext.Entry(existingEntity).State = EntityState.Detached;
            }
            this.RepositoryContext.Set<T>().Update(entity);
        }
        
        public void UpdateById(int id, T entity)
        {
            T existingEntity = this.RepositoryContext.Set<T>().Find(id);
            if (existingEntity != null)
            {
                RepositoryContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                throw new Exception($"Entity with ID { id } not found.");
            }
        }

        public void Delete(T entity)
        {
            T existingEntity = this.RepositoryContext.Set<T>().Find(RepositoryContext.Entry(entity).Property("Id").CurrentValue);
            if (existingEntity != null)
            {
                RepositoryContext.Entry(existingEntity).State = EntityState.Detached;
            }
            this.RepositoryContext.Set<T>().Remove(entity);
        }

        public void Delete(int id)
        {
            T entity = this.RepositoryContext.Set<T>().Find(id);
            if (entity != null)
            {
                RepositoryContext.Entry(entity).State = EntityState.Detached;
                this.RepositoryContext.Set<T>().Remove(entity);
            }
        }
        
        public void RemoveRange(IEnumerable<T> entities)
        {
            if (entities == null || !entities.Any()) return;

            foreach (var entity in entities)
            {
                if (this.RepositoryContext.Entry(entity).State == EntityState.Detached)
                {
                    this.RepositoryContext.Set<T>().Attach(entity);
                }
            }

            this.RepositoryContext.Set<T>().RemoveRange(entities);
        }
    }
