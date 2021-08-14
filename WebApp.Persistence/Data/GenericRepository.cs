using System;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebApp.Data
{
    public class GenericRepository : IRepository
    {
        private readonly WebAppContext context;
        public GenericRepository(WebAppContext context)
        {
            this.context = context;
        }

        public IQueryable<T> Collection<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return context.Set<T>().Where(expression);
        }

        public IQueryable<T> Collection<T>() where T : class
        {
            return context.Set<T>();
        }

        public async Task<IEnumerable<T>> CollectionAsync<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return await context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> CollectionAsync<T>() where T : class
        {
            return await context.Set<T>().ToListAsync();
        }
        public async Task<IEnumerable<T>> CollectionAsNoTrackingAsync<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return await context.Set<T>().AsNoTracking().Where(expression).ToListAsync();
        }
        public async Task<IEnumerable<T>> CollectionAsNoTrackingAsync<T>() where T : class
        {
            return await context.Set<T>().AsNoTracking().ToListAsync();
        }

        public T Load<T>(params object[] keys) where T : class
        {
            return context.Set<T>().Find(keys);
        }
        public async ValueTask<T> LoadAsNoTrackingAsync<T>(int key) where T : class, IBaseEntity
        {
            return await context.Set<T>().AsNoTracking().FirstAsync(x => x.Id == key);
        }
        public async ValueTask<T> LoadAsync<T>(params object[] keys) where T : class
        {
            return await context.Set<T>().FindAsync(keys);
        }
        public async ValueTask<T> LoadAsyncCondition<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return await context.Set<T>().Where(expression).SingleOrDefaultAsync();
        }

        public async Task<object[]> AddAsync<T>(T element) where T : class
        {
            if (element == null)
                throw new ArgumentNullException("entity");

            EntityEntry<T> entityEntry = await context.Set<T>().AddAsync(element);
            await context.SaveChangesAsync();

            object[] primaryKeyValue = entityEntry.Metadata.FindPrimaryKey().Properties
            .Select(p => entityEntry.Property(p.Name).CurrentValue).ToArray();

            return primaryKeyValue;
        }

        public async Task RemoveAsync<T>(T element) where T : class
        {
            if (element == null)
                throw new ArgumentNullException("entity");

            context.Set<T>().Remove(element);
            await context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync<T>(IEnumerable<T> elements) where T : class
        {
            if (!elements.Any())
                throw new ArgumentNullException("entity");

            context.Set<T>().RemoveRange(elements);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync<T>(T element) where T : class
        {
            if (element == null)
                throw new ArgumentNullException("entity");

            context.Set<T>().Update(element);
            await context.SaveChangesAsync();
        }
    }
}