using Eventify.Common.Classes.Products;
using Eventify.DAL.Base;
using Eventify.DAL.Infrastructure;
using Eventify.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventify.DAL.Products
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
    }

    internal class ProductRepository : BaseRepository<AppDbContext>, IProductRepository
    {
        public ProductRepository(DbContextOptions options) : base(options)
        {
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var dbProducts = await GetAllAsync<DbProduct>();
            return dbProducts.Select(x => x.ToProduct());
        }
    }
}
