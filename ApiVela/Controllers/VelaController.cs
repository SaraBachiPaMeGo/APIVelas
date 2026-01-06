using System;
using Microsoft.AspNetCore.Mvc;
using ApiVela.Models;
using ApiVela.Repository;

namespace ApiVela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VelaController : ControllerBase
    {
        private readonly RepositoryVelas repo;

        public VelaController(RepositoryVelas repo)
        {
            this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        // GET: api/Vela
        [HttpGet]
        [Route("GetVelas")]

        public IActionResult GetVelas()
        {
            var resultado = repo.GetVelas(); // CustomApiResponse<List<VelaDTO>>
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // GET: api/Vela/BuscarVela/{idVela}
        [HttpGet]
        [Route("[action]/{idVela}")]
        public IActionResult BuscarVela(Guid idVela)
        {
            var resultado = repo.BuscarVela(idVela);
            if (resultado.Error != null)
                return NotFound(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // POST: api/Vela
        [HttpPost]
        [Route("InsertarVela")]

        public IActionResult InsertarVela( Vela vela)
        {
            var resultado = repo.InsertarVela(vela);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return CreatedAtAction(nameof(BuscarVela), new { idVela = resultado.Object.IDVela }, resultado.Object);
        }

        // PUT: api/Vela/{id}
        [HttpPut]
        [Route("ActualizarVela/{id}")]

        public IActionResult ActualizarVela(Guid id,  Vela vela)
        {
            if (id != vela.IDVela)
                return BadRequest("El ID de la vela no coincide con el parámetro.");

            var resultado = repo.ActualizarVela(vela);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // DELETE: api/Vela/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            // Puedes implementar la lógica de eliminar aquí si la tienes en el repositorio
            return StatusCode(501, "Eliminación no implementada");
        }
    }
}
