using System;
using System.Collections.Generic;
using System.Linq;
using ApiVela.Data;
using ApiVela.Models;
using ApiVela.Models.DTO;
using AutoMapper;

namespace ApiVela.Repository
{
    public class RepositoryVelas
    {
        private readonly Contexto context;
        private readonly IMapper mapper;

        public RepositoryVelas(Contexto context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public CustomApiResponse<List<VelaDTO>> GetVelas1()
        {
            var response = new CustomApiResponse<List<VelaDTO>>();
            try
            {
                var velas = context.Vela.ToList();
                response.Object = mapper.Map<List<VelaDTO>>(velas);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public CustomApiResponse<List<VelaDTO>> GetVelas()
        {
            var response = new CustomApiResponse<List<VelaDTO>>();

            try
            {
                var velas = context.Vela
                    .Select(v => new VelaDTO
                    {
                        IDVela = v.IDVela,
                        VelaNombre = v.VelaNombre,
                        Image = v.Image,
                        FechaReal = v.FechaReal,
                        Coste = v.Coste,

                        CantidadCera = v.CantidadCera,
                        CantidadMecha = v.CantidadMecha,
                        CantidadEnd = v.CantidadEnd,

                        Pigmentos = v.VelaPigmentos
                            .Select(vp => new VelaPigmentoDTO
                            {
                                IDPig = vp.IDPig,
                                NombrePigmento = vp.Pigmento.ColorNombre, // 👈 NOMBRE
                                Cantidad = vp.Cantidad,
                                Coste = vp.Coste
                            }).ToList(),

                        Fragancias = v.VelaFragancias
                            .Select(vf => new VelaFraganciaDTO
                            {
                                IDFrag = vf.IDFrag,
                                NombreFragancia = vf.Fragancia.FragNombre, // 👈 NOMBRE
                        Cantidad = vf.Cantidad,
                                Coste = vf.Coste
                            }).ToList()
                    })
                    .ToList();

                response.Object = velas;
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }


        public CustomApiResponse<VelaDTO> BuscarVela(Guid idVela)
        {
            var response = new CustomApiResponse<VelaDTO>();
            try
            {
                var vela = context.Vela.SingleOrDefault(x => x.IDVela == idVela);
                if (vela == null) throw new Exception("Vela no encontrada");
                response.Object = mapper.Map<VelaDTO>(vela);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public CustomApiResponse<VelaDTO> InsertarVela(Vela vel)
        {
            var response = new CustomApiResponse<VelaDTO>();
            try
            {
                var vela = mapper.Map<Vela>(vel);
                vela.IDVela = Guid.NewGuid();
                vela.FechaReal = DateTime.Now;
                                
                // Asegurar FK en pigmentos y fragancias
                if (vela.VelaPigmentos != null)
                    vela.VelaPigmentos.ForEach(vp => vp.IDVela = vela.IDVela);

                if (vela.VelaFragancias != null)
                    vela.VelaFragancias.ForEach(vf => vf.IDVela = vela.IDVela);

                context.Vela.Add(vela);
                context.SaveChanges();

                response.Object = mapper.Map<VelaDTO>(vela);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public CustomApiResponse<VelaDTO> ActualizarVela(Vela vel)
        {
            var response = new CustomApiResponse<VelaDTO>();
            try
            {
                var vela = context.Vela.SingleOrDefault(v => v.IDVela == vel.IDVela);
                if (vela == null) throw new Exception("La vela no existe");

                // Actualizar pigmentos
                var pigmentosActuales = context.VelaPigmento.Where(vp => vp.IDVela == vela.IDVela).ToList();
                var pigmentos = vela.VelaPigmentos ?? new List<VelaPigmento>();

                // Eliminar pigmentos removidos
                var pigmentosAEliminar = pigmentosActuales
                    .Where(p => !pigmentos.Any(pd => pd.IDPig == p.IDPig)).ToList();
                context.VelaPigmento.RemoveRange(pigmentosAEliminar);

                // Agregar o actualizar pigmentos
                foreach (var pig in pigmentos)
                {
                    var existente = pigmentosActuales.FirstOrDefault(p => p.IDPig == pig.IDPig);
                    if (existente != null)
                    {
                        existente.Cantidad = pig.Cantidad;
                        existente.Coste = pig.Coste;
                    }
                    else
                    {
                        var nuevo = mapper.Map<VelaPigmento>(pig);
                        nuevo.IDVela = vela.IDVela;
                        context.VelaPigmento.Add(nuevo);
                    }
                }

                // Actualizar fragancias
                var fraganciasActuales = context.VelaFragancia.Where(vf => vf.IDVela == vela.IDVela).ToList();
                var fragancias = vela.VelaFragancias ?? new List<VelaFragancia>();

                var fraganciasAEliminar = fraganciasActuales
                    .Where(f => !fragancias.Any(fd => fd.IDFrag == f.IDFrag)).ToList();
                context.VelaFragancia.RemoveRange(fraganciasAEliminar);

                foreach (var frag in fragancias)
                {
                    var existente = fraganciasActuales.FirstOrDefault(f => f.IDFrag == frag.IDFrag);
                    if (existente != null)
                    {
                        existente.Cantidad = frag.Cantidad;
                        existente.Coste = frag.Coste;
                    }
                    else
                    {
                        var nueva = mapper.Map<VelaFragancia>(frag);
                        nueva.IDVela = vela.IDVela;
                        context.VelaFragancia.Add(nueva);
                    }
                }

                context.SaveChanges();

                response.Object = mapper.Map<VelaDTO>(vela);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public CustomApiResponse<bool> EliminarVela(Guid idVela)
        {
            var response = new CustomApiResponse<bool>();

            try
            {
                var Vela = context.Vela.SingleOrDefault(x => x.IDVela == idVela);

                if (Vela == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "Vela no encontrado" };
                    response.Object = false;
                }
                else
                {
                    context.Set<Vela>().Remove(Vela);
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
