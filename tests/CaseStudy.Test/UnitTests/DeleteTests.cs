namespace CaseStudy.Test.UnitTests;

using System;
using CaseStudy.Domain.Models;
using CaseStudy.Persistence;
using CaseStudy.Persistence.Repositories;
using CaseStudy.WebApi;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

[Collection("Sequential")]
public class DeleteTests
{
    [Fact]
    public void Delete_DeleteByIdValidItemId_ReturnsNoContent()
    {
        // Arrange
        using var context = new CaseStudyContext("DataSource=../../../../../src/data/testdb.db");

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

        // Act
        var result = controller.DeleteById(1);

        // Assert
        Assert.IsType<NoContentResult>(result);

        repositoryMock.Received(1).ReadById(1);
        repositoryMock.Received(1).DeleteById(1);
    }

    [Fact]
    public void Delete_DeleteByIdInvalidItemId_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<Product>>();
        var controller = new CaseStudyController(repositoryMock);

        repositoryMock.ReadById(1).Returns((Product?)null);

        // Act
        var result = controller.DeleteById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);

        repositoryMock.Received(1).ReadById(1);
        repositoryMock.DidNotReceive().DeleteById(Arg.Any<int>());
    }

    [Fact]
    public void Delete_DeleteByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<Product>>();
        repositoryMock.ReadById(Arg.Any<int>()).Returns(x => throw new Exception("Database error"));
        var controller = new CaseStudyController(repositoryMock);

        // Act
        var result = controller.DeleteById(1);
        var resultResult = result as ObjectResult;

        // Assert
        Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, resultResult?.StatusCode);

        repositoryMock.Received(1).ReadById(1);
        repositoryMock.DidNotReceive().DeleteById(Arg.Any<int>());
    }
}

