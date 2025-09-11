using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiVela.Models;
using ApiVela.Repository;

namespace ApiVela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PigmentoController : ControllerBase
    {
        RepositoryPigmentos repo;

        public PigmentoController(RepositoryPigmentos repo)
        {
            this.repo = repo;
        }

        // GET: api/Pigmento
        [HttpGet]
        public ActionResult<List<Pigmento>> GetPigmento()
        {
            return repo.GetPigmentos();
        }

        // GET: api/Pigmento/5
        [HttpGet]

        [Route("[action]/{idPigmento}")]
        public ActionResult<Pigmento> BuscarPigmento(Guid idPigmento)
        {
            return repo.BuscarPigmento(idPigmento);
        }

        // POST: api/Pigmento
        [HttpPost]
        public void InsertarPigmento(Pigmento Pigmento)
        {
            repo.InsertarPigmento(Pigmento);
        }

        // PUT: api/Pigmento/5
        [HttpPut("{id}")]
        public void ActualizarPigmento(Pigmento Pigmento)
        {
            repo.ActualizarPigmento(Pigmento);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
