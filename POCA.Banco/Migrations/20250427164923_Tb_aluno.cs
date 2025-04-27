using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace POCA.Banco.Migrations
{
    /// <inheritdoc />
    public partial class Tb_aluno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_alunos",
                columns: table => new
                {
                    id_aluno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nome_aluno = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    idade_aluno = table.Column<int>(type: "int", nullable: false),
                    progresso_aluno = table.Column<int>(type: "int", nullable: false),
                    contato_aluno = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    login_aluno = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    senha_aluno = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_aluno);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "nome_aluno_UNIQUE",
                table: "tb_alunos",
                column: "nome_aluno",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_alunos");
        }
    }
}
