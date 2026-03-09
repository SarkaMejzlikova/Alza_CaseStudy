namespace CaseStudy.Test;

using System;
using CaseStudy.Domain.Models;
using CaseStudy.WebApi;
using Microsoft.AspNetCore.Mvc;

[Collection("Sequential")]
public class GetByIdTest
{
    [Fact]
    public void GetById_ValidId_ReturnsItem()
    {
        // Arrange
        var controller = new CaseStudyController();
        controller.ClearStorage();
        
        var record1 = new Product
        {
            ProductId = 1,
            Name = "Karabina Camp Steel",
            Url = "https://www.alza.cz/sport/camp-steel-oval-standard-lock-d6939453.htm",
            Price = 179,
            Description = "Karabina - s pojistkou, šroubovací zámek",
            Quantity = 50
        };

        controller.AddProductToStorage(record1);

        // Act
        var result = controller.ReadById(record1.ProductId);
        var resultResult = result.Result;
        var value = result.GetValue();

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        Assert.NotNull(value);

        Assert.Equal(record1.ProductId, value.ProductId);
        Assert.Equal(record1.Name, value.Name);
        Assert.Equal(record1.Url, value.Url);
        Assert.Equal(record1.Price, value.Price);
        Assert.Equal(record1.Description, value.Description);
        Assert.Equal(record1.Quantity, value.Quantity);
    }

    [Fact]
    public void GetById_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var controller = new CaseStudyController();
        controller.ClearStorage();
        
        var record1 = new Product
        {
            ProductId = 1,
            Name = "Karabina Camp Steel",
            Url = "https://www.alza.cz/sport/camp-steel-oval-standard-lock-d6939453.htm",
            Price = 179,
            Description = "Karabina - s pojistkou, šroubovací zámek",
            Quantity = 50
        };

        controller.AddProductToStorage(record1);

        // Act
        var invalidId = -1;
        var result = controller.ReadById(invalidId);
        var resultResult = result.Result;

        // Assert
        Assert.IsType<NotFoundResult>(resultResult);
    }
}
