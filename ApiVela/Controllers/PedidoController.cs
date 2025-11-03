using System;
using Microsoft.AspNetCore.Mvc;
using ApiVela.Models;
using ApiVela.Repository;

namespace ApiVela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly RepositoryPedidos repo;

        public PedidoController(RepositoryPedidos repo)
        {
            this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        // GET: api/Pedido
        [HttpGet]
        [Route("GetPedidos")]

        public IActionResult GetPedidos()
        {
            var resultado = repo.GetPedidos();  // CustomApiResponse<List<Pedido>>
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // GET: api/Pedido/BuscarPedido/{idPedido}
        [HttpGet]
        [Route("[action]/{idPedido}")]
        public IActionResult BuscarPedido(Guid idPedido)
        {
            var resultado = repo.BuscarPedido(idPedido);
            if (resultado.Error != null)
                return NotFound(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // POST: api/Pedido
        [HttpPost]
        [Route("InsertarPedido")]

        public IActionResult InsertarPedido( Pedido pedi)
        {
            // Supongo que haces un DTO o Request object para recibir los datos
            var resultado = repo.InsertarPedido(pedi);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            // En este caso, Pedido tiene un GUID nuevo generado, asumiré que Object contiene esa nueva entidad
            return CreatedAtAction(nameof(BuscarPedido), new { idPedido = resultado.Object.IDPedido }, resultado.Object);
        }

        // PUT: api/Pedido/{id}
        [HttpPut]
        [Route("ActualizarPedido/{id}")]

        public IActionResult ActualizarPedido(Guid id, Pedido pedi)
        {

            if (id != pedi.IDPedido)
                return BadRequest("El ID del pedido no coincide.");

            var resultado = repo.ActualizarPedido(pedi);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // DELETE: api/Pedido/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletePedido(Guid id)
        {
            // Si implementas un método de eliminar en el repositorio:
            // var resultado = repo.EliminarPedido(id);
            // if (resultado.Error != null) return BadRequest(resultado.Error.Mensaje);
            // return NoContent();

            return StatusCode(501, "Eliminación no implementada");
        }
    }

}
