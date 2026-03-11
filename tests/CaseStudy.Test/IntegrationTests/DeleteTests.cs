namespace CaseStudy.Test.IntegrationTests;

using System;
using CaseStudy.Domain.Models;
using CaseStudy.Persistence;
using CaseStudy.Persistence.Repositories;
using CaseStudy.WebApi;
using Microsoft.AspNetCore.Mvc;

[Collection("Sequential")]
public class DeleteTests
{
    [Fact]
    public async Task Delete_ValidId_ReturnsNoContent()
    {
        // Arrange
        using var context = new CaseStudyContext("DataSource=../../../../../src/data/testdb.db");

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
        var result = await controller.DeleteByIdAsync(record1.ProductId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_InvalidId_ReturnsNotFound()
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
        var result = await controller.DeleteByIdAsync(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
