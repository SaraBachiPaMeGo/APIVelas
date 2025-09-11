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
    public class ClienteController : ControllerBase
    {

        RepositoryClientes repo;

        public ClienteController(RepositoryClientes repo)
        {
            this.repo = repo;
        }

        // GET: api/Cliente
        [HttpGet]
        public ActionResult<List<Cliente>> GetClientes()
        {
            return repo.GetClientes();
        }

        // GET: api/Cliente/5
        [HttpGet]
        
        [Route("[action]/{idCliente}")]
        public ActionResult<Cliente> BuscarCliente(Guid idCliente)
        {
            return repo.BuscarCliente(idCliente);
        }

        // POST: api/Cliente
        [HttpPost]
        public void InsertarCliente(Cliente cli)
        {
            repo.InsertarCliente(cli);
        }


        // PUT: api/Cliente/5
        [HttpPut("{id}")]
        public void ActualizarCliente(Cliente cli)
        {
            repo.ActualizarCliente(cli);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
