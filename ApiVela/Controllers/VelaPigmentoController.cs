using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVela.Models;
using ApiVela.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiVela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VelaPigmentoController : ControllerBase
    {

        RepositoryVelaPigmento repo;

        public VelaPigmentoController(RepositoryVelaPigmento repo)
        {
            this.repo = repo;
        }

        // GET: api/VelaPigmento
        [HttpGet]
        [Route("[action]/{idVelaPigmento}")]

        public ActionResult<List<Pigmento>> GetPigmentosPorVela(Guid idVela)
        {
            return repo.GetPigmentosPorVela(idVela);
        }

        // GET: api/VelaPigmento/5
        //[HttpGet]

        //[Route("[action]/{idVelaPigmento}")]
        //public ActionResult<VelaPigmento> BuscarVelaPigmento(Guid idVelaPigmento)
        //{
        //    return repo.BuscarVelaPigmento(idVelaPigmento);
        //}

        // POST: api/VelaPigmento
        [HttpPost]
        public void InsertarVelaPigmento(Guid idVela, Guid idPig)
        {
            repo.InsertarVelaPigmento(idVela, idPig);
        }

        // PUT: api/VelaPigmento/5
        [HttpPut("{id}")]
        public void ActualizarVelaPigmento(VelaPigmento VelaPigmento)
        {
            //repo.ActualizarVelaPigmento(VelaPigmento);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
