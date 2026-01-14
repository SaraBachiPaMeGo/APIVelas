using System;
using Microsoft.AspNetCore.Mvc;
using ApiVela.Models;
using ApiVela.Repository;
using System.Threading.Tasks;


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

        public async Task<IActionResult> GetVelas()
        {
            var resultado = await repo.GetVelas(); // CustomApiResponse<List<VelaDTO>>
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // GET: api/Vela/BuscarVela/{idVela}
        [HttpGet]
        [Route("[action]/{idVela}")]
        public async Task<IActionResult> BuscarVela(Guid idVela)
        {
            var resultado = await repo.BuscarVela(idVela);
            if (resultado.Error != null)
                return NotFound(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // POST: api/Vela
        [HttpPost]
        [Route("InsertarVela")]

        public async Task<IActionResult> InsertarVela( Vela vela)
        {
            var resultado = await repo.InsertarVela(vela);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return CreatedAtAction(nameof(BuscarVela), new { idVela = resultado.Object.IDVela }, resultado.Object);
        }

        // PUT: api/Vela/{id}
        [HttpPut]
        [Route("ActualizarVela/{id}")]

        public async Task<IActionResult> ActualizarVela(Guid id,  Vela vela)
        {
            if (id != vela.IDVela)
                return BadRequest("El ID de la vela no coincide con el parámetro.");

            var resultado = await repo.ActualizarVela(vela);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // DELETE: api/Vela/{id}
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var eliminado = await repo.EliminarVela(id);

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
