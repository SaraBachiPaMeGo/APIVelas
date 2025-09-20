using System;
using Microsoft.AspNetCore.Mvc;
using ApiVela.Models;
using ApiVela.Repository;

namespace ApiVela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PigmentoController : ControllerBase
    {
        private readonly RepositoryPigmentos repo;

        public PigmentoController(RepositoryPigmentos repo)
        {
            this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        // GET: api/Pigmento
        [HttpGet]
        [Route("api/GetPigmentos")]

        public IActionResult GetPigmentos()
        {
            var resultado = repo.GetPigmentos();  // Debe devolver CustomApiResponse<List<Pigmento>>
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // GET: api/Pigmento/BuscarPigmento/{idPigmento}
        [HttpGet]
        [Route("[action]/{idPigmento}")]
        public IActionResult BuscarPigmento(Guid idPigmento)
        {
            var resultado = repo.BuscarPigmento<Pigmento>(idPigmento);
            if (resultado.Error != null)
                return NotFound(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // POST: api/Pigmento
        [HttpPost]
        [Route("api/InsertarPigmento")]

        public IActionResult InsertarPigmento( Pigmento pigmento)
        {
            var resultado = repo.InsertarPigmento<Pigmento>(pigmento);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return CreatedAtAction(nameof(BuscarPigmento), new { idPigmento = resultado.Object.IDPig }, resultado.Object);
        }

        // PUT: api/Pigmento/{id}
        [HttpPut("{id}")]
        [Route("api/ActualizarPigmento")]

        public IActionResult ActualizarPigmento(Guid id,  Pigmento pigmento)
        {
            if (id != pigmento.IDPig)
                return BadRequest("El ID del pigmento no coincide con el parámetro.");

            var resultado = repo.ActualizarPigmento<Pigmento>(pigmento);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // DELETE: api/Pigmento/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletePigmento(Guid id)
        {
            // Si implementas un método eliminar en tu repositorio:
            // var resultado = repo.EliminarPigmento(id);
            // if (resultado.Error != null) return BadRequest(resultado.Error.Mensaje);
            // return NoContent();

            return StatusCode(501, "Eliminación no implementada");
        }
    }
}
