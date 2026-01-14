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
    public class VelaFraganciaController : ControllerBase
    {
        private readonly RepositoryVelaFragancia repo;

        public VelaFraganciaController(RepositoryVelaFragancia repo)
        {
            this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        //// GET: api/VelaFragancia/GetFraganciasPorVela/{idVela}
        //[HttpGet]
        //[Route("[action]/{idVela}")]
        //public async Task<IActionResult> GetVelaFraganciasPorVela(Guid idVela)
        //{
        //    var resultado = await repo.GetFraganciasPorVela(idVela); // CustomApiResponse<List<Fragancia>>
        //    if (resultado.Error != null)
        //        return BadRequest(resultado.Error.Mensaje);

        //    return Ok(resultado.Object);
        //}

        //// GET: api/BuscarVelaFragancia/GetFraganciasPorVela/{idVela}
        //[HttpGet]
        //[Route("[action]/{idVela}")]
        //public async Task<IActionResult> BuscarVelaFraganciasPorVela(Guid idVela)
        //{
        //    var resultado = await repo.BuscarVelaFragancia(idVela); // CustomApiResponse<List<Fragancia>>
        //    if (resultado.Error != null)
        //        return BadRequest(resultado.Error.Mensaje);

        //    return Ok(resultado.Object);
        //}

        // POST: api/VelaFragancia
        //[HttpPost]
        //[Route("InsertarVelaFragancia")]

        //public async Task<IActionResult> InsertarVelaFragancia(Guid idVela, Guid idFrag)
        //{
        //    var resultado = await repo.InsertarVelaFragancia(idVela, idFrag);
        //    if (resultado.Error != null)
        //        return BadRequest(resultado.Error.Mensaje);

        //    return Ok(resultado.Object);
        //}

        //PUT: api/VelaFragancia/{idVelaFragancia}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> ActualizarVelaFragancia(VelaFragancia velaFragancia)
        //{
        //    // Si tienes método para actualizar en repo, usarlo aquí, ejemplo:
        //    var resultado = await repo.ac(velaFragancia);
        //    if (resultado.Error != null)
        //        return BadRequest(resultado.Error.Mensaje);

        //    return Ok(resultado.Object);
        //}

        // DELETE: api/VelaFragancia/{id}
        //[HttpDelete("[action]/{idvelaFragancia}")]
        //public async Task<IActionResult> EliminarVelaFragancia(Guid id)
        //{
        //    var resultado = await repo.EliminarRelacionesFragancias(id);
        //    if (resultado.Error != null)
        //        return BadRequest(resultado.Error.Mensaje);

        //    return Ok(resultado.Object);
        //}
    }
}
