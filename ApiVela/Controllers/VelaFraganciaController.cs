using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        RepositoryVelaFragancia repo;

        public VelaFraganciaController(RepositoryVelaFragancia repo)
        {
            this.repo = repo;
        }

        // GET: api/VelaFragancia
        [HttpGet]
        [Route("[action]/{idVela}")]

        public ActionResult<List<Fragancia>> GetFraganciasPorVela(Guid idVela)
        {
            return repo.GetFraganciasPorVela(idVela);
        }

        // GET: api/VelaFragancia/5
        //[HttpGet]

        //[Route("[action]/{idVelaFragancia}")]
        //public ActionResult<VelaFragancia> BuscarVelaFragancia(Guid idVelaFragancia)
        //{
           // return repo.BuscarVelaFragancia(idVelaFragancia);
        //}

        // POST: api/VelaFragancia
        [HttpPost]
        public void InsertarVelaFragancia(Guid idVela, Guid idFrag)
        {
            repo.InsertarVelaFragancia(idVela, idFrag);
        }

        // PUT: api/VelaFragancia/5
        [HttpPut("{id}")]
        public void ActualizarVelaFragancia(VelaFragancia VelaFragancia)
        {
            //repo.ActualizarVelaFragancia(VelaFragancia);
        }
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
