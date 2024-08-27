using Application;
using Infrastructure;
using Template.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder
    .ConfigureKestrel()
    .AddSwagger()
    .AddApplication()
    .AddInfrastructure();

var app = builder.Build();

app
    .UseSwaggerInDevelopment()
    .UseAuth()
    .UseHttpsRedirection();

app.MapEndpoints();

await app.RunMigrations();

app.Run();
