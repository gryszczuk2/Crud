using System;
using System.Collections.Generic;
using System.Linq;
using CrudApi.Logics.Repositories;
using CrudApi.Models;

namespace CrudApi.DataAccess
{
    public abstract class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly Lazy<DataContext> _dataContext;
        protected DataContext DataContext
        {
            get { return _dataContext.Value; }
        }

        protected Repository(Lazy<DataContext> dataContext)
        {
            _dataContext = dataContext;
        }

        public T GetById(int id)
        {
            return DataContext.Set<T>().FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<T> GetAllActive()
        {
            return DataContext.Set<T>().Where(t => t.IsActive);
        }

        public void Add(T model)
        {
            DataContext.Set<T>().Add(model);
        }

        public void Remove(T model)
        {
            DataContext.Set<T>().Remove(model);
        }

        public void Update(T model)
        {
            DataContext.Set<T>().Update(model);
        }

        public void SaveChanges()
        {
            DataContext.SaveChanges();
        }

    }
}
