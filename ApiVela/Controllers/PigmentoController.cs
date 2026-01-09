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
        [Route("GetPigmentos")]

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
        [Route("InsertarPigmento")]

        public IActionResult InsertarPigmento( Pigmento pigmento)
        {
            var resultado = repo.InsertarPigmento<Pigmento>(pigmento);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return CreatedAtAction(nameof(BuscarPigmento), new { idPigmento = resultado.Object.IDPig }, resultado.Object);
        }

        // PUT: api/Pigmento/{id}
        [HttpPut]
        [Route("ActualizarPigmento/{id}")]

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
        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(Guid id)
        {
            var eliminado = repo.EliminarPig(id);

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
