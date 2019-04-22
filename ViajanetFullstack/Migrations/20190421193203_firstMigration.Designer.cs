﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ViajanetFullstack.Models;

namespace ViajanetFullstack.Migrations
{
    [DbContext(typeof(ViajanetContext))]
    [Migration("20190421193203_firstMigration")]
    partial class firstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ViajanetFullstack.Models.PedidoCliente", b =>
                {
                    b.Property<int>("PedidoClienteId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Browser");

                    b.Property<DateTime>("DataIda");

                    b.Property<DateTime>("DataVolta");

                    b.Property<string>("Destino");

                    b.Property<int>("IdaVolta");

                    b.Property<string>("Ip");

                    b.Property<string>("Origem");

                    b.Property<string>("Pagina");

                    b.Property<int>("QtdAdultos");

                    b.Property<int>("QtdBebes");

                    b.Property<int>("QtdCriancas");

                    b.HasKey("PedidoClienteId");

                    b.ToTable("PedidosClientes");
                });
#pragma warning restore 612, 618
        }
    }
}
