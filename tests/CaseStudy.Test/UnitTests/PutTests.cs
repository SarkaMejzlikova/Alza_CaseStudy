namespace CaseStudy.Test.UnitTests;

using System;
using CaseStudy.Domain.DTOs;
using CaseStudy.Domain.Models;
using CaseStudy.Persistence;
using CaseStudy.Persistence.Repositories;
using CaseStudy.WebApi;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

[Collection("Sequential")]
public class PutTests
{
     [Fact]
    public async Task Put_UpdateByIdWhenItemUpdated_ReturnsNoContent()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<Product>>();
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
        
        repositoryMock.ReadByIdAsync(1).Returns(record1);

        var request = new ProductUpdateRequestDto(
            Name: "Karabina",
            Url: "https://www.alza.cz/sport/camp-steel-oval-standard-lock-d6939453.htm",
            Price: 129,
            Description: "Karabina",
            Quantity: 10
        );

        // Act
        var result = await controller.UpdateByIdAsync(1, request);

        // Assert
        Assert.IsType<NoContentResult>(result);

        await repositoryMock.Received(1).ReadByIdAsync(1);
        await repositoryMock.Received(1).UpdateAsync(Arg.Is<Product>(x => 
            x.ProductId == 1 && 
            x.Name == "Karabina" && 
            x.Url == "https://www.alza.cz/sport/camp-steel-oval-standard-lock-d6939453.htm" && 
            x.Price == 129 &&
            x.Description == "Karabina" &&
            x.Quantity == 10));
    }

    [Fact]
    public async Task Put_UpdateByIdWhenIdNotFound_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<Product>>();
        var controller = new CaseStudyController(repositoryMock);

        repositoryMock.ReadByIdAsync(1).Returns((Product?)null);

        var request = new ProductUpdateRequestDto(
            Name: "Karabina",
            Url: "https://www.alza.cz/sport/camp-steel-oval-standard-lock-d6939453.htm",
            Price: 129,
            Description: "Karabina",
            Quantity: 10
        );

        // Act
        var result = await controller.UpdateByIdAsync(1, request);

        // Assert
        Assert.IsType<NotFoundResult>(result);

        await repositoryMock.Received(1).ReadByIdAsync(1);
        await repositoryMock.DidNotReceive().UpdateAsync(Arg.Any<Product>());
    }

    [Fact]
    public async Task Put_UpdateByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<Product>>();
        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Throws(new Exception());      
        var controller = new CaseStudyController(repositoryMock);

        var request = new ProductUpdateRequestDto(
            Name: "Karabina",
            Url: "https://www.alza.cz/sport/camp-steel-oval-standard-lock-d6939453.htm",
            Price: 129,
            Description: "Karabina",
            Quantity: 10
        );

        // Act
        var result = await controller.UpdateByIdAsync(1, request);
        var resultResult = result as ObjectResult;

        // Assert
        Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, resultResult?.StatusCode);

        await repositoryMock.Received(1).ReadByIdAsync(1);
        await repositoryMock.DidNotReceive().UpdateAsync(Arg.Any<Product>());
    }
}
