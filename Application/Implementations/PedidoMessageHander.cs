using Application.Interfaces.UseCases;

namespace Application.Implementations
{
    public class PedidoMessageHandler : Interfaces.RabbitMQ.IPedidoMessageHandler
    {
        IPedidoUseCase _pedidoUseCase;

        public PedidoMessageHandler(IPedidoUseCase pedidoUseCase)
        { 
            this._pedidoUseCase = pedidoUseCase;
        }

        public void HandleMessage(string message)
        {
            var newOrder = System.Text.Json.JsonSerializer.Deserialize<DTOs.Imput.Pedido>(message);
            this._pedidoUseCase.Save(newOrder);
        }
    }
}
