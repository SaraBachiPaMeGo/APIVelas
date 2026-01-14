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

        public async Task<IActionResult> GetInventarios()
        {
            var resultado = await repo.GetInventarios();  // CustomApiResponse<List<Inventario>>
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // GET: api/Inventario/BuscarInventario/{idInventario}
        [HttpGet]
        [Route("[action]/{idInventario}")]
        public async Task<IActionResult> BuscarInventario(Guid idInventario)
        {
            var resultado = await repo.BuscarInventario(idInventario);
            if (resultado.Error != null)
                return NotFound(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // POST: api/Inventario
        [HttpPost]
        [Route("InsertarInventario")]

        public async Task<IActionResult> InsertarInventario(Inventario inv)
        {
            var resultado = await repo.InsertarInventario(inv);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return CreatedAtAction(nameof(BuscarInventario), new { idInventario = resultado.Object.IDInventario }, resultado.Object);
        }

        // PUT: api/Inventario/{id}
        [HttpPut]
        [Route("ActualizarInventario/{id}")]

        public async Task<IActionResult> ActualizarInventario(Guid id, Inventario inv)
        {
            if (id != inv.IDInventario)
                return BadRequest("El ID de la Inventario no coincide con el parámetro.");

            var resultado = await repo.ActualizarInventario(inv);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // DELETE: api/Inventario/{id}
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var eliminado = await repo.EliminarInventario(id);

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