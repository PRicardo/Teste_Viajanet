using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ViajanetFullstack.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PedidosClientes",
                columns: table => new
                {
                    PedidoClienteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ip = table.Column<string>(nullable: true),
                    Browser = table.Column<string>(nullable: true),
                    Pagina = table.Column<string>(nullable: true),
                    IdaVolta = table.Column<int>(nullable: false),
                    Origem = table.Column<string>(nullable: true),
                    Destino = table.Column<string>(nullable: true),
                    DataIda = table.Column<DateTime>(nullable: false),
                    DataVolta = table.Column<DateTime>(nullable: false),
                    QtdAdultos = table.Column<int>(nullable: false),
                    QtdCriancas = table.Column<int>(nullable: false),
                    QtdBebes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosClientes", x => x.PedidoClienteId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidosClientes");
        }
    }
}
