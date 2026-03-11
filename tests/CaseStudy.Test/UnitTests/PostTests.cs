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
public class PostTests
{
    [Fact]
    public async Task Post_ValidRequest_ReturnsNewItem()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<Product>>();
        var controller = new CaseStudyController(repositoryMock);
        
        var request = new ProductCreateRequestDto
        (
            Name: "Karabina Camp Steel",
            Url: "https://www.alza.cz/sport/camp-steel-oval-standard-lock-d6939453.htm",
            Price: 179,
            Description: "Karabina - s pojistkou, šroubovací zámek",
            Quantity: 50
        );

        // Act
        var result = await controller.CreateAsync(request);
        var resultResult = result.Result;
        var value = result.GetValue();

        // Assert
        Assert.IsType<CreatedAtActionResult>(resultResult);
        Assert.NotNull(value);

        Assert.Equal(request.Name, value.Name);
        Assert.Equal(request.Url, value.Url);
        Assert.Equal(request.Price, value.Price);
        Assert.Equal(request.Description, value.Description);
        Assert.Equal(request.Quantity, value.Quantity);
    }
}
