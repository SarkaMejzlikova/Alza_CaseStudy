namespace CaseStudy.Test;

using System;
using CaseStudy.Domain.DTOs;
using CaseStudy.Domain.Models;
using CaseStudy.Persistence;
using CaseStudy.WebApi;
using Microsoft.AspNetCore.Mvc;

[Collection("Sequential")]
public class PutTests
{
    [Fact]
    public void Put_ValidId_ReturnsNoContent()
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

        var request = new ProductUpdateRequestDto(
            Name: "Karabina",
            Url: "https://www.alza.cz/sport/camp-steel-oval-standard-lock-d6939453.htm",
            Price: 129,
            Description: "Karabina",
            Quantity: 10
        );

        // Act
        var result = controller.UpdateById(record1.ProductId, request);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Put_InvalidId_ReturnsNotFound()
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

        var request = new ProductUpdateRequestDto(
            Name: "Karabina",
            Url: "https://www.alza.cz/sport/camp-steel-oval-standard-lock-d6939453.htm",
            Price: 129,
            Description: "Karabina",
            Quantity: 10
        );

        // Act
        var invalidId = -1;
        var result = controller.UpdateById(invalidId, request);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
