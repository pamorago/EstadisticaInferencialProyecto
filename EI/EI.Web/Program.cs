using EI.Web.Data;
using EI.Web.Repository.Implementations;
using EI.Web.Repository.Interfaces;
using EI.Web.Services.Implementations;
using EI.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

// Forzar cultura invariante para que el model binder
// parsee correctamente los valores decimales con '.' de los inputs range
var invariantCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentCulture = invariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = invariantCulture;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("SqlServerDataBase");
builder.Services.AddDbContext<EIContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddTransient<IRepositoryPaciente, RepositoryPaciente>();
builder.Services.AddTransient<IServiceEstadisticas, ServiceEstadisticas>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
