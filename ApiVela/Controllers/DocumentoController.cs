using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVela.Models;
using ApiVela.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiVela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : Controller
    {
        private readonly RespositoryDocumentos repo;

        public DocumentoController(RespositoryDocumentos repo)
        {
            this.repo = repo;
        }
        // GET: api/Documento
        [HttpGet]
        [Route("GetDocumentos")]
        public IActionResult GetDocumentos()
        {
            var resultado = repo.GetDocumentos();  // supongo que devuelve CustomApiResponse<List>

            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // GET: api/Documento/BuscarDocumento/{IDDoc}
        [HttpGet]
        [Route("[action]/{IDDoc}")]
        public IActionResult BuscarDocumento(Guid IDDoc)
        {
            var resultado = repo.BuscarDocumento(IDDoc);

            if (resultado.Error != null)
                return NotFound(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // POST: api/Documento
        [HttpPost]
        [Route("InsertarDocumento")]

        public IActionResult InsertarDocumento(Documento Documento)
        {
            var resultado = repo.InsertarDocumento(Documento);

            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            // Si quieres, puedes devolver CreatedAtAction, si tienes ruta para BuscarDocumento
            return CreatedAtAction(nameof(BuscarDocumento), new { IDDoc = resultado.Object.IDDoc }, resultado.Object);
        }

        // PUT: api/Documento/{id}
        [HttpPut("ActualizarDocumento/{id}")]

        public IActionResult ActualizarDocumento(Guid id, Documento Documento)
        {
            if (id != Documento.IDDoc)
                return BadRequest("El ID de la Documento no coincide con el parámetro.");

            var resultado = repo.ActualizarDocumento(Documento);

            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // DELETE: api/Documento/{id}
        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(Guid id)
        {
            var eliminado = repo.EliminarDocumento(id);

            if (!eliminado.Object)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró el registro a eliminar"
                });
            }

            return Ok(new
            {
                mensaje = "Registro eliminado correctamente"
            });
        }
    }
}