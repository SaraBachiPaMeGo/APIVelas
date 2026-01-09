using System;
using Microsoft.AspNetCore.Mvc;
using ApiVela.Models;
using ApiVela.Repository;

namespace ApiVela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoldeController : ControllerBase
    {
        private readonly RepositoryMoldes repo;

        public MoldeController(RepositoryMoldes repo)
        {
            this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        // GET: api/Molde
        [HttpGet]
        [Route("GetMoldes")]

        public IActionResult GetMoldes()
        {
            var resultado = repo.GetMoldes();  // CustomApiResponse<List<Molde>>
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // GET: api/Molde/BuscarMolde/{idMolde}
        [HttpGet]
        [Route("[action]/{idMolde}")]
        public IActionResult BuscarMolde(Guid idMolde)
        {
            var resultado = repo.BuscarMolde(idMolde);
            if (resultado.Error != null)
                return NotFound(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // POST: api/Molde
        [HttpPost]
        [Route("InsertarMolde")]

        public IActionResult InsertarMolde( Molde molde)
        {
            var resultado = repo.InsertarMolde(molde);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return CreatedAtAction(nameof(BuscarMolde), new { idMolde = resultado.Object.IDMolde }, resultado.Object);
        }

        // PUT: api/Molde/{id}
        [HttpPut("ActualizarMolde/{id}")]

        public IActionResult ActualizarMolde(Guid id,  Molde molde)
        {
            if (id != molde.IDMolde)
                return BadRequest("El ID del molde no coincide con el parámetro.");

            var resultado = repo.ActualizarMolde(molde);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // DELETE: api/Molde/{id}
        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(Guid id)
        {
            var eliminado = repo.EliminarMolde(id);

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
