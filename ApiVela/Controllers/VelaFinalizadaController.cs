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

            public async Task<IActionResult> GetVelaFinalizadas()
            {
                var resultado = await repo.GetVelaFin1();  // CustomApiResponse<List<VelaFinalizada>>
                if (resultado.Error != null)
                    return BadRequest(resultado.Error.Mensaje);

                return Ok(resultado.Object);
            }

            // GET: api/VelaFinalizada/BuscarVelaFinalizada/{idVelaFinalizada}
            [HttpGet]
            [Route("[action]/{idVelaFinalizada}")]
            public async Task<IActionResult> BuscarVelaFinalizada(Guid idVelaFinalizada)
            {
                var resultado = await repo.BuscarVelaFinalizada(idVelaFinalizada);
                if (resultado.Error != null)
                    return NotFound(resultado.Error.Mensaje);

                return Ok(resultado.Object);
            }

            // POST: api/VelaFinalizada
            [HttpPost]
            [Route("InsertarVelaFinalizada")]

            public async Task<IActionResult> InsertarVelaFinalizada(VelaFinalizada mech)
            {
                var resultado = await repo.InsertarVelaFinalizada(mech);
                if (resultado.Error != null)
                    return BadRequest(resultado.Error.Mensaje);

                return CreatedAtAction(nameof(BuscarVelaFinalizada), new { idVelaFinalizada = resultado.Object.IDVelaFin }, resultado.Object);
            }

            // PUT: api/VelaFinalizada/{id}
            [HttpPut]
            [Route("ActualizarVelaFinalizada/{id}")]

            public async Task<IActionResult> ActualizarVelaFinalizada(Guid id, VelaFinalizada mech)
            {
                if (id != mech.IDVelaFin)
                    return BadRequest("El ID de la VelaFinalizada no coincide con el parámetro.");

                var resultado = await repo.ActualizarVelaFinalizada(mech);
                if (resultado.Error != null)
                    return BadRequest(resultado.Error.Mensaje);

                return Ok(resultado.Object);
            }

        // DELETE: api/VelaFinalizada/{id}
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var eliminado = await repo.EliminarVelaFinalizada(id);

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
