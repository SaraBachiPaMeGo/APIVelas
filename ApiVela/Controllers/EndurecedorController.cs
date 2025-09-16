using System;
using Microsoft.AspNetCore.Mvc;
using ApiVela.Models;
using ApiVela.Repository;

namespace ApiVela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndurecedorController : ControllerBase
    {
        private readonly RepositoryEndurecedores repo;

        public EndurecedorController(RepositoryEndurecedores repo)
        {
            this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        // GET: api/Endurecedor
        [HttpGet]
        public IActionResult GetEndurecedores()
        {
            var result = repo.GetEndurecedor<Endurecedor>();  // supongo devuelve CustomApiResponse<List<Endurecedor>>
            if (result.Error != null)
                return BadRequest(result.Error.Mensaje);

            return Ok(result.Object);
        }

        // GET: api/Endurecedor/BuscarEndurecedor/{idend}
        [HttpGet]
        [Route("[action]/{idend}")]
        public IActionResult BuscarEndurecedor(Guid idend)
        {
            var result = repo.BuscarEndurecedor<Endurecedor>(idend);
            if (result.Error != null)
                return NotFound(result.Error.Mensaje);

            return Ok(result.Object);
        }

        // POST: api/Endurecedor
        [HttpPost]
        public IActionResult InsertarEndurecedor( Endurecedor ent)
        {
            var result = repo.InsertarEndurecedor<Endurecedor>(ent);
            if (result.Error != null)
                return BadRequest(result.Error.Mensaje);

            return CreatedAtAction(nameof(BuscarEndurecedor), new { idend = result.Object.IDEndurecedor }, result.Object);
        }

        // PUT: api/Endurecedor/{id}
        [HttpPut("{id}")]
        public IActionResult ActualizarEndurecedor(Guid id,  Endurecedor ent)
        {
            if (id != ent.IDEndurecedor)
                return BadRequest("El ID del endurecedor no coincide con el parámetro.");

            var result = repo.ActualizarEndurecedor<Endurecedor>(ent);
            if (result.Error != null)
                return BadRequest(result.Error.Mensaje);

            return Ok(result.Object);
        }

        // DELETE: api/Endurecedor/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteEndurecedor(Guid id)
        {
            // Si tienes algún método eliminar en tu repositorio:
            // var result = repo.EliminarEndurecedor(id);
            // if (result.Error != null) return BadRequest(result.Error.Mensaje);
            // return NoContent();

            return StatusCode(501, "Eliminación no implementada");
        }
    }
}
