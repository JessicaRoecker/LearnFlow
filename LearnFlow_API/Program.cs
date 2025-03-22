using LearnFlow_Service.DTOs;
using Microsoft.OpenApi.Models;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        // Configuração do Serilog
        var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(Path.Combine(logDirectory, "log.txt"), rollingInterval: RollingInterval.Day)
            .CreateLogger();

        // Criação do builder e configuração do Serilog
        var builder = WebApplication.CreateBuilder(args);

        // Configura o Serilog como o logger padrão
        builder.Host.UseSerilog();

        // Adiciona os serviços ao contêiner
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        // Adiciona o Swagger
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
        });

        // Injeção de dependência básica (exemplo)
        builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

        var app = builder.Build();

        // Configuração do pipeline HTTP
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger(); // Ativa o middleware do Swagger
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });
        }
        else
        {
            // Para produção, você pode configurar um handler para erros genéricos
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts(); // HSTS em produção
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
