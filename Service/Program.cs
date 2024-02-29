namespace Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                    Infrastructure.Persistence.Settings.PostgreSQLConnectionString = Settings.PostgreSQLConnectionString;
                    services.AddTransient<Application.Interfaces.Repositories.IPedidoRepository, Infrastructure.Persistence.Repositories.Pedido>();
                    services.AddTransient<Application.Interfaces.UseCases.IPedidoUseCase, Application.Implementations.PedidoUseCase>();
                    RunSQLScript();
                    services.AddHostedService<Worker.NewOrder>();
                })
                .Build();

            host.Run();
        }


        static void RunSQLScript()
        {
            var _connPg = Infrastructure.Persistence.Database.Connection();
            var filePath = Path.GetFullPath(@"./Fiap.sql");
            FileInfo file = new FileInfo(filePath);
            string script = file.OpenText().ReadToEnd();
            var db_cmd = Infrastructure.Persistence.Database.Command(script, _connPg);
            _connPg.Open();
            db_cmd.ExecuteNonQuery();
            _connPg.Close();
        }
    }
}