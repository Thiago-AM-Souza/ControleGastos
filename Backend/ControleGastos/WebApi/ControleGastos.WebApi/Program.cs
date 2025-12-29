using ControleGastos.WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppBootstrapper(builder.Configuration);

var app = builder.Build();


app.Run();
