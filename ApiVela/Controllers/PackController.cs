using System;
using Microsoft.AspNetCore.Mvc;
using ApiVela.Models;
using ApiVela.Repository;

namespace ApiVela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackController : ControllerBase
    {
        private readonly RepositoryPacks repo;

        public PackController(RepositoryPacks repo)
        {
            this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        // GET: api/Pack
        [HttpGet]
        [Route("GetPacks")]

        public IActionResult GetPacks()
        {
            var resultado = repo.GetPacks(); // CustomApiResponse<List<Pack>>
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // GET: api/Pack/BuscarPack/{idPack}
        [HttpGet]
        [Route("[action]/{idPack}")]
        public IActionResult BuscarPack(Guid idPack)
        {
            var resultado = repo.BuscarPack(idPack);
            if (resultado.Error != null)
                return NotFound(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // POST: api/Pack
        [HttpPost]
        [Route("InsertarPack")]

        public IActionResult InsertarPack( Pack pack)
        {
            var resultado = repo.InsertarPack(pack);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return CreatedAtAction(nameof(BuscarPack), new { idPack = resultado.Object.IDPack }, resultado.Object);
        }

        // PUT: api/Pack/{id}
        [HttpPut]
        [Route("ActualizarPack/{id}")]

        public IActionResult ActualizarPack(Guid id,  Pack pack)
        {
            if (id != pack.IDPack)
                return BadRequest("El ID del Pack no coincide con el parámetro.");

            var resultado = repo.ActualizarPack(pack);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // DELETE: api/Pack/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletePack(Guid id)
        {
            // Si implementas un método eliminar en tu repositorio:
            // var resultado = repo.EliminarPack(id);
            // if (resultado.Error != null) return BadRequest(resultado.Error.Mensaje);
            // return NoContent();

            return StatusCode(501, "Eliminación no implementada");
        }
    }
}
