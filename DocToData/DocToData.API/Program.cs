using DocToData.Application;
using DocToData.Domain.Factories;
using DocToData.Domain.Interfaces.Repositories;
using DocToData.Domain.Interfaces.Services;
using DocToDomain.Infrastructure.Data;
using DocToDomain.Infrastructure.Factories;
using DocToDomain.Infrastructure.Repositories;
using DocToDomain.Infrastructure.Repositories.Receipt;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<RegisterAssemblyMediatR>());

builder.Services.AddDbContext<DocToDataDBContext>(options => options.UseSqlServer(""));
builder.Services.AddTransient<IDBContextFactory, DbContextFactory>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IReceiptProcessRepository, ReceiptProcessRepository>();
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


