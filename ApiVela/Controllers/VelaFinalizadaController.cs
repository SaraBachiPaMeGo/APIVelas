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

        public class VelaFinalizadaController : ControllerBase
        {
            private readonly RepositoryVelaFin repo;

            public VelaFinalizadaController(RepositoryVelaFin repo)
            {
                this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
            }

            // GET: api/VelaFinalizada
            [HttpGet]
            [Route("GetVelaFinalizadas")]

            public IActionResult GetVelaFinalizadas()
            {
                var resultado = repo.GetVelaFin1();  // CustomApiResponse<List<VelaFinalizada>>
                if (resultado.Error != null)
                    return BadRequest(resultado.Error.Mensaje);

                return Ok(resultado.Object);
            }

            // GET: api/VelaFinalizada/BuscarVelaFinalizada/{idVelaFinalizada}
            [HttpGet]
            [Route("[action]/{idVelaFinalizada}")]
            public IActionResult BuscarVelaFinalizada(Guid idVelaFinalizada)
            {
                var resultado = repo.BuscarVelaFinalizada(idVelaFinalizada);
                if (resultado.Error != null)
                    return NotFound(resultado.Error.Mensaje);

                return Ok(resultado.Object);
            }

            // POST: api/VelaFinalizada
            [HttpPost]
            [Route("InsertarVelaFinalizada")]

            public IActionResult InsertarVelaFinalizada(VelaFinalizada mech)
            {
                var resultado = repo.InsertarVelaFinalizada(mech);
                if (resultado.Error != null)
                    return BadRequest(resultado.Error.Mensaje);

                return CreatedAtAction(nameof(BuscarVelaFinalizada), new { idVelaFinalizada = resultado.Object.IDVelaFin }, resultado.Object);
            }

            // PUT: api/VelaFinalizada/{id}
            [HttpPut]
            [Route("ActualizarVelaFinalizada/{id}")]

            public IActionResult ActualizarVelaFinalizada(Guid id, VelaFinalizada mech)
            {
                if (id != mech.IDVelaFin)
                    return BadRequest("El ID de la VelaFinalizada no coincide con el parámetro.");

                var resultado = repo.ActualizarVelaFinalizada(mech);
                if (resultado.Error != null)
                    return BadRequest(resultado.Error.Mensaje);

                return Ok(resultado.Object);
            }

            // DELETE: api/VelaFinalizada/{id}
            [HttpDelete("{id}")]
            public IActionResult DeleteVelaFinalizada(Guid id)
            {
                // Si tienes un método eliminar en el repositorio:
                // var resultado = repo.EliminarVelaFinalizada(id);
                // if (resultado.Error != null) return BadRequest(resultado.Error.Mensaje);
                // return NoContent();

                return StatusCode(501, "Método DELETE no implementado.");
            }
        }
    }
