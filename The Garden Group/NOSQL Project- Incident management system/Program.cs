using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NOSQL_Project__Incident_management_system.Data;
using NOSQL_Project__Incident_management_system.Repositories;
using NOSQL_Project__Incident_management_system.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


builder.Services.AddSession();

// Register MongoDB context and repositories
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<TicketRepository>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddSingleton<TicketSorter>();


// Load environment variables from .env file (optional)
DotNetEnv.Env.Load();

var app = builder.Build();

// Error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// i Enable session B4 Authorization 
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
