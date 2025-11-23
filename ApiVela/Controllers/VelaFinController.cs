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
    public class VelaFinController : ControllerBase
    {
        private readonly RepositoryVelaFin repo;

        public VelaFinController(RepositoryVelaFin repo)
        {
            this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        // GET: api/Vela
        [HttpGet]
        [Route("GetVelas")]

        public IActionResult GetVelas()
        {
            var resultado = repo.GetVelaFin(); // CustomApiResponse<List<VelaFin>>
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // GET: api/velaFin/BuscarvelaFin/{idvelaFin}
        [HttpGet]
        [Route("[action]/{idvelaFin}")]
        public IActionResult BuscarvelaFin(Guid idvelaFin)
        {
            var resultado = repo.BuscarVelaFinalizada(idvelaFin);
            if (resultado.Error != null)
                return NotFound(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // POST: api/velaFin
        [HttpPost]
        [Route("InsertarvelaFin")]

        public IActionResult InsertarvelaFin(VelaFinalizada velaFin)
        {
            var resultado = repo.InsertarVelaFinalizada(velaFin);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return CreatedAtAction(nameof(BuscarvelaFin), new { idvelaFin = resultado.Object.IDVelaFin }, resultado.Object);
        }

        // PUT: api/velaFin/{id}
        [HttpPut]
        [Route("ActualizarvelaFin/{id}")]

        public IActionResult ActualizarvelaFin(Guid id, VelaFinalizada velaFin)
        {
            if (id != velaFin.IDVelaFin)
                return BadRequest("El ID de la velaFin no coincide con el parámetro.");

            var resultado = repo.ActualizarVelaFinalizada(velaFin);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // DELETE: api/velaFin/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            // Puedes implementar la lógica de eliminar aquí si la tienes en el repositorio
            return StatusCode(501, "Eliminación no implementada");
        }
    }
    }