namespace CaseStudy.Persistence.Repositories;

using CaseStudy.Domain.Models;

public class CaseStudyRepository : IRepository<Product>
{
    private readonly CaseStudyContext context;

    public CaseStudyRepository(CaseStudyContext context)
    {
        this.context = context;
    }

    public void Create(Product product)
    {
        context.Products.Add(product);
        context.SaveChanges();
    }

    public IEnumerable<Product> ReadAll()
    {
        return context.Products.ToList();
    }

    public Product? ReadById(int productId)
    {
        return context.Products.Find(productId);
    }

    public void Update(Product product)
    {
        var foundItem = context.Products.Find(product.ProductId) ?? throw new ArgumentOutOfRangeException($"Product with ID {product.ProductId} not found.");
        context.Entry(foundItem).CurrentValues.SetValues(product);
        context.SaveChanges();
    }

    public void DeleteById(int productId)
    {
        var item = context.Products.Find(productId) ?? throw new ArgumentOutOfRangeException($"Product with ID {productId} not found.");
        context.Products.Remove(item);
        context.SaveChanges();
    }
}