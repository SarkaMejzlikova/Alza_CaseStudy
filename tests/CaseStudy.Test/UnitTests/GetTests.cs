namespace CaseStudy.Test.UnitTests;

using System.Runtime;
using CaseStudy.Domain.DTOs;
using CaseStudy.Domain.Models;
using CaseStudy.Persistence;
using CaseStudy.Persistence.Repositories;
using CaseStudy.WebApi;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

[Collection("Sequential")]
public class GetTests
{
    [Fact]
    public async Task Get_ReadWhenSomeItemAvailable_ReturnsOk()
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

        var someItemList = new List<Product> { record1 };
        repositoryMock.ReadAllAsync().Returns(someItemList);

        // Act
        var result = await controller.ReadAsync();
        var resultResult = result.Result;
        var value = result.GetValue();

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        Assert.NotNull(value);
        Assert.Single(value);
        Assert.Equal(record1.ProductId, value.First().ProductId);
        Assert.Equal(record1.Name, value.First().Name);

        await repositoryMock.Received(1).ReadAllAsync();
    }
}