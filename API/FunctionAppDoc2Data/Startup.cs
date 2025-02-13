using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Respositories;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(FunctionAppDoc2Data.Startup))]
namespace FunctionAppDoc2Data;
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        builder.Services.AddSingleton<IConfiguration>(config);
        builder.Services.AddScoped<IExpenseSubExpenseRepository, ExpenseSubExpenseRepository>();
        builder.Services.AddScoped<IMerchantRepository, MerchantRepository>();
        builder.Services.AddScoped<IReceiptRespository, ReceiptRespository>();
        builder.Services.AddScoped<IReceiptDashbboardRepository, ReceiptDashbboardRepository>();
        builder.Services.AddScoped<IReceiptVerificationRepository, ReceiptVerificationRepository>();
        builder.Services.AddScoped<IReceiptApprovalRepository, ReceiptApprovalRepository>();
        builder.Services.AddSingleton<DocToDataDBContext>();
        //builder.Services.AddDbContext<DocToDataDBContext>(options => options.UseSqlServer("Server=tcp:dbs-solutioneersforge.database.windows.net,1433;Initial Catalog=db-doc2data;Persist Security Info=False;User ID=serveradmin;Password=9U[X!mDG2_n89Ep:;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30"));
    }
}
