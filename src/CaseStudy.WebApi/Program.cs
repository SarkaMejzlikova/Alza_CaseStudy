using CaseStudy.Domain.Models;
using CaseStudy.Persistence;
using CaseStudy.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);
{
    //Configure DI
    builder.Services.AddControllers();
    builder.Services.AddDbContext<CaseStudyContext>();
    builder.Services.AddScoped<IRepository<Product>, CaseStudyRepository>();
}

var app = builder.Build();
{
    //Configure MiddleWare
    app.MapControllers();
}

app.Run();
