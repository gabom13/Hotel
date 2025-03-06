using WebTest5.Components;
using MudBlazor.Services;
using WebTest5.Components.Data;
using WebTest5.Components.Interface;
using WebTest5.Components.Service;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddMudServices();
builder.Services.AddSingleton<DataContext>();
builder.Services.AddScoped<IClientesService, ClientesService>();
builder.Services.AddScoped<IHabitacionesService, HabitacionesService>();
builder.Services.AddScoped<ICategoriasService, CategoriasService>();
builder.Services.AddScoped<IServiciosService, ServicioService>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<IReservasService, ReservasService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IBiService, BiService>();
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
