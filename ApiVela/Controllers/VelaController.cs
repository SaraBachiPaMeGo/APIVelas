using System;
using Microsoft.AspNetCore.Mvc;
using ApiVela.Models;
using ApiVela.Repository;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using ApiVela.Models.DTO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ApiVela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VelaController : ControllerBase
    {
        private readonly RepositoryVelas repo;

        public VelaController(RepositoryVelas repo)
        {
            this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        // GET: api/Vela
        [HttpGet]
        [Route("GetVelas")]

        public async Task<IActionResult> GetVelas() // CustomApiResponse<List<VelaDTO>>
        {
            var resultado = await repo.GetVelas(); // CustomApiResponse<List<VelaDTO>>
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // GET: api/Vela/BuscarVela/{idVela}
        [HttpGet]
        [Route("[action]/{idVela}")]
        public async Task<IActionResult> BuscarVela(Guid idVela)
        {
            var resultado = await repo.BuscarVela(idVela);
            if (resultado.Error != null)
                return NotFound(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        } // CustomApiResponse<VelaDTO>

        // POST: api/Vela
        [HttpPost]
        [Route("InsertarVela")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> InsertarVela(
            [FromForm]Vela vela, 
            [FromForm] string vf,
            [FromForm] string vp,
            [FromForm] IFormFile file) // CustomApiResponse<Vela>
        {
            var resultado = new CustomApiResponse<VelaDTO>();

            if (file != null && file.Length != 0)
            {
                // Validación básica de extensión
                var ext = Path.GetExtension(file.FileName).ToLower();

                var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif" };

                if (!allowed.Contains(ext))
                {
                    resultado.Error.Mensaje = "Tipo de archivo no permitido.";
                    return BadRequest();
                }

                // 🔹 Convertir a bytes (FORMA EFICIENTE)

                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);

                vela.Image = ms.ToArray();
                vela.ImagenContentType = file.ContentType;
            }

            if (!string.IsNullOrEmpty(vf))
            {
                vela.VelaFragancias = JsonConvert.DeserializeObject<List<VelaFragancia>>(vf);
            }
            else
            {
                vela.VelaFragancias = new List<VelaFragancia>();
            }

            if (!string.IsNullOrEmpty(vp))
            {
                vela.VelaPigmentos = JsonConvert.DeserializeObject<List<VelaPigmento>>(vp);
            }
            else
            {
                vela.VelaPigmentos = new List<VelaPigmento>();
            }

            resultado = await repo.InsertarVela(vela);

            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // PUT: api/Vela/{id}
        [HttpPut]
        [Route("ActualizarVela/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ActualizarVela(Guid id, [FromForm]Vela vela, IFormFile file) // CustomApiResponse<VelaDTO>
        {
            var resultado = new CustomApiResponse<Molde>();

            if (file != null && file.Length != 0)
            {
                // Validación básica de extensión
                var ext = Path.GetExtension(file.FileName).ToLower();

                var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif" };

                if (!allowed.Contains(ext))
                {
                    resultado.Error.Mensaje = "Tipo de archivo no permitido.";
                    return BadRequest();
                }

                // 🔹 Convertir a bytes (FORMA EFICIENTE)

                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);

                vela.Image = ms.ToArray();
                vela.ImagenContentType = file.ContentType;
            }

            if (id != vela.IDVela)
                return BadRequest("El ID de la vela no coincide con el parámetro.");

            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // DELETE: api/Vela/{id}
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(Guid id) // CustomApiResponse<bool>
        {
            var eliminado = await repo.EliminarVela(id);

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
