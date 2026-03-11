namespace CaseStudy.Test.IntegrationTests;

using System.Runtime;
using CaseStudy.Domain.DTOs;
using CaseStudy.Domain.Models;
using CaseStudy.Persistence;
using CaseStudy.Persistence.Repositories;
using CaseStudy.WebApi;
using Microsoft.AspNetCore.Mvc;

[Collection("Sequential")]
public class GetTests
{
    [Fact]
    public async Task Get_AllProducts_ReturnsAllProducts()
    {
        // Arrange
        var context = new CaseStudyContext("DataSource=../../../../../src/data/testdb.db");
        var repository = new CaseStudyRepository(context);
        var controller = new CaseStudyController(repository);

        context.Products.RemoveRange(context.Products);
        await context.SaveChangesAsync();
        
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

        await context.Products.AddAsync(record1);
        await context.Products.AddAsync(record2);
        await context.SaveChangesAsync();

        // Act
        var result = await controller.ReadAsync();
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
    public async Task Get_ReadWhenNoItemAvailable_ReturnsNotFound()
    {
        // Arrange
        var context = new CaseStudyContext("DataSource=../../../../../src/data/testdb.db");
        var repository = new CaseStudyRepository(context);
        var controller = new CaseStudyController(repository);

        // Act
        var result = await controller.ReadAsync();

        // Assert
        Assert.IsType<ActionResult<IEnumerable<ProductGetResponseDto>>>(result);
    }
}