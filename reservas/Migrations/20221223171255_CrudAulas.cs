using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace reservas.Migrations
{
    /// <inheritdoc />
    public partial class CrudAulas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aula_Edificios_EdificioId",
                table: "Aula");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Aula",
                table: "Aula");

            migrationBuilder.RenameTable(
                name: "Aula",
                newName: "Aulas");

            migrationBuilder.RenameIndex(
                name: "IX_Aula_EdificioId",
                table: "Aulas",
                newName: "IX_Aulas_EdificioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Aulas",
                table: "Aulas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_Name",
                table: "Aulas",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Edificios_EdificioId",
                table: "Aulas",
                column: "EdificioId",
                principalTable: "Edificios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Edificios_EdificioId",
                table: "Aulas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Aulas",
                table: "Aulas");

            migrationBuilder.DropIndex(
                name: "IX_Aulas_Name",
                table: "Aulas");

            migrationBuilder.RenameTable(
                name: "Aulas",
                newName: "Aula");

            migrationBuilder.RenameIndex(
                name: "IX_Aulas_EdificioId",
                table: "Aula",
                newName: "IX_Aula_EdificioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Aula",
                table: "Aula",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Aula_Edificios_EdificioId",
                table: "Aula",
                column: "EdificioId",
                principalTable: "Edificios",
                principalColumn: "Id");
        }
    }
}
