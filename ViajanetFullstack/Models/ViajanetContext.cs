using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViajanetFullstack.Models
{
    public class ViajanetContext : DbContext
    {
        public ViajanetContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Viajanet;Trusted_Connection=True;ConnectRetryCount=0");
            }
        }

        public DbSet<PedidoCliente> PedidosClientes { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqldb;Database=Viajanet;Trusted_Connection=true;MultipleActiveResultSets=true");
        //}
    }
}
