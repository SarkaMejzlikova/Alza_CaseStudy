namespace CaseStudy.Persistence.Repositories;

using System.Threading.Tasks;
using CaseStudy.Domain.Models;
using Microsoft.EntityFrameworkCore;

public class CaseStudyRepository : IRepositoryAsync<Product>
{
    private readonly CaseStudyContext context;

    public CaseStudyRepository(CaseStudyContext context)
    {
        this.context = context;
    }

    public async Task CreateAsync(Product product)
    {
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> ReadAllAsync()
    {
        return await context.Products.ToListAsync();
    }

    public async Task<Product?> ReadByIdAsync(int productId)
    {
        return await context.Products.FindAsync(productId);
    }

    public async Task UpdateAsync(Product product)
    {
        var foundItem = await context.Products.FindAsync(product.ProductId) ?? throw new ArgumentOutOfRangeException($"Product with ID {product.ProductId} not found.");
        context.Entry(foundItem).CurrentValues.SetValues(product);
        await context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int productId)
    {
        var item = await context.Products.FindAsync(productId) ?? throw new ArgumentOutOfRangeException($"Product with ID {productId} not found.");
        context.Products.Remove(item);
        await context.SaveChangesAsync();
    }
}