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
    public class VelaPigmentoController : ControllerBase
    {
        private readonly RepositoryVelaPigmento repo;

        public VelaPigmentoController(RepositoryVelaPigmento repo)
        {
            this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        // GET: api/VelaPigmento/GetPigmentosPorVela/{idVela}
        [HttpGet]
        [Route("[action]/{idVela}")]
        public IActionResult GetVelaPigmentosPorVela(Guid idVela)
        {
            var resultado = repo.GetPigmentosPorVela(idVela); // CustomApiResponse<List<Pigmento>>
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // GET: api/BuscarVelaFragancia/{idVela}
        [HttpGet]
        [Route("[action]/{idVelaPigmento}")]
        public IActionResult BuscarVelaPigmentoPorVela(Guid idVela)
        {
            var resultado = repo.BuscarVelaPigmento(idVela);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // POST: api/VelaPigmento
        [HttpPost]
        [Route("api/InsertarVelaPigmento")]

        public IActionResult InsertarVelaPigmento(Guid idVela, Guid idPig)
        {
            var resultado = repo.InsertarVelaPigmento(idVela, idPig);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // PUT: api/VelaPigmento/{id}
        //[HttpPut("{id}")]
        //public IActionResult ActualizarVelaPigmento(VelaPigmento velaPigmento)
        //{
        //        var resultado = repo.ActualizarVelaPigmento(velaPigmento);
        //    if (resultado.Error != null)
        //        return BadRequest(resultado.Error.Mensaje);

        //    return Ok(resultado.Object);
        //}

    // DELETE: api/VelaPigmento/{id}
    [HttpDelete("[action]/{idvelaPigmento}")]
        public IActionResult EliminarVelaPigmento(Guid id)
        {
            var resultado = repo.EliminarRelacionesPigmentos(id);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }
    }
}
