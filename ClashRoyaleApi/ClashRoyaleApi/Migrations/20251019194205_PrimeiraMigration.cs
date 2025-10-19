using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashRoyaleApi.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Arena",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    TrofeusMinimos = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arena", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Batalha",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TipoBatalha = table.Column<string>(type: "TEXT", nullable: false),
                    DataHora = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DuracaoSegundos = table.Column<int>(type: "INTEGER", nullable: false),
                    Resultado = table.Column<string>(type: "TEXT", nullable: false),
                    TrofeusGanhosPerdidos = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batalha", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    CustoElixir = table.Column<int>(type: "INTEGER", nullable: false),
                    Raridade = table.Column<string>(type: "TEXT", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    PossuiEvolucao = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartaJogador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdJogador = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCarta = table.Column<int>(type: "INTEGER", nullable: false),
                    NivelAtual = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantidadeCartas = table.Column<int>(type: "INTEGER", nullable: false),
                    PossuiEvolucaoDesbloqueada = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartaJogador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cla",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdLider = table.Column<int>(type: "INTEGER", nullable: false),
                    NomeCla = table.Column<string>(type: "TEXT", nullable: false),
                    TrofeusMinimos = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoAdesao = table.Column<string>(type: "TEXT", nullable: false),
                    PontosGuerra = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cla", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdJogador = table.Column<int>(type: "INTEGER", nullable: false),
                    NomeDeck = table.Column<string>(type: "TEXT", nullable: false),
                    EhAtivo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deck", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jogador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeUsuario = table.Column<string>(type: "TEXT", nullable: false),
                    Nivel = table.Column<int>(type: "INTEGER", nullable: false),
                    Trofeus = table.Column<int>(type: "INTEGER", nullable: false),
                    Gemas = table.Column<int>(type: "INTEGER", nullable: false),
                    Ouro = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParticipanteBatalha",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdBatalha = table.Column<int>(type: "INTEGER", nullable: false),
                    IdJogador = table.Column<int>(type: "INTEGER", nullable: false),
                    IdDeckUsado = table.Column<int>(type: "INTEGER", nullable: false),
                    CoroasConquistadas = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipanteBatalha", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arena");

            migrationBuilder.DropTable(
                name: "Batalha");

            migrationBuilder.DropTable(
                name: "Carta");

            migrationBuilder.DropTable(
                name: "CartaJogador");

            migrationBuilder.DropTable(
                name: "Cla");

            migrationBuilder.DropTable(
                name: "Deck");

            migrationBuilder.DropTable(
                name: "Jogador");

            migrationBuilder.DropTable(
                name: "ParticipanteBatalha");
        }
    }
}
