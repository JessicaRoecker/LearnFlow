using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

        // Obter o caminho do diretório atual (pasta onde o programa está sendo executado)
        var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");

        // Verificar se a pasta Logs existe, caso contrário, criar
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }

        // Configuração do Serilog
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console() // Log no console
            .WriteTo.File(Path.Combine(logDirectory, "log.txt"), rollingInterval: RollingInterval.Day) // Salvar os logs na pasta "Logs" dentro de Debug
            .CreateLogger();


        var app = builder.Build();

      
        // Configuração do pipeline HTTP
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
