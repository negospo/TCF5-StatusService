namespace Application.Interfaces.RabbitMQ
{
    public interface IPedidoMessageHandler
    {
        void HandleMessage(string message);
    }
}
