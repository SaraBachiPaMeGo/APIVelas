using System;
using Microsoft.AspNetCore.Mvc;
using ApiVela.Models;
using ApiVela.Repository;
using System.Threading.Tasks;


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

        public async Task<IActionResult> GetMechas()
        {
            var resultado = await repo.GetMechas();  // CustomApiResponse<List<Mecha>>
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // GET: api/Mecha/BuscarMecha/{idMecha}
        [HttpGet]
        [Route("[action]/{idMecha}")]
        public async Task<IActionResult> BuscarMecha(Guid idMecha)
        {
            var resultado = await repo.BuscarMecha(idMecha);
            if (resultado.Error != null)
                return NotFound(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // POST: api/Mecha
        [HttpPost]
        [Route("InsertarMecha")]

        public async Task<IActionResult> InsertarMecha( Mecha mech)
        {
            var resultado = await repo.InsertarMecha(mech);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return CreatedAtAction(nameof(BuscarMecha), new { idMecha = resultado.Object.IDMecha }, resultado.Object);
        }

        // PUT: api/Mecha/{id}
        [HttpPut]
        [Route("ActualizarMecha/{id}")]

        public async Task<IActionResult> ActualizarMecha(Guid id,  Mecha mech)
        {
            if (id != mech.IDMecha)
                return BadRequest("El ID de la Mecha no coincide con el parámetro.");

            var resultado = await repo.ActualizarMecha(mech);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // DELETE: api/Mecha/{id}
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var eliminado = await repo.EliminarMecha(id);

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
