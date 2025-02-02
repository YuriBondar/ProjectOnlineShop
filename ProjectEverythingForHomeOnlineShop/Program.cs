using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectEverythingForHomeOnlineShop.Application.Services;
using ProjectEverythingForHomeOnlineShop.Application.Services.Implementation;
using ProjectEverythingForHomeOnlineShop.DataAccess.Persistence;
using ProjectEverythingForHomeOnlineShop.DataAccess.Persistence.Identity;
using ProjectEverythingForHomeOnlineShop.DataAccess.Repositories;
using ProjectEverythingForHomeOnlineShop.DataAccess.Repositories.Implementation;
using ProjectEverythingForHomeOnlineShop.Infrastructure;
using ProjectEverythingForHomeOnlineShop.Infrastructure.JwtAuthentication;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// logging registration

builder.Logging.ClearProviders();

LoggingConfig.ConfigureLogger();

builder.Host.UseSerilog();


// database context registration
//builder.Services.AddSingleton<DatabaseConnectionConfigurator>(); is created automatic in api apps
builder.Services.AddDbContext<OnlineShopMySQLDatabaseContext>();

/////////////////////////////////////////////////////////////////////////
// Identity section
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                                                            {
                                                                options.Password.RequireDigit = true;
                                                                options.Password.RequiredLength = 8;
                                                                options.Password.RequireNonAlphanumeric = true;
                                                                options.User.RequireUniqueEmail = true;
                                                            })
                .AddEntityFrameworkStores<OnlineShopMySQLDatabaseContext>()
                .AddDefaultTokenProviders();

//  registration services for data access

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();



//  registration services for business logic
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IOrderService, OrderService>();



// authentication query with token

builder.Services.AddJwtAuthentication(builder.Configuration);


// jwt tocken ganaration
builder.Services.AddScoped<TokenGenerator>();


builder.Services.AddAuthorization();

// registation class
// for initialaize database if it is not exist when app starts to run
// also for implementation migrations if they was created
builder.Services.AddScoped<DatabaseInitializer>();


//set up culture for working mit decimal data type
var cultureInfo = new CultureInfo("en-US"); 
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var databaseInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();

    try
    {
        await databaseInitializer.InitializeAsync();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error by database initialization.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
