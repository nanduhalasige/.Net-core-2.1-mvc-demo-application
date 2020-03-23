using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApplication.MVC.Models.Repositories.GenericRepo
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly StudentDbContext context;
        public Repository(StudentDbContext _context)
        {
            context = _context;
        }
        public async Task<bool> Add(T entity)
        {
            try
            {
                context.Set<T>().Add(entity);
                await context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> AddRange(List<T> entities)
        {
            try
            {
                context.Set<T>().AddRange(entities);
                await context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> HardDelete(Guid id)
        {
            var result = await GetById(id);
            try
            {
                context.Remove(result);
                await context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<List<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<bool> Update(T entity)
        {
            try
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public bool Exists(Guid id)
        {
            var existCount = context.Set<T>().FindAsync(id);
            return existCount != null ? true : false;
        }
    }
}
