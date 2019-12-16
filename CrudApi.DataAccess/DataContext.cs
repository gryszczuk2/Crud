using CrudApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
