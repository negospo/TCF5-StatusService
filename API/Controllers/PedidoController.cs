using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("Pedido")]
    public class PedidoController : ControllerBase
    {
        private readonly Application.Interfaces.UseCases.IPedidoUseCase _pedidoUseCase;

        public PedidoController(Application.Interfaces.UseCases.IPedidoUseCase pedidoUseCase)
        {
            this._pedidoUseCase = pedidoUseCase;
        }


        /// <summary>
        /// Lista o pedido status passado por parâmetro
        /// </summary>
        /// <response code="404" >Pedido não encontrado</response> 
        [HttpGet]
        [Route("{pedidoId}")]
        [ProducesResponseType(typeof(Application.DTOs.Output.Pedido), 200)]
        public ActionResult<IEnumerable<Application.DTOs.Output.Pedido>> Get(int pedidoId)
        {
            try
            {
                return Ok(_pedidoUseCase.Get(pedidoId));
            }
            catch (Application.CustomExceptions.NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Lista todos os pedidos status passados por parâmetro.
        /// </summary>
        /// <param name="pedidoIds">Ids dos pedidos</param>
        [HttpPost]
        [Route("list")]
        [ProducesResponseType(typeof(IEnumerable<Application.DTOs.Output.Pedido>), 200)]
        public ActionResult<IEnumerable<Application.DTOs.Output.Pedido>> List(List<int> pedidoIds)
        {
            return Ok(_pedidoUseCase.List(pedidoIds));
        }

        /// <summary>
        /// Lista todos os pedidos status de um status específico
        /// </summary>
        /// <param name="status">Status do pedido</param>
        [HttpPost]
        [Route("listByStatus")]
        [ProducesResponseType(typeof(IEnumerable<Application.DTOs.Output.Pedido>), 200)]
        public ActionResult<IEnumerable<Application.DTOs.Output.Pedido>> ListByStatus(Application.Enums.PedidoStatus status)
        {
            return Ok(_pedidoUseCase.ListByStatus(status));
        }
    }
}
