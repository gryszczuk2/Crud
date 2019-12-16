using System;
using System.Linq;
using CrudApi.Logics.Repositories;
using CrudApi.Models;

namespace CrudApi.DataAccess
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(Lazy<DataContext> dataContext) : base(dataContext)
        {
        }

        public Product GetByName(string name)
        {
            return DataContext.Products.FirstOrDefault(p => p.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()));
        }
    }
}
