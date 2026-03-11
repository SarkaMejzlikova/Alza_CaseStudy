using System;
using CaseStudy.Domain.Models;

namespace CaseStudy.Domain.DTOs;

public record ProductUpdateRequestDto(string Name, string Url, decimal Price, string? Description, int Quantity)
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
