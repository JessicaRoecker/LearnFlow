namespace LearnFlow_Servises.Repositorio
{
    public class UploadRepositorio
    {
        private readonly string? _connectionString;

        public UploadRepositorio(IConfiguration _config)
        {
            _connectionString = _config.GetConnectionString("_connectionString");
        }
        

    }
}
