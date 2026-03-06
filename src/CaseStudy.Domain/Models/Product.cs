namespace CaseStudy.Domain.Models;

public class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; }

    public string Url { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; }

    public int Quantity { get; set; }
}
