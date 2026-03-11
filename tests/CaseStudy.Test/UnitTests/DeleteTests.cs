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
public class DeleteTests
{
    [Fact]
    public async Task Delete_DeleteByIdValidItemId_ReturnsNoContent()
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

        // Act
        var result = await controller.DeleteByIdAsync(1);

        // Assert
        Assert.IsType<NoContentResult>(result);

        await repositoryMock.Received(1).ReadByIdAsync(1);
        await repositoryMock.Received(1).DeleteByIdAsync(1);
    }

    [Fact]
    public async Task Delete_DeleteByIdInvalidItemId_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<Product>>();
        var controller = new CaseStudyController(repositoryMock);

        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Returns(null as Product);

        // Act
        var result = await controller.DeleteByIdAsync(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);

        await repositoryMock.Received(1).ReadByIdAsync(1);
        await repositoryMock.DidNotReceive().DeleteByIdAsync(Arg.Any<int>());
    }

    [Fact]
    public async Task Delete_DeleteByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<Product>>();
        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Throws(new Exception());
        var controller = new CaseStudyController(repositoryMock);

        // Act
        var result = await controller.DeleteByIdAsync(1);
        var resultResult = result as ObjectResult;

        // Assert
        Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, resultResult?.StatusCode);

        await repositoryMock.Received(1).ReadByIdAsync(1);
        await repositoryMock.DidNotReceive().DeleteByIdAsync(Arg.Any<int>());
    }
}

