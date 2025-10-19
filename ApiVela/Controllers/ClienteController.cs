using System;
using Microsoft.AspNetCore.Mvc;
using ApiVela.Models;
using ApiVela.Repository;

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
        [Route("api/GetClientes")]
        public IActionResult GetClientes()
        {
            var resultado = repo.GetClientes();  // debería devolver CustomApiResponse<List<Cliente>>
            if (resultado.Error != null)
            {
                return BadRequest(resultado.Error.Mensaje);
            }
            return Ok(resultado.Object);
        }

        // GET: api/Cliente/BuscarCliente/{idCliente}
        [HttpGet]
        [Route("[action]/{idCliente}")]
        public IActionResult BuscarCliente(Guid idCliente)
        {
            var resultado = repo.BuscarCliente(idCliente);  // CustomApiResponse<Cliente>
            if (resultado.Error != null)
            {
                return NotFound(resultado.Error.Mensaje);
            }
            return Ok(resultado.Object);
        }

        // POST: api/Cliente
        [HttpPost]
        [Route("api/InsertarCliente")]

        public IActionResult InsertarCliente( Cliente cli)
        {
            var resultado = repo.InsertarCliente(cli);  // CustomApiResponse<Cliente>
            if (resultado.Error != null)
            {
                return BadRequest(resultado.Error.Mensaje);
            }
            // Retornar Created con la ruta para obtener el nuevo cliente
            return CreatedAtAction(nameof(BuscarCliente), new { idCliente = resultado.Object.IDCliente }, resultado.Object);
        }

        // PUT: api/Cliente/{id}
        [HttpPut]
        [Route("api/ActualizarCliente/{id}")]

        public IActionResult ActualizarCliente(Guid id,  Cliente cli)
        {
            if (id != cli.IDCliente)
            {
                return BadRequest("El ID del cliente no coincide.");
            }

            var resultado = repo.ActualizarCliente(cli);  // CustomApiResponse<Cliente>
            if (resultado.Error != null)
            {
                return BadRequest(resultado.Error.Mensaje);
            }
            return Ok(resultado.Object);
        }

        // DELETE: api/Cliente/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCliente(Guid id)
        {
            // Si agregas un método de eliminar en repo, lo usarías así:
            // var resultado = repo.EliminarCliente(id);
            // if (resultado.Error != null) return BadRequest(resultado.Error.Mensaje);
            // return NoContent();

            return StatusCode(501, "El método DELETE no está implementado");
        }
    }
}
