﻿namespace Application.Interfaces.UseCases
{
    public interface IPedidoUseCase
    {
        public IEnumerable<Application.DTOs.Output.Pedido> List(List<int> pedidoIds);
        public IEnumerable<Application.DTOs.Output.Pedido> ListByStatus(Application.Enums.PedidoStatus status);

        public Application.DTOs.Output.Pedido Get(int pedidoId);
        public bool Save(DTOs.Imput.Pedido pedido);
    }
}
