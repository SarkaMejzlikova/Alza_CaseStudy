using System.ComponentModel.DataAnnotations;

namespace CaseStudy.Domain.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Name is mandatory.")]
    [Length(1,50)]
    public string Name { get; set; }  = string.Empty;

    [Required(ErrorMessage = "Url is mandatory.")]
    [Length(1,500)]
    public string Url { get; set; } = string.Empty;

    public decimal Price { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    public int Quantity { get; set; }
}
