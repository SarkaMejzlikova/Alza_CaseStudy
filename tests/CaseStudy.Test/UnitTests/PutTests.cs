namespace CaseStudy.Test.UnitTests;

using System;
using CaseStudy.Domain.DTOs;
using CaseStudy.Domain.Models;
using CaseStudy.Persistence;
using CaseStudy.Persistence.Repositories;
using CaseStudy.WebApi;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

[Collection("Sequential")]
public class PutTests
{
     [Fact]
    public void Put_UpdateByIdWhenItemUpdated_ReturnsNoContent()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<Product>>();
        var controller = new CaseStudyController(repositoryMock);

        var record1 = new Product
        {
            ProductId = 1,
            Name = "Karabina Camp Steel",
            Url = "https://www.alza.cz/sport/camp-steel-oval-standard-lock-d6939453.htm",
            Price = 179,
            Description = "Karabina - s pojistkou, šroubovací zámek",
            Quantity = 50
        };
        
        repositoryMock.ReadById(1).Returns(record1);

        var request = new ProductUpdateRequestDto(
            Name: "Karabina",
            Url: "https://www.alza.cz/sport/camp-steel-oval-standard-lock-d6939453.htm",
            Price: 129,
            Description: "Karabina",
            Quantity: 10
        );

        // Act
        var result = controller.UpdateById(1, request);

        // Assert
        Assert.IsType<NoContentResult>(result);

        repositoryMock.Received(1).ReadById(1);
        repositoryMock.Received(1).Update(Arg.Is<Product>(x => 
            x.ProductId == 1 && 
            x.Name == "Karabina" && 
            x.Url == "https://www.alza.cz/sport/camp-steel-oval-standard-lock-d6939453.htm" && 
            x.Price == 129 &&
            x.Description == "Karabina" &&
            x.Quantity == 10));
    }

    [Fact]
    public void Put_UpdateByIdWhenIdNotFound_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<Product>>();
        var controller = new CaseStudyController(repositoryMock);

        repositoryMock.ReadById(1).Returns((Product?)null);

        var request = new ProductUpdateRequestDto(
            Name: "Karabina",
            Url: "https://www.alza.cz/sport/camp-steel-oval-standard-lock-d6939453.htm",
            Price: 129,
            Description: "Karabina",
            Quantity: 10
        );

        // Act
        var result = controller.UpdateById(1, request);

        // Assert
        Assert.IsType<NotFoundResult>(result);

        repositoryMock.Received(1).ReadById(1);
        repositoryMock.DidNotReceive().Update(Arg.Any<Product>());
    }

    [Fact]
    public void Put_UpdateByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<Product>>();
        repositoryMock.ReadById(Arg.Any<int>()).Returns(x => throw new Exception("Database error"));        
        var controller = new CaseStudyController(repositoryMock);

        var request = new ProductUpdateRequestDto(
            Name: "Karabina",
            Url: "https://www.alza.cz/sport/camp-steel-oval-standard-lock-d6939453.htm",
            Price: 129,
            Description: "Karabina",
            Quantity: 10
        );

        // Act
        var result = controller.UpdateById(1, request);
        var resultResult = result as ObjectResult;

        // Assert
        Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, resultResult?.StatusCode);

        repositoryMock.Received(1).ReadById(1);
        repositoryMock.DidNotReceive().Update(Arg.Any<Product>());
    }
}
