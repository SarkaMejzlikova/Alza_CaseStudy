namespace CaseStudy.Test;

using System.Runtime;
using CaseStudy.Domain.DTOs;
using CaseStudy.Domain.Models;
using CaseStudy.WebApi;
using Microsoft.AspNetCore.Mvc;

[Collection("Sequential")]
public class GetTests
{
    [Fact]
    public void Get_AllProducts_ReturnsAllProducts()
    {
        // Arrange
        var record1 = new Product
        {
            ProductId = 1,
            Name = "Karabina Camp Steel",
            Url = "https://www.alza.cz/sport/camp-steel-oval-standard-lock-d6939453.htm",
            Price = 179,
            Description = "Karabina - s pojistkou, šroubovací zámek",
            Quantity = 50
        };

        var record2 = new Product
        {
            ProductId = 2,
            Name = "Karabina Camp Orbit",
            Url = "https://www.alza.cz/sport/camp-orbit-lock-orange-d6939454.htm",
            Price = 279,
            Description = "Karabina - s pojistkou, šroubovací zámek",
            Quantity = 25
        };

        var controller = new CaseStudyController();
        controller.ClearStorage();
        controller.AddProductToStorage(record1);
        controller.AddProductToStorage(record2);

        // Act
        var result = controller.Read();
        var value = result.GetValue();

        // Assert
        Assert.NotNull(value);

        var firstRecord = value.First();
        Assert.Equal(record1.ProductId, firstRecord.ProductId);
        Assert.Equal(record1.Name, firstRecord.Name);
        Assert.Equal(record1.Url, firstRecord.Url);
        Assert.Equal(record1.Price, firstRecord.Price);
        Assert.Equal(record1.Description, firstRecord.Description);
        Assert.Equal(record1.Quantity, firstRecord.Quantity);
    }

    [Fact]
    public void Get_ReadWhenNoItemAvailable_ReturnsNotFound()
    {
        // Arrange
        var controller = new CaseStudyController();

        // Act
        var result = controller.Read();

        // Assert
        Assert.IsType<ActionResult<IEnumerable<ProductGetResponseDto>>>(result);
    }
}