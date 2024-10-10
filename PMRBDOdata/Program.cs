using BusinessObject.Models;
using BussinessObject.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using System.Diagnostics;
using System.Reflection.Emit;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped(typeof(RmrbdContext));
// Add services to the container.
builder.Services.AddControllers();

var modelbuilder = new ODataConventionModelBuilder();
modelbuilder.EntitySet<Book>("Books");
modelbuilder.EntitySet<Comment>("Comments");
modelbuilder.EntitySet<Customer>("Customers");
modelbuilder.EntitySet<Ebook>("Ebooks");
modelbuilder.EntitySet<Employee>("Employees");
modelbuilder.EntitySet<EmployeeType>("EmployeeTypes");
modelbuilder.EntitySet<Image>("Images");
modelbuilder.EntitySet<Notification>("Notifications");
modelbuilder.EntitySet<Recipe>("Recipes");
modelbuilder.EntitySet<Tag>("Tags");
modelbuilder.EntitySet<CoinTransaction>("CoinTransactions");
modelbuilder.EntitySet<RecipeTransaction>("RecipeTransactions");
modelbuilder.EntitySet<BookTransaction>("BookTransactions");
modelbuilder.EntitySet<EbookTransaction>("EbookTransactions");
modelbuilder.EntitySet<BookOrderStatus>("BookOrderStatuses");

modelbuilder.EntityType<BookCategory>();
modelbuilder.EntityType<BookOrder>();
modelbuilder.EntityType<BookRate>().HasKey(x => new { x.BookId, x.CustomerId });
modelbuilder.EntityType<BookShelf>().HasKey(x => new { x.EbookId, x.CustomerId });
modelbuilder.EntityType<PersonalRecipe>().HasKey(x => new { x.RecipeId, x.CustomerId });
modelbuilder.EntityType<RecipeRate>().HasKey(x => new { x.RecipeId, x.CustomerId });
modelbuilder.EntityType<ServiceFeedBack>();

builder.Services.AddControllers().AddOData(options => options.Select().Filter().OrderBy().SetMaxTop(null).Count().Expand().AddRouteComponents("odata", modelbuilder.GetEdmModel()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseODataBatching();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
