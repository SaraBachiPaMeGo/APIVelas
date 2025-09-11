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
    public class MechaController : ControllerBase
    {
        RepositoryMechas repo;

        public MechaController(RepositoryMechas repo)
        {
            this.repo = repo;
        }

        // GET: api/Mecha
        [HttpGet]
        public ActionResult<List<Mecha>> GetMecha()
        {
            return repo.GetMechas();
        }

        // GET: api/Mecha/5
        [HttpGet]

        [Route("[action]/{idMecha}")]
        public ActionResult<Mecha> BuscarMecha(Guid idMecha)
        {
            return repo.BuscarMecha(idMecha);
        }

        // POST: api/Mecha
        [HttpPost]
        public void InsertarMecha(Mecha Mecha)
        {
            repo.InsertarMecha(Mecha);
        }

        // PUT: api/Mecha/5
        [HttpPut("{id}")]
        public void ActualizarMecha(Mecha Mecha)
        {
            repo.ActualizarMecha(Mecha);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
