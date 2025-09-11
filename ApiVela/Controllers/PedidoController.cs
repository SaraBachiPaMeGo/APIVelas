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
    public class PedidoController : ControllerBase
    {
        RepositoryPedidos repo;

        public PedidoController(RepositoryPedidos repo)
        {
            this.repo = repo;
        }

        // GET: api/Pedido
        [HttpGet]
        public ActionResult<List<Pedido>> GetPedido()
        {
            return repo.GetPedidos();
        }

        // GET: api/Pedido/5
        [HttpGet]

        [Route("[action]/{idPedido}")]
        public ActionResult<Pedido> BuscarPedido(Guid idPedido)
        {
            return repo.BuscarPedido(idPedido);
        }

        // POST: api/Pedido
        [HttpPost]
        public void InsertarPedido(Guid idCliente, Guid iDVela)
        {
            repo.InsertarPedido(idCliente, iDVela);
        }

        // PUT: api/Pedido/5
        [HttpPut("{id}")]
        public void ActualizarPedido(Guid idPedo, DateTime fechaEntrega, Guid idCliente,
            Guid iDVela)
        {
            repo.ActualizarPedido(idPedo, fechaEntrega, idCliente, iDVela);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
