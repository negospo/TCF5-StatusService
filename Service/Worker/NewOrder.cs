using RabbitMQ.Client;

namespace Service.Worker
{
    public class NewOrder : BackgroundService
    {
        private readonly ILogger<NewOrder> _logger;
        private IConnection _connection;
        private IModel _channel;


        public NewOrder(ILogger<NewOrder> logger)
        {
            _logger = logger;
            this.InitRabbitMQ();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker NewOrder running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }


        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory() {  HostName = "localhost" };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "new-order",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            _channel.BasicQos(0, 1, false);
        }

    }
}