using System;
using Microsoft.AspNetCore.Mvc;
using ApiVela.Models;
using ApiVela.Repository;
using System.Threading.Tasks;


namespace ApiVela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly RepositoryClientes repo;

        public ClienteController(RepositoryClientes repo)
        {
            this.repo = repo;
        }

        // GET: api/Cliente
        [HttpGet]
        [Route("GetClientes")]
        public async Task<IActionResult> GetClientes()
        {
            var resultado = await repo.GetClientes();  // debería devolver CustomApiResponse<List<Cliente>>
            if (resultado.Error != null)
            {
                return BadRequest(resultado.Error.Mensaje);
            }
            return Ok(resultado.Object);
        }

        // GET: api/Cliente/BuscarCliente/{idCliente}
        [HttpGet]
        [Route("[action]/{idCliente}")]
        public async Task<IActionResult> BuscarCliente(Guid idCliente)
        {
            var resultado = await repo.BuscarCliente(idCliente);  // CustomApiResponse<Cliente>
            if (resultado.Error != null)
            {
                return NotFound(resultado.Error.Mensaje);
            }
            return Ok(resultado.Object);
        }

        // POST: api/Cliente
        [HttpPost]
        [Route("InsertarCliente")]

        public async Task<IActionResult> InsertarCliente( Cliente cli)
        {
            var resultado = await repo.InsertarCliente(cli);  // CustomApiResponse<Cliente>
            if (resultado.Error != null)
            {
                return BadRequest(resultado.Error.Mensaje);
            }
            // Retornar Created con la ruta para obtener el nuevo cliente
            return CreatedAtAction(nameof(BuscarCliente), new { idCliente = resultado.Object.IDCliente }, resultado.Object);
        }

        // PUT: api/Cliente/{id}
        [HttpPut]
        [Route("ActualizarCliente/{id}")]

        public async Task<IActionResult> ActualizarCliente(Guid id,  Cliente cli)
        {
            if (id != cli.IDCliente)
            {
                return BadRequest("El ID del cliente no coincide.");
            }

            var resultado = await repo.ActualizarCliente(cli);  // CustomApiResponse<Cliente>
            if (resultado.Error != null)
            {
                return BadRequest(resultado.Error.Mensaje);
            }
            return Ok(resultado.Object);
        }

        // DELETE: api/Cliente/{id}
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var eliminado = await repo.EliminarCliente(id);

            if (!eliminado.Object)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró el registro a eliminar"
                });
            }

            return Ok(new
            {
                mensaje = "Registro eliminado correctamente"
            });
        }
    }
}
