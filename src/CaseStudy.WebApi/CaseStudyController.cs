namespace CaseStudy.WebApi;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudy.Domain.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CaseStudyController : ControllerBase
{
    private static List<Product> products = [];

    [HttpPost]
    public IActionResult Create()
    {
        return Ok();
    }

    [HttpGet]
    public IActionResult Read()
    {
        return Ok();
    }

    [HttpGet("{productId:int}")]
    public IActionResult ReadById(int productId)
    {
        return Ok();
    }

    [HttpPut]
    public IActionResult UpdateById()
    {
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteById()
    {
        return Ok();
    }
}