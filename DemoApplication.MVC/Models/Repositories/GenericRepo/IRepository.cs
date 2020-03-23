using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoApplication.MVC.Models.Repositories.GenericRepo
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<bool> Add(T entity);
        Task<bool> AddRange(List<T> entities);
        Task<bool> Update(T entity);
        Task<bool> HardDelete(Guid id);
        bool Exists(Guid id);
    }  
}
