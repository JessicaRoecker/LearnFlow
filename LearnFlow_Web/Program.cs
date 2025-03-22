using LearnFlow_Web.Components;
using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);



// Adicionar servi�os ao cont�iner
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddBlazorBootstrap();

// Configura��o de CORS para permitir o frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("https://localhost:7018") // Permitir o frontend espec�fico
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configura��o do pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // Aplique HSTS em produ��o
    app.UseHsts();
}

// Habilitar o CORS para o frontend espec�fico
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
