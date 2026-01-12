using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVela.Data;
using ApiVela.Models;
using AutoMapper;

namespace ApiVela.Repositories
{
    public class RepositoryInventarios
    {
        private readonly Contexto context;
        private readonly IMapper mapper;

        public RepositoryInventarios(Contexto context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public CustomApiResponse<List<Inventario>> GetInventarios()
        {
            var response = new CustomApiResponse<List<Inventario>>();
            try
            {
                var Inventarios = context.Inventario.ToList();
                response.Object = mapper.Map<List<Inventario>>(Inventarios);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public CustomApiResponse<Inventario> BuscarInventario(Guid idInventario)
        {
            var response = new CustomApiResponse<Inventario>();
            try
            {
                var Inventario = context.Inventario.SingleOrDefault(m => m.IDInventario == idInventario);
                if (Inventario == null) throw new Exception("Inventario no encontrada");
                response.Object = mapper.Map<Inventario>(Inventario);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public CustomApiResponse<Inventario> InsertarInventario(Inventario inv)
        {
            var response = new CustomApiResponse<Inventario>();
            try
            {
                // Puedes agregar validaciones aquí si es necesario, por ejemplo permitir solo una Inventario:
                // if (context.Inventario.Any()) throw new Exception("Solo se permite una Inventario");

                inv.IDInventario = inv.IDInventario != Guid.Empty ? inv.IDInventario : inv.IDInventario;
                var Inventario = mapper.Map<Inventario>(inv);
                Inventario.IDInventario = Guid.NewGuid();

                context.Inventario.Add(Inventario);
                context.SaveChanges();

                response.Object = mapper.Map<Inventario>(Inventario);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public CustomApiResponse<Inventario> ActualizarInventario(Inventario inv)
        {
            var response = new CustomApiResponse<Inventario>();
            try
            {
                var InventarioExistente = context.Inventario.SingleOrDefault(m => m.IDInventario == inv.IDInventario);
                if (InventarioExistente == null) throw new Exception("Inventario no encontrado");

                // Actualiza solo los campos que hayan cambiado
                if (inv.Firma != InventarioExistente.Firma) InventarioExistente.Firma = inv.Firma;
                if (inv.Tipo != InventarioExistente.Tipo) InventarioExistente.Tipo = inv.Tipo;
                if (inv.CompradoEn != InventarioExistente.CompradoEn) InventarioExistente.CompradoEn = inv.CompradoEn;
                if (inv.IDInventario != InventarioExistente.IDInventario) InventarioExistente.IDInventario = inv.IDInventario;
                if (inv.Cantidad != InventarioExistente.Cantidad) InventarioExistente.Cantidad = inv.Cantidad;
                if (inv.Coste != InventarioExistente.Coste) InventarioExistente.Coste = inv.Coste;

                context.SaveChanges();

                response.Object = mapper.Map<Inventario>(InventarioExistente);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public CustomApiResponse<bool> EliminarInventario(Guid idInventario)
        {
            var response = new CustomApiResponse<bool>();

            try
            {
                var Inventario = context.Inventario.SingleOrDefault(x => x.IDInventario == idInventario);

                if (Inventario == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Inventario no encontrado" };
                    response.Object = false;
                }
                else
                {
                    context.Set<Inventario>().Remove(Inventario);
                    context.SaveChangesAsync();
                    response.Object = true;

                }
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }
    }
}
