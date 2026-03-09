namespace CaseStudy.WebApi;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudy.Domain.DTOs;
using CaseStudy.Domain.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CaseStudyController : ControllerBase
{
    private static List<Product> products = [];

    [HttpPost]
    public ActionResult<ProductGetResponseDto> Create(ProductCreateRequestDto request)
    {
        // vytvořím nový produkt
        var product = request.ToDomain();
        try
        {
            // doplním si id
            product.ProductId = products.Count == 0 ? 1 : products.Max(p => p.ProductId) + 1;
            // přidám produkt
            products.Add(product);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        return CreatedAtAction(
            nameof(ReadById),
            new { product = product.ProductId },
            ProductGetResponseDto.FromDomain(product)); //201
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProductGetResponseDto>> Read()
    {
        List<Product> productsToGet;
        try
        {
            productsToGet = products;
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
            productToGet  = products.Find(p => p.ProductId == productId);
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
            // Index v listu
            int index = products.FindIndex(p => p.ProductId == productId);
            // neexistuje
            if (index == -1) { return NotFound(); } // 404
            // nové údaje pro produkt
            var updatedItem = request.ToDomain();
            // doplním Id, které chci aktualizovat
            updatedItem.ProductId = productId;
            // aktualizace
            products[index] = updatedItem;

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
            // najdu konkrétní produkt
            var product = products.Find(p => p.ProductId == productId);
            // neexistuje
            if (product == null) { return NotFound(); } // 404
            // odstraním ze seznamu
            products.Remove(product);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        return NoContent(); // 204
    }

    public void AddProductToStorage(Product product)
    {
        products.Add(product);
    }

    public void ClearStorage()
    {
        products.Clear();
    }
}