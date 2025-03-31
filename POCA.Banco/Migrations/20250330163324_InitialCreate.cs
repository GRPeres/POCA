using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace POCA.Banco.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_questoes",
                columns: table => new
                {
                    id_questao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    enunciado_questao = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    respostacerta_questao = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    respostaerrada1_questao = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    respostaerrada2_questao = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    respostaerrada3_questao = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_questao);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_questoes");
        }
    }
}
