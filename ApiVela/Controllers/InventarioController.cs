using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVela.Models;
using ApiVela.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiVela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly RepositoryInventarios repo;

        public InventarioController(RepositoryInventarios repo)
        {
            this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        // GET: api/Inventario
        [HttpGet]
        [Route("GetInventarios")]

        public IActionResult GetInventarios()
        {
            var resultado = repo.GetInventarios();  // CustomApiResponse<List<Inventario>>
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // GET: api/Inventario/BuscarInventario/{idInventario}
        [HttpGet]
        [Route("[action]/{idInventario}")]
        public IActionResult BuscarInventario(Guid idInventario)
        {
            var resultado = repo.BuscarInventario(idInventario);
            if (resultado.Error != null)
                return NotFound(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // POST: api/Inventario
        [HttpPost]
        [Route("InsertarInventario")]

        public IActionResult InsertarInventario(Inventario inv)
        {
            var resultado = repo.InsertarInventario(inv);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return CreatedAtAction(nameof(BuscarInventario), new { idInventario = resultado.Object.IDInventario }, resultado.Object);
        }

        // PUT: api/Inventario/{id}
        [HttpPut]
        [Route("ActualizarInventario/{id}")]

        public IActionResult ActualizarInventario(Guid id, Inventario inv)
        {
            if (id != inv.IDInventario)
                return BadRequest("El ID de la Inventario no coincide con el parámetro.");

            var resultado = repo.ActualizarInventario(inv);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // DELETE: api/Inventario/{id}
        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(Guid id)
        {
            var eliminado = repo.EliminarInventario(id);

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