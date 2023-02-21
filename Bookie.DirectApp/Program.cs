using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bookie.DirectApp.Data;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Bookie.DirectApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookieDirectAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookieDirectAppContext") ?? throw new InvalidOperationException("Connection string 'BookieDirectAppContext' not found.")));

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<IAuthorsService, AuthorsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
