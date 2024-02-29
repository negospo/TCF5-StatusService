using Application.Interfaces.RabbitMQ;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Infrastructure.RabbitMQ
{
    public class Listener : IHostedService
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly IPedidoMessageHandler _messageHandler;
        private IConnection _connection;
        private IModel _channel;

        public Listener(string hostname, string queueName, IPedidoMessageHandler messageHandler)
        {
            _hostname = hostname;
            _queueName = queueName;
            _messageHandler = messageHandler;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _messageHandler.HandleMessage(message);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel?.Close();
            _connection?.Close();
            return Task.CompletedTask;
        }
    }
}
