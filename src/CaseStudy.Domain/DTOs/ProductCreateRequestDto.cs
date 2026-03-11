using System.Data;
using CaseStudy.Domain.Models;

namespace CaseStudy.Domain.DTOs;

public record ProductCreateRequestDto(string Name, string Url, decimal Price, string? Description, int Quantity)
{
    public Product ToDomain() => new()
    {
        Name = this.Name,
        Url = this.Url,
        Price = this.Price,
        Description = this.Description,
        Quantity = this.Quantity
    };
}