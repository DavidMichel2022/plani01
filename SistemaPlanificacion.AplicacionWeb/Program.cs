// Programacion a 5 Capas: a Saber:
// Capa De Aplicacion Web .AplicacionWeb
// Capa De Negocios .BLL (Business Logic Layer)
// Capa De Acceso a Los Datos .DAL (Data Access Layer)
// Capa De Entidad .Entity
// Capa De Indicador de Compromiso .IOC (Indicator Of Compromise)

using SistemaPlanificacion.AplicacionWeb.Utilidades.Automapper;
using SistemaPlanificacion.IOC;

using Microsoft.AspNetCore.Authentication.Cookies;
using SistemaPlanificacion.AplicacionWeb.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Acceso/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });


builder.Services.InyectarDependencia(builder.Configuration);
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Login}/{id?}");

app.Run();
