using System;
using Microsoft.AspNetCore.Mvc;
using ApiVela.Models;
using ApiVela.Repository;

namespace ApiVela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MechaController : ControllerBase
    {
        private readonly RepositoryMechas repo;

        public MechaController(RepositoryMechas repo)
        {
            this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        // GET: api/Mecha
        [HttpGet]
        [Route("GetMechas")]

        public IActionResult GetMechas()
        {
            var resultado = repo.GetMechas();  // CustomApiResponse<List<Mecha>>
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // GET: api/Mecha/BuscarMecha/{idMecha}
        [HttpGet]
        [Route("[action]/{idMecha}")]
        public IActionResult BuscarMecha(Guid idMecha)
        {
            var resultado = repo.BuscarMecha(idMecha);
            if (resultado.Error != null)
                return NotFound(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // POST: api/Mecha
        [HttpPost]
        [Route("InsertarMecha")]

        public IActionResult InsertarMecha( Mecha mech)
        {
            var resultado = repo.InsertarMecha(mech);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return CreatedAtAction(nameof(BuscarMecha), new { idMecha = resultado.Object.IDMecha }, resultado.Object);
        }

        // PUT: api/Mecha/{id}
        [HttpPut]
        [Route("ActualizarMecha/{id}")]

        public IActionResult ActualizarMecha(Guid id,  Mecha mech)
        {
            if (id != mech.IDMecha)
                return BadRequest("El ID de la Mecha no coincide con el parámetro.");

            var resultado = repo.ActualizarMecha(mech);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // DELETE: api/Mecha/{id}
        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(Guid id)
        {
            var eliminado = repo.EliminarMecha(id);

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
