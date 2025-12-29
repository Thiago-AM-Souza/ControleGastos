using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleGastos.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class BaseDoBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "categoria");

            migrationBuilder.EnsureSchema(
                name: "pessoa");

            migrationBuilder.EnsureSchema(
                name: "transacao");

            migrationBuilder.CreateTable(
                name: "categorias",
                schema: "categoria",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    descricao = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    finalidade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pessoas",
                schema: "pessoa",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    data_nascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pessoas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transacoes",
                schema: "transacao",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    descricao = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    tipo = table.Column<int>(type: "integer", nullable: false),
                    categoria_id = table.Column<Guid>(type: "uuid", nullable: false),
                    pessoa_id = table.Column<Guid>(type: "uuid", nullable: false),
                    valor = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transacoes", x => x.id);
                    table.ForeignKey(
                        name: "FK_transacoes_categorias_categoria_id",
                        column: x => x.categoria_id,
                        principalSchema: "categoria",
                        principalTable: "categorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transacoes_pessoas_pessoa_id",
                        column: x => x.pessoa_id,
                        principalSchema: "pessoa",
                        principalTable: "pessoas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_transacoes_categoria_id",
                schema: "transacao",
                table: "transacoes",
                column: "categoria_id");

            migrationBuilder.CreateIndex(
                name: "IX_transacoes_pessoa_id",
                schema: "transacao",
                table: "transacoes",
                column: "pessoa_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transacoes",
                schema: "transacao");

            migrationBuilder.DropTable(
                name: "categorias",
                schema: "categoria");

            migrationBuilder.DropTable(
                name: "pessoas",
                schema: "pessoa");
        }
    }
}
