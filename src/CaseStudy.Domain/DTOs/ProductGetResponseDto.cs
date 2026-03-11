using System;
using CaseStudy.Domain.Models;

namespace CaseStudy.Domain.DTOs;

public record ProductGetResponseDto(int ProductId, string Name, string Url, decimal Price, string? Description, int Quantity)
{
    public static ProductGetResponseDto FromDomain(Product product) => new
    (
        product.ProductId,
        product.Name,
        product.Url,
        product.Price,
        product.Description,
        product.Quantity
    );
    
}