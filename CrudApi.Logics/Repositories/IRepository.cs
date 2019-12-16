using System.Collections.Generic;
using CrudApi.Models;

namespace CrudApi.Logics.Repositories
{
    public interface IRepository<T> where T : BaseModel
    {
        T GetById(int id);
        IEnumerable<T> GetAllActive();
        void Add(T model);
        void Remove(T model);
        void SaveChanges();
        void Update(T model);
    }
}
