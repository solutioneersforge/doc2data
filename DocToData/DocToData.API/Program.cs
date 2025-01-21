using DocToData.Application;
using DocToData.Application.QueriesHandler;
using DocToData.Domain.Factories;
using DocToData.Domain.Interfaces.Repositories;
using DocToDomain.Infrastructure.Data;
using DocToDomain.Infrastructure.Factories;
using DocToDomain.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.Loader;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<RegisterAssemblyMediatR>());

builder.Services.AddDbContext<DocToDataDBContext>(options => options.UseSqlServer(""));
builder.Services.AddTransient<IDBContextFactory, DbContextFactory>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Doc to Data";
        options.Theme = ScalarTheme.BluePlanet;
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
        options.CustomCss = "";
        options.ShowSidebar = true;
    });
}


app.UseAuthorization();

app.MapControllers();

app.Run();


