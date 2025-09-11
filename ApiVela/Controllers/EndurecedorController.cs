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
    public class EndurecedorController : ControllerBase
    {

        RepositoryEndurecedores repo;

        public EndurecedorController(RepositoryEndurecedores repo)
        {
            this.repo = repo;
        }
        // GET: api/Endurecedor
        [HttpGet]
        public ActionResult<List<Endurecedor>> GetEndurecedor()
        {
            return repo.GetEndurecedor();
        }

        // GET: api/Endurecedor/5
        [HttpGet]

        [Route("[action]/{idend}")]
        public ActionResult<Endurecedor> BuscarEndurecedor(Guid idend)
        {
            return repo.BuscarEndurecedor(idend);
        }

        // POST: api/Endurecedor
        [HttpPost]
        public void InsertarEndurecedor(Endurecedor end)
        {
            repo.InsertarEndurecedor(end);
        }

        // PUT: api/Endurecedor/5
        [HttpPut("{id}")]
        public void ActualizarEndurecedor(Endurecedor end)
        {
            repo.ActualizarEndurecedor(end);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
