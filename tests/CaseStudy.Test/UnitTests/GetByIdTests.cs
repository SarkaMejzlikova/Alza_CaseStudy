namespace CaseStudy.Test.UnitTests;

using System;
using CaseStudy.Domain.Models;
using CaseStudy.Persistence;
using CaseStudy.Persistence.Repositories;
using CaseStudy.WebApi;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

[Collection("Sequential")]
public class GetByIdTest
{
     [Fact]
    public async Task Get_ReadByIdWhenSomeItemAvailable_ReturnsOk()
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

        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Returns(record1);

        // Act
        var result = await controller.ReadByIdAsync(1);
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

        await repositoryMock.Received(1).ReadByIdAsync(1);
    }

    [Fact]
    public async Task Get_ReadByIdWhenItemIsNull_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<Product>>();
        var controller = new CaseStudyController(repositoryMock);

        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Returns((Product?)null);

        // Act
        var result = await controller.ReadByIdAsync(1);
        var resultResult = result.Result;

        // Assert
        Assert.IsType<NotFoundResult>(resultResult);

        await repositoryMock.Received(1).ReadByIdAsync(1);
    }

    [Fact]
    public async Task Get_ReadByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<Product>>();
        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Throws(new Exception());
        var controller = new CaseStudyController(repositoryMock);

        // Act
        var result = await controller.ReadByIdAsync(1);
        var resultResult = result.Result;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        var objectResult = resultResult as ObjectResult;
        Assert.Equal(500, objectResult?.StatusCode);

        await repositoryMock.Received(1).ReadByIdAsync(1);
    }
}
