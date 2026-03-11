namespace CaseStudy.Test.IntegrationTests;

using System;
using CaseStudy.Domain.Models;
using CaseStudy.Persistence;
using CaseStudy.Persistence.Repositories;
using CaseStudy.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;

[Collection("Sequential")]
public class GetByIdTest
{
    [Fact]
    public async Task GetById_ValidId_ReturnsItem()
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

        await context.Products.AddAsync(record1);
        await context.SaveChangesAsync();

        // Act
        var result = await controller.ReadByIdAsync(record1.ProductId);
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
    public async Task GetById_InvalidId_ReturnsNotFound()
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

        await context.Products.AddAsync(record1);
        await context.SaveChangesAsync();

        // Act
        var invalidId = -1;
        var result = await controller.ReadByIdAsync(invalidId);
        var resultResult = result.Result;

        // Assert
        Assert.IsType<NotFoundResult>(resultResult);
    }
}
