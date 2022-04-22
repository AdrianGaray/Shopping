using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;
using Shopping.Data.Entities;
using Shopping.Helpers;

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


//TODO: Make strongest password
builder.Services.AddIdentity<User, IdentityRole>(cfg =>
{
    cfg.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
    cfg.SignIn.RequireConfirmedEmail = true;
    cfg.User.RequireUniqueEmail = true; // email unico
    cfg.Password.RequireDigit = false; // condiciones del password
    cfg.Password.RequiredUniqueChars = 0;
    cfg.Password.RequireLowercase = false;
    cfg.Password.RequireNonAlphanumeric = false;
    cfg.Password.RequireUppercase = false;
    // bloqueo de usuarios por intento falllidos
    //cfg.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
    cfg.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    cfg.Lockout.MaxFailedAccessAttempts = 3; // cantidad de intentos
    cfg.Lockout.AllowedForNewUsers = true;
})
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<DataContext>();

// Problema de Login a la accion NotAuthorized
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/NotAuthorized";
    options.AccessDeniedPath = "/Account/NotAuthorized";
});

// Transient: la voy a usar una sola vez. Lo inyecta una vez y lo destruye cuando no lo necesita
// Scope: la inyecta cada vez q la llame y lo destruye cuando lo deja de usar
// Singleton: la inyecta una vez y no lo destruye, lo deja en memoria
builder.Services.AddTransient<SeedDb>();
builder.Services.AddScoped<IUserHelper, UserHelper>();
builder.Services.AddScoped<ICombosHelper, CombosHelper>();
builder.Services.AddScoped<IBlobHelper, BlobHelper>();
builder.Services.AddScoped<IMailHelper, MailHelper>();


// sea grega el servicio AddRazorPages, es un cambio para los developers
// nos permite modificcar la vista sin parar el proyecto
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();
SeedData();

void SeedData()
{
    IServiceScopeFactory? scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (IServiceScope? scope = scopedFactory.CreateScope())
    {
        SeedDb? service = scope.ServiceProvider.GetService<SeedDb>();
        service.SeedAsync().Wait();
    }

}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// se ejcuta en el homecontroller
app.UseStatusCodePagesWithReExecute("/error/{0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
