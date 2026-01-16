using System;
using Microsoft.AspNetCore.Mvc;
using ApiVela.Models;
using ApiVela.Repository;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoldeController : ControllerBase
    {
        private readonly RepositoryMoldes repo;
        private readonly IWebHostEnvironment _env;

        public MoldeController(RepositoryMoldes repo, IWebHostEnvironment env)
        {
            this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _env = env;
        }

        // GET: api/Molde
        [HttpGet]
        [Route("GetMoldes")]

        public async Task<IActionResult> GetMoldes()
        {
            var resultado = await repo.GetMoldes();  // CustomApiResponse<List<Molde>>
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // GET: api/Molde/BuscarMolde/{idMolde}
        [HttpGet]
        [Route("[action]/{idMolde}")]
        public async Task<IActionResult> BuscarMolde(Guid idMolde)
        {
            var resultado = await repo.BuscarMolde(idMolde);
            if (resultado.Error != null)
                return NotFound(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // POST: api/Molde
        [HttpPost]
        [Route("InsertarMolde")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> InsertarMolde([FromForm]Molde molde, IFormFile file)
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

                molde.Image = ms.ToArray();
                molde.ImagenContentType = file.ContentType;
            }            

            resultado = await repo.InsertarMolde(molde);


            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return CreatedAtAction(nameof(BuscarMolde), new { idMolde = resultado.Object.IDMolde }, resultado.Object);
        }

        // PUT: api/Molde/{id}
        [HttpPut("ActualizarMolde/{id}")]

        public async Task<IActionResult> ActualizarMolde(Guid id, Molde molde)
        {
            if (id != molde.IDMolde)
                return BadRequest("El ID del molde no coincide con el parámetro.");

            var resultado = await repo.ActualizarMolde(molde);
            if (resultado.Error != null)
                return BadRequest(resultado.Error.Mensaje);

            return Ok(resultado.Object);
        }

        // DELETE: api/Molde/{id}
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var eliminado = await repo.EliminarMolde(id);

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

        //[HttpPost("upload")]
        //public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] string nombre, [FromForm] string? descripcion)
        //{
        //    if (file == null || file.Length == 0)
        //        return BadRequest("No se ha enviado ninguna imagen.");

        //    // Validación básica de extensión
        //    var ext = Path.GetExtension(file.FileName).ToLower();
        //    var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        //    if (!allowed.Contains(ext))
        //        return BadRequest("Tipo de archivo no permitido.");

        //    // Carpeta donde se guardan las imágenes
        //    var uploadsFolder = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "uploads");
        //    if (!Directory.Exists(uploadsFolder))
        //        Directory.CreateDirectory(uploadsFolder);

        //    // Nombre único para el archivo
        //    var fileName = $"{Guid.NewGuid()}{ext}";
        //    var filePath = Path.Combine(uploadsFolder, fileName);

        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    var relativePath = $"/uploads/{fileName}";
        //}
    }
}
