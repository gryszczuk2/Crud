using System.Collections;
using System.Collections.Generic;
using CrudApi.Models;
using Microsoft.VisualBasic.CompilerServices;

namespace CrudApi.Logics.Interfaces
{
    public interface IProductLogic : ILogic
    {
        Result<Product> GetById(int id);
        Result<IEnumerable<Product>> GetAllActive();
        Result<Product> Add(Product product);
        Result<Product> Update(Product product);
        Result<Product> Remove(Product product);
    }
}
