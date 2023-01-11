using Microsoft.EntityFrameworkCore;
using Mkt.Domain.Product.Application.Infraestructure.Contexts;
using Mkt.Domain.Product.Application.Infraestructure.Entities;
using System.Linq.Expressions;

namespace Mkt.Domain.Product.Application.Infraestructure.Repositories
{
    public class ProductRepository
    {
        private readonly ProductContext context;

        public ProductRepository(ProductContext context)
        {
            this.context = context;
        }

        public async Task AddRangeAsync(IEnumerable<ProductEntity> entities)
        {
            context.Products.AddRange(entities);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductEntity>> ListAllAsync()
        {
            return await context.Products.ToListAsync();
        }

        public ProductEntity GetByFilter(Expression<Func<ProductEntity, bool>> expression)
        {
            return context.Products.FirstOrDefault(expression.Compile());
        }
    }
}
