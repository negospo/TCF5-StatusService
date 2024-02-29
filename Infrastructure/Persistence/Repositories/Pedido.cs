using Application.Enums;
using Application.Interfaces.Repositories;
using Dapper;

namespace Infrastructure.Persistence.Repositories
{
    public class Pedido : Application.Interfaces.Repositories.IPedidoRepository
    {
        IEnumerable<Application.DTOs.Output.Pedido> IPedidoRepository.List(List<int> pedidoIds)
        {
            string query = "select pedido_id,pedido_status_id as status from pedido where pedido_id = any(@pedidoIds)";
            var payments = Database.Connection().Query<Application.DTOs.Output.Pedido>(query, new { pedidoIds = pedidoIds });
            return payments;
        }

        public IEnumerable<Application.DTOs.Output.Pedido> ListByStatus(PedidoStatus status)
        {
            string query = "select pedido_id,pedido_status_id as status from pedido where pedido_status_id = @status";
            var payments = Database.Connection().Query<Application.DTOs.Output.Pedido>(query, new { status = (int)status });
            return payments;
        }

        Application.DTOs.Output.Pedido IPedidoRepository.Get(int pedidoId)
        {
            string query = "select pedido_id,pedido_status_id as status from pedido where pedido_id = @pedido_id";
            var item = Database.Connection().QueryFirstOrDefault<Application.DTOs.Output.Pedido>(query, new { pedido_id = pedidoId });
            return item;
        }

        bool IPedidoRepository.Save(Domain.Entities.PedidoStatus pedido)
        {
            string queryExists = "select true from pedido where pedido_id = @pedidoId";
            string quryInsert = "insert into pedido (pedido_id,pedido_status_id,created_at) values (@pedido_id,@pedido_status_id,now())";
            string queryUpdate = "update pedido set pedido_status_id = @pedido_status_id, updated_at = now() where pedido_id = @pedido_id";

            var exists = Database.Connection().QueryFirstOrDefault<bool>(queryExists, new { pedidoId = pedido.PedidoId });
            if (exists)
            {
                Database.Connection().Execute(queryUpdate, new
                {
                    pedido_id = pedido.PedidoId,
                    pedido_status_id = pedido.Status
                });
            }
            else
            {
                Database.Connection().Execute(quryInsert, new
                {
                    pedido_id = pedido.PedidoId,
                    pedido_status_id = pedido.Status
                });
            }
          
            return true;
        }
    }
}
