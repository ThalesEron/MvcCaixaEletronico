using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class InicioBanco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CaixaEletronico",
                columns: table => new
                {
                    CaixalID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    ValorNota = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaixaEletronico", x => x.CaixalID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaixaEletronico");
        }
    }
}
