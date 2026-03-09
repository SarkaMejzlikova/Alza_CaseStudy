namespace CaseStudy.Test;

using System;
using CaseStudy.Domain.Models;
using CaseStudy.Persistence;
using CaseStudy.WebApi;
using Microsoft.AspNetCore.Mvc;

[Collection("Sequential")]
public class DeleteTests
{
    [Fact]
    public void Delete_ValidId_ReturnsNoContent()
    {
        // Arrange
        var context = new CaseStudyContext("DataSource=../../../../../src/data/localdb.db");


        var controller = new CaseStudyController(context);
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
        var result = controller.DeleteById(record1.ProductId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Delete_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var context = new CaseStudyContext("DataSource=../../../../../src/data/localdb.db");
        var controller = new CaseStudyController(context);
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
        var result = controller.DeleteById(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
