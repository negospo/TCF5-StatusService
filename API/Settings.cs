namespace API
{
    public class Settings
    {
        /// <summary>
        /// String de conexão com o Postgre
        /// </summary>
        public static string PostgreSQLConnectionString => Environment.GetEnvironmentVariable("POSTGRE_CONNECTION_STRING");

        /// <summary>
        /// String de conexão com o Rabbit
        /// </summary>
        public static string RabbitMQConnectionString => Environment.GetEnvironmentVariable("RABBIT_MQ_CONNECTION_STRING");
    }
}
