using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POCA.Banco.Migrations
{
    /// <inheritdoc />
    public partial class TbQuestaoV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "dificuldade_questao",
                table: "tb_questoes",
                type: "enum('Fácil','Médio','Difícil')",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "tema_questao",
                table: "tb_questoes",
                type: "enum('Teoria','Programação')",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "__efmigrationshistory",
                columns: table => new
                {
                    MigrationId = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    ProductVersion = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.MigrationId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__efmigrationshistory");

            migrationBuilder.DropColumn(
                name: "dificuldade_questao",
                table: "tb_questoes");

            migrationBuilder.DropColumn(
                name: "tema_questao",
                table: "tb_questoes");
        }
    }
}
