using CaseStudy.Persistence;

var builder = WebApplication.CreateBuilder(args);
{
    //Configure DI
    builder.Services.AddControllers();
    builder.Services.AddDbContext<CaseStudyContext>();
}

var app = builder.Build();
{
    //Configure MiddleWare
    app.MapControllers();
}

app.Run();
