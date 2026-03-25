using ApiVela.Data;
using ApiVela.Models;
using ApiVela.Models.DTO;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVela.Repositories
{
    public class RepositoryVelaFin
    {
        private readonly Contexto context;
        private readonly IMapper mapper;

        public RepositoryVelaFin(Contexto context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CustomApiResponse<List<VelaFinalizada>>> GetVelaFin1()
        {
            var response = new CustomApiResponse<List<VelaFinalizada>>();
            try
            {
                var VelaFins = context.VelaFinalizada.ToList();
                response.Object = mapper.Map<List<VelaFinalizada>>(VelaFins);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public async Task<CustomApiResponse<List<VelaFinDTO>>> GetVelasFinalizadas()
        {
            var response = new CustomApiResponse<List<VelaFinDTO>>();

            try
            {
                var list = context.VelaFinalizada
                    .Include(vf => vf.Pedido)
                    .Include(vf => vf.Velas)   // carga las velas relacionadas
                    .Include(vf => vf.Packs)   // carga los packs relacionados
                    .AsNoTracking()
                    .Select(vf => new VelaFinDTO
                    {
                        IDVelaFin = vf.IDVelaFin,
                        Coste = vf.Coste,
                        Beneficio = vf.Beneficio,
                        PVP = vf.PVP,
                        FechaFin = vf.FechaFin,

                        Velas = vf.Velas.Select(v => new VelaDTO
                        {
                            IDVela = v.IDVela,
                            VelaNombre = v.VelaNombre,
                            Image = v.Image,
                            FechaReal = v.FechaReal,
                            Coste = v.Coste,
                            CantidadCera = v.CantidadCera,
                            CantidadEnd = v.CantidadEnd,
                    // NO incluyas Documentos binarios enteros si no los necesitas aquí
                }).ToList(),

                        Packs = vf.Packs.Select(p => new PackDTO
                        {
                            IDPack = p.IDPack,
                            Tipo = p.Tipo,
                            Firma = p.Firma,
                            Coste = p.Coste
                        }).ToList(),

                        Pedido = vf.Pedido == null ? null : new PedidoDTO
                        {
                            IDPedido = vf.Pedido.IDPedido,
                            FechaPedi = vf.Pedido.FechaPedi,
                            FechaEntrega = vf.Pedido.FechaEntrega,
                            Vendido = vf.Pedido.Vendido
                        }
                    })
                    .ToList();

                response.Object = list;
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }

            return response;
        }



        public async Task<CustomApiResponse<VelaFinalizada>>  BuscarVelaFinalizada(Guid idVelaFinalizada)
        {
            var response = new CustomApiResponse<VelaFinalizada>();
            try
            {
                var VelaFinalizada = context.VelaFinalizada.SingleOrDefault(x => x.IDVelaFin == idVelaFinalizada);
                if (VelaFinalizada == null) throw new Exception("VelaFinalizada no encontrada");
                response.Object = mapper.Map<VelaFinalizada>(VelaFinalizada);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public async Task<CustomApiResponse<VelaFinalizada>>  InsertarVelaFinalizada(VelaFinalizada vel)
        {
            var response = new CustomApiResponse<VelaFinalizada>();
            try
            {
                context.Database.ExecuteSqlRawAsync(
                     "EXEC sp_CalcularVelaFinalizada @IDVela={0}, @IDPedido={1}, @IDPack={2}",
                     vel.IDVela, vel.IDPedido, vel.IDPack
                 );

                var VelaFinalizada = mapper.Map<VelaFinalizada>(vel);
                VelaFinalizada.IDVelaFin = Guid.NewGuid();
                VelaFinalizada.FechaFin = DateTime.Now;

                // 🔹 Validar que el pedido asociado exista
                if (Guid.Empty == VelaFinalizada.IDPedido)
                {
                    var pedidoExiste = context.Pedido.Any(p => p.IDPedido == VelaFinalizada.IDPedido);
                    if (!pedidoExiste)
                        throw new Exception($"No se encontró el pedido con ID {VelaFinalizada.IDPedido}");
                }

                if (Guid.Empty == VelaFinalizada.IDPack)
                {
                    var packExiste = context.Pack.Any(p => p.IDPack == VelaFinalizada.IDPack);
                    if (!packExiste)
                        throw new Exception($"No se encontró el pack con ID {VelaFinalizada.IDPack}");
                }


                context.VelaFinalizada.Add(VelaFinalizada);
                await context.SaveChangesAsync();

                response.Object = mapper.Map<VelaFinalizada>(VelaFinalizada);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public async Task<CustomApiResponse<VelaFinalizada>>  ActualizarVelaFinalizada(VelaFinalizada vel)
        {
            var response = new CustomApiResponse<VelaFinalizada>();
            try
            {
                var velaFin = context.VelaFinalizada.SingleOrDefault(v => v.IDVelaFin == vel.IDVelaFin);
                if (velaFin == null) throw new Exception("La VelaFinalizada no existe");

                // Actualiza solo los campos que hayan cambiado
                if (vel.FechaFin != velaFin.FechaFin) velaFin.FechaFin = vel.FechaFin;
                if (vel.Coste != velaFin.Coste) velaFin.Coste = vel.Coste;
                if (vel.Beneficio != velaFin.Beneficio) velaFin.Beneficio = vel.Beneficio;
                if (vel.IDVela != velaFin.IDVela) velaFin.IDVela = vel.IDVela;
                if (vel.IDPedido != velaFin.IDPedido) velaFin.IDPedido = vel.IDPedido;
                if (vel.IDPack != velaFin.IDPack) velaFin.IDPack = vel.IDPack;

                await context.SaveChangesAsync();

                response.Object = mapper.Map<VelaFinalizada>(velaFin);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public async Task<CustomApiResponse<bool>> EliminarVelaFinalizada(Guid idVelaFinalizada)
        {
            var response = new CustomApiResponse<bool>();

            try
            {
                var VelaFinalizada = context.VelaFinalizada.SingleOrDefault(x => x.IDVelaFin == idVelaFinalizada);

                if (VelaFinalizada == null)
                {
                    response.Error = new ErrorViewModel { Mensaje = "VelaFinalizada no encontrado" };
                    response.Object = false;
                }
                else
                {
                    context.Set<VelaFinalizada>().Remove(VelaFinalizada);
                    await context.SaveChangesAsync();
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
