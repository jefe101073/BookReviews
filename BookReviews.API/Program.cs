using BookReviews.Data;
using BookReviews.Interfaces;
using BookReviews.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Entity Framework - Add DbContext to services - Setup Connection string from appsettings.json "BookReviewsDb"
builder.Services.AddDbContext<BookReviewDataContext>(
    z => z.UseNpgsql(builder.Configuration.GetConnectionString("BookReviewsDb")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Jeff McCann - Book Reviews",
            Version = "1"
        }
     );
    // Default path for Swagger documentation
    var filePath = Path.Combine(System.AppContext.BaseDirectory, "BookReviews.API.xml");
    c.IncludeXmlComments(filePath);
});
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
