using System.Linq.Expressions;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebApp.Models.Interfaces
{
    public interface IRepository
    {
        T Load<T>(params object[] keys) where T : class;
        ValueTask<T> LoadAsNoTrackingAsync<T>(int key) where T : class, IBaseEntity;
        ValueTask<T> LoadAsync<T>(params object[] keys) where T : class;
        ValueTask<T> LoadAsyncCondition<T>(Expression<Func<T, bool>> expression) where T : class;
        IQueryable<T> Collection<T>(Expression<Func<T, bool>> expression) where T : class;
        IQueryable<T> Collection<T>() where T : class;
        Task<IEnumerable<T>> CollectionAsync<T>(Expression<Func<T, bool>> expression) where T : class;
        Task<IEnumerable<T>> CollectionAsync<T>() where T : class;
        Task<IEnumerable<T>> CollectionAsNoTrackingAsync<T>(Expression<Func<T, bool>> expression) where T : class;
        Task<IEnumerable<T>> CollectionAsNoTrackingAsync<T>() where T : class;
        Task<object[]> AddAsync<T>(T element) where T : class;
        Task RemoveAsync<T>(T element) where T : class;
        Task RemoveRangeAsync<T>(IEnumerable<T> element) where T : class;
        Task UpdateAsync<T>(T element) where T : class;
    }
}