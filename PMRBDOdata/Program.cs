using BusinessObject.Models;
using BussinessObject.Models;
using DataAccess;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using PMRBDOdata.TokenValidation;
using Repository.IRepository;
using Repository.Repository;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Emit;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped(typeof(RmrbdContext));
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IEbookRepository, EbookRepository>();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


// Google Authentication Configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
});

// Add services to the container.

builder.Services.AddControllers();

var modelbuilder = new ODataConventionModelBuilder();
modelbuilder.EntitySet<Book>("Books");
modelbuilder.EntitySet<Comment>("Comments");
modelbuilder.EntitySet<Account>("Accounts");
modelbuilder.EntitySet<Ebook>("Ebooks");
modelbuilder.EntitySet<AccountProfile>("AccountProfiles");
modelbuilder.EntitySet<Image>("Images");
modelbuilder.EntitySet<Notification>("Notifications");
modelbuilder.EntitySet<Recipe>("Recipes");
modelbuilder.EntitySet<Tag>("Tags");
modelbuilder.EntitySet<CustomerAddress>("CustomerAddresses");
modelbuilder.EntitySet<CoinTransaction>("CoinTransactions");
modelbuilder.EntitySet<RecipeTransaction>("RecipeTransactions");
modelbuilder.EntitySet<BookTransaction>("BookTransactions");
modelbuilder.EntitySet<EbookTransaction>("EbookTransactions");
modelbuilder.EntitySet<BookOrderStatus>("BookOrderStatuses");
modelbuilder.EntitySet<BookCategory>("BookCategories");
modelbuilder.EntitySet<BookOrder>("BookOrders");
modelbuilder.EntitySet<Role>("Roles");
modelbuilder.EntitySet<BookRate>("BookRates");
modelbuilder.EntitySet<BookShelf>("BookShelves");
modelbuilder.EntitySet<PersonalRecipe>("PersonalRecipes");
modelbuilder.EntitySet<RecipeRate>("RecipeRates");
//
modelbuilder.EntityType<RecipeTag>().HasKey(x => new { x.RecipeId, x.TagId });
modelbuilder.EntityType<BookRate>().HasKey(x => new { x.BookId, x.CustomerId });
modelbuilder.EntityType<BookShelf>().HasKey(x => new { x.EbookId, x.CustomerId });
modelbuilder.EntityType<PersonalRecipe>().HasKey(x => new { x.RecipeId, x.CustomerId });
modelbuilder.EntityType<RecipeRate>().HasKey(x => new { x.RecipeId, x.AccountId });


builder.Services.AddControllers().AddOData(options => options.Select().Filter().OrderBy().SetMaxTop(null).Count().Expand().AddRouteComponents("odata", modelbuilder.GetEdmModel()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

// 

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddAuthorization();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseMiddleware<TokenValidationMiddleware>();

app.UseStaticFiles();

app.UseODataBatching();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();