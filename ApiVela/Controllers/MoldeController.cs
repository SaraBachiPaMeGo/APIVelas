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
    public class MoldeController : ControllerBase
    {
        RepositoryMoldes repo;

        public MoldeController(RepositoryMoldes repo)
        {
            this.repo = repo;
        }

        // GET: api/Molde
        [HttpGet]
        public ActionResult<List<Molde>> GetMolde()
        {
            return repo.GetMoldes();
        }

        // GET: api/Molde/5
        [HttpGet]

        [Route("[action]/{idMolde}")]
        public ActionResult<Molde> BuscarMolde(Guid idMolde)
        {
            return repo.BuscarMolde(idMolde);
        }

        // POST: api/Molde
        [HttpPost]
        public void InsertarMolde(Molde Molde)
        {
            repo.InsertarMolde(Molde);
        }

        // PUT: api/Molde/5
        [HttpPut("{id}")]
        public void ActualizarMolde(Molde Molde)
        {
            repo.ActualizarMolde(Molde);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
