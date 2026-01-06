using ApiVela.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVela.Data
{
    public class Contexto  : DbContext
    {

        public Contexto(DbContextOptions<Contexto> options) : base(options)
        { }
        public DbSet<Cera> Cera { get; set; }
        public DbSet<VelaFinalizada> VelaFinalizada { get; set; }
        public DbSet<Pack> Pack { get; set; }
        public DbSet<Endurecedor> Endurecedor { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Documento> Documento { get; set; }
        
        public DbSet<Fragancia> Fragancia { get; set; }
        public DbSet<Mecha> Mecha { get; set; }
        public DbSet<Molde> Molde { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Pigmento> Pigmento { get; set; }
        public DbSet<Vela> Vela { get; set; }

        public DbSet<VelaPigmento> VelaPigmento { get; set; }
        public DbSet<VelaFragancia> VelaFragancia { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Pedidos)
                .WithOne(p => p.Cliente)
                .HasForeignKey(p => p.IDCliente)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VelaFinalizada>()
                .HasOne(v => v.Pedido)
                .WithMany(p => p.VelaFin)
                .HasForeignKey(v => v.IDPedido)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<VelaFragancia>()
                .HasKey(vf => new { vf.IDVela, vf.IDFrag });

            modelBuilder.Entity<VelaFragancia>()
                .HasOne(vf => vf.Vela)
                .WithMany(v => v.VelaFragancias) // si tienes la colección en Vela
                .HasForeignKey(vf => vf.IDVela);

            //modelBuilder.Entity<VelaFragancia>()
            //    .HasOne(vf => vf.Fragancia)
            //    .WithMany(f => f.VelaFragancia) // si tienes la colección en Fragancia
            //    .HasForeignKey(vf => vf.IDFrag);

            modelBuilder.Entity<VelaPigmento>()
                .HasKey(vp => new { vp.IDVela, vp.IDPig });

            modelBuilder.Entity<VelaPigmento>()
                .HasOne(vp => vp.Vela)
                .WithMany(v => v.VelaPigmentos) // si tienes la colección en Vela
                .HasForeignKey(vp => vp.IDVela);

            //modelBuilder.Entity<VelaPigmento>()
            //    .HasOne(vp => vp.Pigmento)
            //    .WithMany(p => p.VelaPigmentos) // si tienes la colección en Pigmento
            //    .HasForeignKey(vp => vp.IDPig);

            // VelaFinalizada -> Velas (1 - N)
            modelBuilder.Entity<VelaFinalizada>()
                .HasMany(vf => vf.Velas)
                .WithOne(v => v.VelaFinalizada)
                .HasForeignKey(v => v.IDVelaFin)
                .OnDelete(DeleteBehavior.SetNull); // o Cascade según tu lógica

            // VelaFinalizada -> Packs (1 - N)
            //modelBuilder.Entity<VelaFinalizada>()
            //    .HasMany(vf => vf.Packs)
            //    .WithOne(p => p.VelaFinalizada)
            //    .HasForeignKey(p => p.IDVelaFin)
            //    .OnDelete(DeleteBehavior.SetNull); // o Cascade
        }
    }
}
