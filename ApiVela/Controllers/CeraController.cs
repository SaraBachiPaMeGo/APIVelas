using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVela.Models;
using ApiVela.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiVela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CeraController : ControllerBase
    {

        RepositoryCeras repo;

        public CeraController(RepositoryCeras repo)
        {
            this.repo = repo;
        }

        // GET: api/Cera
        [HttpGet]
        public ActionResult<List<Cera>> GetCera()
        {
            return repo.GetCeras();
        }

        // GET: api/Cera/5
        [HttpGet]

        [Route("[action]/{idCera}")]
        public ActionResult<Cera> BuscarCera(Guid idCera)
        {
            return repo.BuscarCera(idCera);
        }

        // POST: api/Cera
        [HttpPost]
        public void InsertarCera(Cera cera)
        {
            repo.InsertarCera(cera);
        }

        // PUT: api/Cera/5
        [HttpPut("{id}")]
        public void ActualizarCera(Cera cera)
        {
            repo.ActualizarCera(cera);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
