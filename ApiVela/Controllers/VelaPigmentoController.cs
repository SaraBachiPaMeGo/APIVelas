using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ApiVela.Models;
using ApiVela.Repository;
using ApiVela.Repositories;
using System.Threading.Tasks;


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
        public async Task<IActionResult> GetVelaPigmentosPorVela(Guid idVela)
        {
            var resultado = await repo.GetPigmentosPorVela(idVela); // CustomApiResponse<List<Pigmento>>
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // GET: api/BuscarVelaFragancia/{idVela}
        [HttpGet]
        [Route("[action]/{idVela}")]
        public async Task<IActionResult> BuscarVelaPigmentoPorVela(Guid idVela)
        {
            var resultado = await repo.BuscarVelaPigmento(idVela);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // POST: api/VelaPigmento
        [HttpPost]
        [Route("InsertarVelaPigmento")]

        public async Task<IActionResult> InsertarVelaPigmento(Guid idVela, Guid idPig)
        {
            var resultado = await repo.InsertarVelaPigmento(idVela, idPig);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

         //PUT: api/VelaPigmento/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarVelaPigmento(VelaPigmento velaPigmento)
        {
                var resultado = await repo.ActualizarVelaPigmento(velaPigmento);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

         //DELETE: api/VelaPigmento/{id}
        [HttpDelete("[action]/{idvelaPigmento}")]
            public async Task<IActionResult> EliminarVelaPigmento(Guid id)
            {
                var resultado = await repo.EliminarRelacionesPigmentos(id);
                if (resultado.Error != null)
                    return BadRequest(resultado.Error.Mensaje);

                return Ok(resultado.Object);
            }
        }
    }

