using System.ComponentModel.DataAnnotations;

namespace CaseStudy.Domain.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Length(1,50)]
    public string Name { get; set; }

    [Length(1,500)]
    public string Url { get; set; }

    public decimal Price { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    public int Quantity { get; set; }
}
