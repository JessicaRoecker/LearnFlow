using LearFlow.Web.Service;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os servi�os necess�rios ao cont�iner de depend�ncias
builder.Services.AddRazorPages(); // Para Razor Pages
builder.Services.AddControllersWithViews(); // Para Controllers e Views
builder.Services.AddHttpClient<LearnFlowServise>(); // Configura o HttpClient
builder.Services.AddDistributedMemoryCache(); // Configura cache
builder.Services.AddSession(options => // Configura sess�es
{
    options.IdleTimeout = TimeSpan.FromSeconds(1800);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configura o pipeline de requisi��es HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); // P�gina de erro personalizada para produ��o
    app.UseHsts(); // HSTS em produ��o
}

app.UseHttpsRedirection(); // Redireciona para HTTPS
app.UseStaticFiles(); // Servir arquivos est�ticos (CSS, JS, imagens)

app.UseRouting(); // Roteamento das requisi��es

app.UseAuthorization(); // Autoriza��o para acessar p�ginas protegidas

app.UseSession(); // Habilita sess�es

// Configura os endpoints (como rotas de controllers e Razor Pages)
app.MapRazorPages(); // Roteamento para Razor Pages
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Rota para Controllers

// Inicia a aplica��o
app.Run();
