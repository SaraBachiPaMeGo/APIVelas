using ApiVela.Data;
using ApiVela.Models;
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

        public CustomApiResponse<List<VelaFinalizada>> GetVelaFin()
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

        public CustomApiResponse<VelaFinalizada> BuscarVelaFinalizada(Guid idVelaFinalizada)
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

        public CustomApiResponse<VelaFinalizada> InsertarVelaFinalizada(VelaFinalizada vel)
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
                context.SaveChanges();

                response.Object = mapper.Map<VelaFinalizada>(VelaFinalizada);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }

        public CustomApiResponse<VelaFinalizada> ActualizarVelaFinalizada(VelaFinalizada vel)
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

                context.SaveChanges();

                response.Object = mapper.Map<VelaFinalizada>(velaFin);
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };
            }
            return response;
        }
    }
}
