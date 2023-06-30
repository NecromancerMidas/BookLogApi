using System.ComponentModel.DataAnnotations;
using System.Reflection;
using BookLogApi.Data;
using BookLogApi.Model;
using BookLogApi.Repositories;
using BookLogApi.UnitOfWork;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*var db = new List<Book>
{
    new ("Meditations", "Marcus Aurelius",
        "Gregory Hays", "Modern Library",
        "Philosophy","Stoicism","Good fucking book.",5),
    new ("Epictetus Discourses", "Book 1-2",
    "W.A Oldfather", "Loeb Classical Library",
    "Philosophy","Stoicism","Good fucking book.",5)

    
};*/
/*builder.Services.AddScoped<IBooksRepository>(sp =>
    new BooksRepository(builder.Configuration.GetConnectionString("DefaultConnection")));*/
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BooksContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        context.Response.StatusCode = 500;
        context.Response.ContentType = "text/html";
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature?.Error is Exception e)
        {
         logger.LogError(e, "an unhandled exception occured dont see how this helps me.");
        }

        await context.Response.WriteAsync("an error occurred still dunno how this info helps me.");
    });
});
// Configure the HTTP request pipeline.
app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("MyCorsPolicy");

//app.UseHttpsRedirection();



app.UseAuthorization();

app.MapControllers();

//app.MapGet("/books", () => db.ToArray()).RequireCors("MyCorsPolicy");
/*app.MapPost("/books", async ([FromForm]Book book) =>
    {
       *//* var form = await req.ReadFormAsync();*/
       /* var book = new Book(
            form["Title"],
            form["SubTitle"],
            form["Author"],
            form["Publisher"],
            form["Genre"],
            form["Subject"],
            form["Description"],
            int.Parse(form["Rating"]));*//*
    db.Add(book);
    return Results.Ok();
    }
);*/
/*app.MapDelete("/books/{bookToDeleteId}", (string bookToDeleteId) =>
{
    db.Remove(db.First(book => book.Id == Guid.Parse(bookToDeleteId)));
    return Results.Ok();
});*/
/*app.MapPut("/books/{bookToEditId}", async (string bookToEditId, Book sentBook) =>
{
    Console.WriteLine(bookToEditId);
    var index = db.Find(book => book.Id.ToString() == bookToEditId);
    var book = db.Find(book => book.Id == Guid.Parse(bookToEditId));

    book.Title = sentBook.Title;
    book.SubTitle = sentBook.SubTitle;
    book.Author = sentBook.Author;
    book.Publisher = sentBook.Publisher;
    book.Genre = sentBook.Genre;
    book.Subject = sentBook.Subject;
    book.Description = sentBook.Description;
    book.Rating = sentBook.Rating;
});*/

app.Run();
