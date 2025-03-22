using LearFlow.Web.Service;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços necessários ao contêiner de dependências
builder.Services.AddRazorPages(); // Para Razor Pages
builder.Services.AddControllersWithViews(); // Para Controllers e Views
builder.Services.AddHttpClient<LearnFlowServise>(); // Configura o HttpClient
builder.Services.AddDistributedMemoryCache(); // Configura cache
builder.Services.AddSession(options => // Configura sessões
{
    options.IdleTimeout = TimeSpan.FromSeconds(1800);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configura o pipeline de requisições HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); // Página de erro personalizada para produção
    app.UseHsts(); // HSTS em produção
}

app.UseHttpsRedirection(); // Redireciona para HTTPS
app.UseStaticFiles(); // Servir arquivos estáticos (CSS, JS, imagens)

app.UseRouting(); // Roteamento das requisições

app.UseAuthorization(); // Autorização para acessar páginas protegidas

app.UseSession(); // Habilita sessões

// Configura os endpoints (como rotas de controllers e Razor Pages)
app.MapRazorPages(); // Roteamento para Razor Pages
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Rota para Controllers

// Inicia a aplicação
app.Run();
