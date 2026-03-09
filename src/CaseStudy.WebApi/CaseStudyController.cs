namespace CaseStudy.WebApi;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudy.Domain.DTOs;
using CaseStudy.Domain.Models;
using CaseStudy.Persistence;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CaseStudyController : ControllerBase
{
    //private static List<Product> products = [];

    private readonly CaseStudyContext context;

    public CaseStudyController(CaseStudyContext context)
    {
        this.context = context;

        // Product product = new Product
        // {
        //     Name = "Black Diamond Hotforge",
        //     Url = "https://www.alza.cz/sport/black-diamond-hotforge-hybrid-quickpack-12-cm-blue-d7160161.htm",
        //     Price = 2319,
        //     Description = "Expreska - drátový zámek a klasický zámek",
        //     Quantity = 2
        // };

        // context.Add(product);
        // context.SaveChanges();
    }


    [HttpPost]
    public ActionResult<ProductGetResponseDto> Create(ProductCreateRequestDto request)
    {
        // vytvořím nový produkt
        var product = request.ToDomain();
        try
        {
            // doplním si id
            //product.ProductId = products.Count == 0 ? 1 : products.Max(p => p.ProductId) + 1;
            // přidám produkt
            context.Products.Add(product);
            context.SaveChanges();
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
            productsToGet = context.Products
                .AsNoTracking()
                .ToList();
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
            productToGet = context.Products
                .AsNoTracking()
                .FirstOrDefault(p => p.ProductId == productId);
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
            var existing = context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (existing == null) { return NotFound(); } // 404

             existing.Name = updatedItem.Name;
             existing.Url = updatedItem.Url;
             existing.Price = updatedItem.Price;
             existing.Description = updatedItem.Description;
             existing.Quantity = updatedItem.Quantity;

            context.SaveChanges();
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
            var itemToDelete = context.Products
                .FirstOrDefault(p => p.ProductId == productId);
            // najdu konkrétní produkt
            // neexistuje
            if (itemToDelete is null) { return NotFound(); } // 404
            // odstraním ze seznamu
            context.Products.Remove(itemToDelete);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        return NoContent(); // 204
    }

    [NonAction]
    public void AddProductToStorage(Product product)
    {
        context.Products.Add(product);
        context.SaveChanges();
    }

    [NonAction]
    public void ClearStorage()
    {
        context.Products.RemoveRange(context.Products);
        context.SaveChanges();
    }
}