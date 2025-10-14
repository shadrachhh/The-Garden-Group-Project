using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NOSQL_Project__Incident_management_system.Data;
using NOSQL_Project__Incident_management_system.Repositories;

var builder = WebApplication.CreateBuilder(args);

//  Add controllers and views
builder.Services.AddControllersWithViews();

//  Register MongoDB context and repositories
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<TicketRepository>();
builder.Services.AddScoped<EmployeeRepository>();



// Load environment variables from .env file (optional)
DotNetEnv.Env.Load();

var app = builder.Build();

//  Error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tickets}/{action=Index}/{id?}");

app.Run();
