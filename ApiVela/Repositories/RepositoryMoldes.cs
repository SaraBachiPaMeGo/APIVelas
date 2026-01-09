using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVela.Data;
using ApiVela.Models;
using AutoMapper;

namespace ApiVela.Repository
{
    public class RepositoryMoldes
    {
        private readonly Contexto context;
        private readonly IMapper mapper;

        public RepositoryMoldes(Contexto context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // ---------------------------- INSERTAR ----------------------------
        public CustomApiResponse<Molde> InsertarMolde(Molde mol) 
        {
            var response = new CustomApiResponse<Molde>();

            try
            {
                var molde = new Molde
                {
                    IDMolde = Guid.NewGuid(),  // Tomo que siempre generas un nuevo GUID
                    MoldeNombre = mol.MoldeNombre,
                    CMMecha = mol.CMMecha,
                    GramCera = mol.GramCera,
                    Medidas = mol.Medidas,
                    Duracion = mol.Duracion,
                    Observ = mol.Observ,
                    CompradoEn = mol.CompradoEn,
                    Firma = mol.Firma,
                    IDVela = mol.IDVela,
                    MilAgua = mol.MilAgua,
                    Tipo = mol.Tipo,
                    Coste = mol.Coste
                };

                context.Molde.Add(molde);
                context.SaveChanges();

                response.Object = mapper.Map<Molde>(molde);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // ---------------------------- ACTUALIZAR ----------------------------
        public CustomApiResponse<Molde> ActualizarMolde(Molde mol) 
        {
            var response = new CustomApiResponse<Molde>();

            try
            {
                var existing = context.Molde.SingleOrDefault(x => x.IDMolde == mol.IDMolde);
                if (existing == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Molde no encontrado" };
                    return response;
                }

                // Actualizar sólo si es diferente / no nulo
                existing.MoldeNombre = mol.MoldeNombre ?? existing.MoldeNombre;
                existing.Firma = mol.Firma ?? existing.Firma;
                existing.Tipo = mol.Tipo ?? existing.Tipo;
                existing.CompradoEn = mol.CompradoEn ?? existing.CompradoEn;
                existing.IDVela = mol.IDVela != Guid.Empty ? mol.IDVela : existing.IDVela;
                existing.CMMecha = mol.CMMecha != default ? mol.CMMecha : existing.CMMecha;
                existing.GramCera = mol.GramCera != default ? mol.GramCera : existing.GramCera;
                existing.Medidas = mol.Medidas ?? existing.Medidas;
                existing.Duracion = mol.Duracion != default ? mol.Duracion : existing.Duracion;
                existing.Observ = mol.Observ ?? existing.Observ;
                existing.MilAgua = mol.MilAgua != default ? mol.MilAgua : existing.MilAgua;
                existing.Coste = mol.Coste;

                context.SaveChanges();

                response.Object = mapper.Map<Molde>(existing);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // ---------------------------- OBTENER TODOS ----------------------------
        public CustomApiResponse<List<Molde>> GetMoldes() 
        {
            var response = new CustomApiResponse<List<Molde>>();

            try
            {
                var list = context.Molde.ToList();
                response.Object = mapper.Map<List<Molde>>(list);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        // ---------------------------- BUSCAR POR ID ----------------------------
        public CustomApiResponse<Molde> BuscarMolde(Guid idMolde) 
        {
            var response = new CustomApiResponse<Molde>();

            try
            {
                var molde = context.Molde.SingleOrDefault(x => x.IDMolde == idMolde);
                if (molde == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Molde no encontrado" };
                    return response;
                }

                response.Object = mapper.Map<Molde>(molde);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }

        public CustomApiResponse<bool> EliminarMoldeAsync(Guid idMolde)
        {
            var response = new CustomApiResponse<bool>();

            try
            {
                var molde = context.Molde.SingleOrDefault(x => x.IDMolde == idMolde);

                if (molde == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Molde no encontrado" };
                    response.Object = false;
                }
                else
                {
                    context.Set<Molde>().Remove(molde);
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
