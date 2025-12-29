using ControleGastos.WebApi.API.Middlewares;
using ControleGastos.WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppBootstrapper(builder.Configuration);

var app = builder.Build();

app.MapControllers();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
