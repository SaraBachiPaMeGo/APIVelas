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
        public IActionResult GetVelaFraganciasPorVela(Guid idVela)
        {
            var resultado = repo.GetFraganciasPorVela(idVela); // CustomApiResponse<List<Fragancia>>
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // GET: api/BuscarVelaFragancia/GetFraganciasPorVela/{idVela}
        [HttpGet]
        [Route("[action]/{idVela}")]
        public IActionResult BuscarVelaFraganciasPorVela(Guid idVela)
        {
            var resultado = repo.BuscarVelaFragancia(idVela); // CustomApiResponse<List<Fragancia>>
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // POST: api/VelaFragancia
        [HttpPost]
        [Route("InsertarVelaFragancia")]

        public IActionResult InsertarVelaFragancia(Guid idVela, Guid idFrag)
        {
            var resultado = repo.InsertarVelaFragancia(idVela, idFrag);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        //PUT: api/VelaFragancia/{idVelaFragancia}
        //[HttpPut("{id}")]
        //public IActionResult ActualizarVelaFragancia(VelaFragancia velaFragancia)
        //{
        //    // Si tienes método para actualizar en repo, usarlo aquí, ejemplo:
        //    var resultado = repo.ac(velaFragancia);
        //    if (resultado.Error != null)
        //        return BadRequest(resultado.Error.Mensaje);

        //    return Ok(resultado.Object);
        //}

        // DELETE: api/VelaFragancia/{id}
        [HttpDelete("[action]/{idvelaFragancia}")]
        public IActionResult EliminarVelaFragancia(Guid id)
        {
            var resultado = repo.EliminarRelacionesFragancias(id);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }
    }
}
