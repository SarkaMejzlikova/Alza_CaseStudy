using CaseStudy.Domain.Models;
using CaseStudy.Persistence;
using CaseStudy.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);
{
    //Configure DI
    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<CaseStudyContext>();
    builder.Services.AddScoped<IRepositoryAsync<Product>, CaseStudyRepository>();
}

var app = builder.Build();
{
    //Configure MiddleWare
    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI(config => config.SwaggerEndpoint("v1/swagger.json", "CaseStudy API V1"));
}

app.Run();
