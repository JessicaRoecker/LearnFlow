using LearnFlow_Service.DTOs;
using Microsoft.OpenApi.Models;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        // Configura��o do Serilog
        var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(Path.Combine(logDirectory, "log.txt"), rollingInterval: RollingInterval.Day)
            .CreateLogger();

        // Cria��o do builder e configura��o do Serilog
        var builder = WebApplication.CreateBuilder(args);

        // Configura o Serilog como o logger padr�o
        builder.Host.UseSerilog();

        // Adiciona os servi�os ao cont�iner
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        // Adiciona o Swagger
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
        });

        // Inje��o de depend�ncia b�sica (exemplo)
        builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

        var app = builder.Build();

        // Configura��o do pipeline HTTP
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
            // Para produ��o, voc� pode configurar um handler para erros gen�ricos
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts(); // HSTS em produ��o
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
