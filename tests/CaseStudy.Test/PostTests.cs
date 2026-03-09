namespace CaseStudy.Test;

using System;
using CaseStudy.Domain.DTOs;
using CaseStudy.Domain.Models;
using CaseStudy.Persistence;
using CaseStudy.WebApi;
using Microsoft.AspNetCore.Mvc;

[Collection("Sequential")]
public class PostTests
{
    [Fact]
    public void Post_ValidRequest_ReturnsNewItem()
    {
        // Arrange
        var context = new CaseStudyContext("DataSource=../../../../../src/data/localdb.db");
        var controller = new CaseStudyController(context);
        controller.ClearStorage();

        var request = new ProductCreateRequestDto
        (
            Name: "Karabina Camp Steel",
            Url: "https://www.alza.cz/sport/camp-steel-oval-standard-lock-d6939453.htm",
            Price: 179,
            Description: "Karabina - s pojistkou, šroubovací zámek",
            Quantity: 50
        );

        // Act
        var result = controller.Create(request);
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
