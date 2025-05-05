using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace POCA.Banco.Migrations
{
    /// <inheritdoc />
    public partial class Materias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__efmigrationshistory");

            migrationBuilder.CreateTable(
                name: "tb_professores",
                columns: table => new
                {
                    id_professor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nome_professor = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    materia_professor = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    contato_professor = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    login_professor = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    senha_professor = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_professor);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_materias",
                columns: table => new
                {
                    id_materia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    id_professor_materia = table.Column<int>(type: "int", nullable: true),
                    id_aluno_materia = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_materia);
                    table.ForeignKey(
                        name: "id_aluno_materia",
                        column: x => x.id_aluno_materia,
                        principalTable: "tb_alunos",
                        principalColumn: "id_aluno");
                    table.ForeignKey(
                        name: "id_professor_materia",
                        column: x => x.id_professor_materia,
                        principalTable: "tb_professores",
                        principalColumn: "id_professor");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "id_aluno_materia_idx",
                table: "tb_materias",
                column: "id_aluno_materia");

            migrationBuilder.CreateIndex(
                name: "id_professor_materia_idx",
                table: "tb_materias",
                column: "id_professor_materia");

            migrationBuilder.CreateIndex(
                name: "contato_professor_UNIQUE",
                table: "tb_professores",
                column: "contato_professor",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "login_professor_UNIQUE",
                table: "tb_professores",
                column: "login_professor",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "nome_professor_UNIQUE",
                table: "tb_professores",
                column: "nome_professor",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_materias");

            migrationBuilder.DropTable(
                name: "tb_professores");

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
    }
}
