using BusinessObject.Models;
using BussinessObject.Models;
using DataAccess;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using PMRBDOdata.TokenValidation;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Emit;
using System.Text;
using Repository.IRepository;
using Repository.Repository;
using Net.payOS;

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

//PayOS

PayOS payOS = new PayOS(builder.Configuration["PayOS:ClientID"] ?? throw new Exception("Cannot find environment"),
                    builder.Configuration["PayOS:APIKey"] ?? throw new Exception("Cannot find environment"),
                    builder.Configuration["PayOS:ChecksumKey"] ?? throw new Exception("Cannot find environment"));


builder.Services.AddSingleton(payOS);
//Odata Service
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

modelbuilder.EntityType<RecipeTag>().HasKey(x => new { x.RecipeId, x.TagId });
modelbuilder.EntityType<BookRate>().HasKey(x => new { x.BookId, x.CustomerId });
modelbuilder.EntityType<BookShelf>().HasKey(x => new { x.EbookId, x.CustomerId });
modelbuilder.EntityType<PersonalRecipe>().HasKey(x => new { x.RecipeId, x.CustomerId });
modelbuilder.EntityType<RecipeRate>().HasKey(x => new { x.RecipeId, x.AccountId });


builder.Services.AddControllers().AddOData(options => options.Select().Filter().OrderBy().SetMaxTop(null).Count().Expand().AddRouteComponents("odata", modelbuilder.GetEdmModel()));

//CORS

//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(
//        builder =>
//        {
//            builder.WithOrigins("http://localhost:5173")
//                   .AllowAnyMethod()
//                   .AllowAnyHeader();
//        });
//});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });

    //options.AddPolicy("AllowSpecificOrigins",
    //builder =>
    //{
    //    builder.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader();
    //});
});

//Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
// 

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseODataBatching();
app.UseCors("AllowAll");
//app.UseCors("AllowSpecificOrigins");
app.UseRouting();


app.UseHttpsRedirection();
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
    context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
    context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Accept-Language, Accept-Encoding");
    await next();
});
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.UseMiddleware<TokenValidationMiddleware>();
//app.UseStaticFiles();

app.MapControllers();

app.Run();