using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ApiVela.Models;
using ApiVela.Repository;

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
        public IActionResult GetFragancias()
        {
            var resultado = repo.GetFragancias();
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // GET: api/Fragancia/BuscarFragancia/{idFrag}
        [HttpGet]
        [Route("[action]/{idFrag}")]
        public IActionResult BuscarFragancia(Guid idFrag)
        {
            var resultado = repo.BuscarFragancia(idFrag);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // POST: api/Fragancia
        [HttpPost]
        public IActionResult InsertarFragancia(Fragancia frag)
        {
            var resultado = repo.InsertarFragancia(frag);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // PUT: api/Fragancia/{id}
        [HttpPut("{id}")]
        public IActionResult ActualizarFragancia( Fragancia frag)
        {
            var resultado = repo.ActualizarFragancia(frag);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // DELETE: api/Fragancia/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Implementar eliminación si se requiere
            return StatusCode(501, "Eliminación no implementada");
        }
    }
}