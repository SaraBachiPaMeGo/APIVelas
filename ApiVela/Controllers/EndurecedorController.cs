using System;
using Microsoft.AspNetCore.Mvc;
using ApiVela.Models;
using System.Threading.Tasks;

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
        [Route("GetEndurecedores")]

        public async Task<IActionResult> GetEndurecedores()
        {
            var result = await repo.GetEndurecedor<Endurecedor>();  // supongo devuelve CustomApiResponse<List<Endurecedor>>
            if (result.Error != null)
                return BadRequest(result.Error.Mensaje);

            return Ok(result.Object);
        }

        // GET: api/Endurecedor/BuscarEndurecedor/{idend}
        [HttpGet]
        [Route("[action]/{idend}")]
        public async Task<IActionResult> BuscarEndurecedor(Guid idend)
        {
            var result = await repo.BuscarEndurecedor(idend);
            if (result.Error != null)
                return NotFound(result.Error.Mensaje);

            return Ok(result.Object);
        }

        // POST: api/Endurecedor
        [HttpPost]
        [Route("InsertarEndurecedor")]

        public async Task<IActionResult> InsertarEndurecedor( Endurecedor ent)
        {
            var result = await repo.InsertarEndurecedor(ent);
            if (result.Error != null)
                return BadRequest(result.Error.Mensaje);

            return CreatedAtAction(nameof(BuscarEndurecedor), new { idend = result.Object.IDEndurecedor }, result.Object);
        }

        // PUT: api/Endurecedor/{id}
        [HttpPut]
        [Route("ActualizarEndurecedor/{id}")]

        public async Task<IActionResult> ActualizarEndurecedor(Guid id,  Endurecedor ent)
        {
            if (id != ent.IDEndurecedor)
                return BadRequest("El ID del endurecedor no coincide con el parámetro.");

            var result = await repo.ActualizarEndurecedor(ent);
            if (result.Error != null)
                return BadRequest(result.Error.Mensaje);

            return Ok(result.Object);
        }

        // DELETE: api/Endurecedor/{id}
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var eliminado = await repo.EliminarEndurecedor(id);

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
