namespace CaseStudy.WebApi;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudy.Domain.DTOs;
using CaseStudy.Domain.Models;
using CaseStudy.Persistence;
using CaseStudy.Persistence.Repositories;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CaseStudyController : ControllerBase
{
    //private static List<Product> products = [];

    private readonly IRepository<Product> repository;

    public CaseStudyController(IRepository<Product> repository)
    {
        this.repository = repository;
    }


    [HttpPost]
    public ActionResult<ProductGetResponseDto> Create(ProductCreateRequestDto request)
    {
        // vytvořím nový produkt
        var product = request.ToDomain();
        try
        {
            repository.Create(product);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        return CreatedAtAction(
            nameof(ReadById),
            new { productId = product.ProductId },
            ProductGetResponseDto.FromDomain(product)); //201
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProductGetResponseDto>> Read()
    {
        IEnumerable<Product> productsToGet;
        try
        {
            productsToGet = repository.ReadAll();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); // 500
        }

        return (productsToGet is null)
            ? NotFound() // 404
            : Ok(productsToGet.Select(ProductGetResponseDto.FromDomain)); // 200
    }

    [HttpGet("{productId:int}")]
    public ActionResult<ProductGetResponseDto> ReadById(int productId)
    {
        Product? productToGet;
        try
        {
            productToGet = repository.ReadById(productId);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); // 500
        }

        return (productToGet is null)
        ? NotFound() //404
        : Ok(ProductGetResponseDto.FromDomain(productToGet)); //200
    }

    [HttpPut("{productId:int}")]
    public IActionResult UpdateById(int productId, [FromBody] ProductUpdateRequestDto request)
    {
        try
        {
            var updatedItem = request.ToDomain();
            updatedItem.ProductId = productId;

            var itemToUpdate = repository.ReadById(productId);
            if (itemToUpdate == null) { return NotFound(); } // 404

            repository.Update(updatedItem);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        return NoContent(); // 204
    }

    [HttpDelete("{productId:int}")]
    public IActionResult DeleteById(int productId)
    {
        try
        {
            var itemToDelete = repository.ReadById(productId);
            // najdu konkrétní produkt
            // neexistuje
            if (itemToDelete is null) { return NotFound(); } // 404
            // odstraním ze seznamu
            repository.DeleteById(productId);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        return NoContent(); // 204
    }
}