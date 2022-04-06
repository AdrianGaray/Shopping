using Microsoft.EntityFrameworkCore;
using Shopping.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ======= CONFIGURACION DE LA BASE DE DATOS ============
// Se agrega al builder, otro servicio
// la o es de options
// le decimos que el datacontext va ser de tipo sqlserver
// Y le decimos que string de conexion va a usar
// ======= CONFIGURAMOS LA INTECCION DE INDEPENDENCIA ============
builder.Services.AddDbContext<DataContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// sea grega el servicio AddRazorPages, es un cambio para los developers
// nos permite modificcar la vista sin parar el proyecto
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

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
