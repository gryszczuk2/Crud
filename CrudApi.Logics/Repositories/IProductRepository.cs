using CrudApi.Models;

namespace CrudApi.Logics.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetByName(string name);
    }
}
