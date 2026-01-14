using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ApiVela.Models;
using ApiVela.Repository;
using System.Threading.Tasks;


namespace ApiVela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FraganciaController : ControllerBase
    {
        private readonly RepositoryFragancias repo;

        public FraganciaController(RepositoryFragancias repo)
        {
            this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        // GET: api/Fragancia
        [HttpGet]
        [Route("GetFragancias")]

        public async Task<IActionResult> GetFragancias()
        {
            var resultado = await repo.GetFragancias();
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // GET: api/Fragancia/BuscarFragancia/{idFrag}
        [HttpGet]
        [Route("[action]/{idFrag}")]
        public async Task<IActionResult> BuscarFragancia(Guid idFrag)
        {
            var resultado = await repo.BuscarFragancia(idFrag);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // POST: api/Fragancia
        [HttpPost]
        [Route("InsertarFragancia")]

        public async Task<IActionResult> InsertarFragancia(Fragancia frag)
        {
            var resultado = await repo.InsertarFragancia(frag);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // PUT: api/Fragancia/{id}
        [HttpPut]
        [Route("ActualizarFragancia/{id}")]

        public async Task<IActionResult> ActualizarFragancia( Fragancia frag)
        {
            var resultado = await repo.ActualizarFragancia(frag);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // DELETE: api/Fragancia/{id}
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var eliminado = await repo.EliminarFrag(id);

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