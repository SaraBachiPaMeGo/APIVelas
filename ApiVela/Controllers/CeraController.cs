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
    public class CeraController : ControllerBase
    {
        private readonly RepositoryCeras repo;

        public CeraController(RepositoryCeras repo)
        {
            this.repo = repo;
        }

        // GET: api/Cera
        [HttpGet]
        [Route("GetCeras")]
        public async Task<IActionResult> GetCeras()
        {
            var resultado = await repo.GetCeras();  // supongo que devuelve CustomApiResponse<List>

            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // GET: api/Cera/BuscarCera/{idCera}
        [HttpGet]
        [Route("[action]/{idCera}")]
        public async Task<IActionResult> BuscarCera(Guid idCera)
        {
            var resultado = await repo.BuscarCera(idCera);

            if (resultado.Error != null)
                return NotFound(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // POST: api/Cera
        [HttpPost]
        [Route("InsertarCera")]

        public async Task<IActionResult> InsertarCera( Cera cera)
        {
            var resultado = await repo.InsertarCera(cera);

            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            // Si quieres, puedes devolver CreatedAtAction, si tienes ruta para BuscarCera
            return CreatedAtAction(nameof(BuscarCera), new { idCera = resultado.Object.IDCera }, resultado.Object);
        }

        // PUT: api/Cera/{id}
        [HttpPut("ActualizarCera/{id}")]

        public async Task<IActionResult> ActualizarCera(Guid id,  Cera cera)
        {
            if (id != cera.IDCera)
                return BadRequest("El ID de la Cera no coincide con el parámetro.");

            var resultado = await repo.ActualizarCera(cera);

            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // DELETE: api/Cera/{id}
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var eliminado = await repo.EliminarCera(id);

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
