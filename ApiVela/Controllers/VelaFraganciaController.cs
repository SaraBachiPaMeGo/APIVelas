using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ApiVela.Models;
using ApiVela.Repository;
using ApiVela.Repositories;

namespace ApiVela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VelaFraganciaController : ControllerBase
    {
        private readonly RepositoryVelaFragancia repo;

        public VelaFraganciaController(RepositoryVelaFragancia repo)
        {
            this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        // GET: api/VelaFragancia/GetFraganciasPorVela/{idVela}
        [HttpGet]
        [Route("[action]/{idVela}")]
        public IActionResult GetFraganciasPorVela(Guid idVela)
        {
            var resultado = repo.GetFraganciasPorVela(idVela); // CustomApiResponse<List<Fragancia>>
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // POST: api/VelaFragancia
        [HttpPost]
        [Route("api/InsertarVelaFragancia")]

        public IActionResult InsertarVelaFragancia(Guid idVela, Guid idFrag)
        {
            var resultado = repo.InsertarVelaFragancia(idVela, idFrag);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // PUT: api/VelaFragancia/{id}
        //[HttpPut("{id}")]
        //public IActionResult ActualizarVelaFragancia( VelaFragancia velaFragancia)
        //{
        //    // Si tienes método para actualizar en repo, usarlo aquí, ejemplo:
        //    var resultado = repo.(velaFragancia);
        //    if (resultado.Error != null)
        //        return BadRequest(resultado.Error.Mensaje);

        //    return Ok(resultado.Object);
        //}

        // DELETE: api/VelaFragancia/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Aquí implementa la lógica si tienes para eliminar.
            return StatusCode(501, "Eliminación no implementada");
        }
    }
}
