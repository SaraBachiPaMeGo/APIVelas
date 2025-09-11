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
    public class FraganciaController : ControllerBase
    {

        RepositoryFragancias repo;

        public FraganciaController(RepositoryFragancias repo)
        {
            this.repo = repo;
        }

        // GET: api/Fragancia
        [HttpGet]

        public ActionResult<List<Fragancia>> GetFragancias()
        {
            return repo.GetFragancias();
        }

        // GET: api/Fragancia/5
        [HttpGet]
        
        [Route("[action]/{idFrag}")]
        public ActionResult<Fragancia> BuscarFragancia(Guid idFrag)
        {
            return repo.BuscarFragancia(idFrag);
        }

        // POST: api/Fragancia
        [HttpPost]
        public void InsertarFragancia(Fragancia frag)
        {
            repo.InsertarFragancia(frag);
        }

        // PUT: api/Fragancia/5
        [HttpPut("{id}")]
        public void ActualizarFragancia(Fragancia frag)
        {
            repo.ActualizarFragancia(frag);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
