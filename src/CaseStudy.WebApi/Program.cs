var builder = WebApplication.CreateBuilder(args);
{
    //Configure DI
    builder.Services.AddControllers();
}

var app = builder.Build();
{
    //Configure MiddleWare
    app.MapControllers();
}



app.Run();
