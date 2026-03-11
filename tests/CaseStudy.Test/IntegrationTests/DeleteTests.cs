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
    public void Delete_ValidId_ReturnsNoContent()
    {
        // Arrange
        using var context = new CaseStudyContext("DataSource=../../../../../src/data/testdb.db");

        var repository = new CaseStudyRepository(context);
        var controller = new CaseStudyController(repository);

        context.Products.RemoveRange(context.Products);
        context.SaveChanges();

        var record1 = new Product
        {
            ProductId = 1,
            Name = "Karabina Camp Steel",
            Url = "https://www.alza.cz/sport/camp-steel-oval-standard-lock-d6939453.htm",
            Price = 179,
            Description = "Karabina - s pojistkou, šroubovací zámek",
            Quantity = 50
        };

        context.Products.Add(record1);
        context.SaveChanges();

        // Act
        var result = controller.DeleteById(record1.ProductId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Delete_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var context = new CaseStudyContext("DataSource=../../../../../src/data/testdb.db");

        var repository = new CaseStudyRepository(context);
        var controller = new CaseStudyController(repository);

        context.Products.RemoveRange(context.Products);
        context.SaveChanges();

        var record1 = new Product
        {
            ProductId = 1,
            Name = "Karabina Camp Steel",
            Url = "https://www.alza.cz/sport/camp-steel-oval-standard-lock-d6939453.htm",
            Price = 179,
            Description = "Karabina - s pojistkou, šroubovací zámek",
            Quantity = 50
        };

        context.Products.Add(record1);
        context.SaveChanges();

        // Act
        var invalidId = -1;
        var result = controller.DeleteById(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
