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
    public class VelaController : ControllerBase
    {
        RepositoryVelas repo;

        public VelaController(RepositoryVelas repo)
        {
            this.repo = repo;
        }

        // GET: api/Vela
        [HttpGet]
        public ActionResult<List<Vela>> GetVela()
        {
            return repo.GetVelas();
        }

        // GET: api/Vela/5
        [HttpGet]
        [Route("[action]/{idVela}")]
        public ActionResult<Vela> BuscarVela(Guid idVela)
        {
            return repo.BuscarVela(idVela);
        }

        // POST: api/Vela
        [HttpPost]
        public void InsertarVela(Vela Vela)
        {
            repo.InsertarVela(Vela);
        }

        // PUT: api/Vela/5
        [HttpPut("{id}")]
        public void ActualizarVela(Vela Vela)
        {
            repo.Actualizarvela(Vela);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
