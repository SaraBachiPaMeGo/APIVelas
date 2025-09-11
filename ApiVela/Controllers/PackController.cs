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
    public class PackController : ControllerBase
    {
        RepositoryPacks repo;

        public PackController(RepositoryPacks repo)
        {
            this.repo = repo;
        }

        // GET: api/Pack
        [HttpGet]
        public ActionResult<List<Pack>> GetPack()
        {
            return repo.GetPacks();
        }

        // GET: api/Pack/5
        [HttpGet]

        [Route("[action]/{idPack}")]
        public ActionResult<Pack> BuscarPack(Guid idPack)
        {
            return repo.BuscarPack(idPack);
        }

        // POST: api/Pack
        [HttpPost]
        public void InsertarPack(Pack Pack)
        {
            repo.InsertarPack(Pack);
        }

        // PUT: api/Pack/5
        [HttpPut("{id}")]
        public void ActualizarPack(Pack Pack)
        {
            repo.ActualizarPack(Pack);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
