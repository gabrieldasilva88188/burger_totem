using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TotemPWA.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryToCombo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Adiciona a coluna nullable, sem FK
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Combos",
                type: "INTEGER",
                nullable: true);

            // Atualiza todos os combos existentes para uma categoria válida (ex: 12)
            migrationBuilder.Sql("UPDATE Combos SET CategoryId = 12 WHERE CategoryId IS NULL;");

            // Torna a coluna NOT NULL
            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Combos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            // Cria o índice e a constraint de FK
            migrationBuilder.CreateIndex(
                name: "IX_Combos_CategoryId",
                table: "Combos",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Combos_Categories_CategoryId",
                table: "Combos",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Combos_Categories_CategoryId",
                table: "Combos");

            migrationBuilder.DropIndex(
                name: "IX_Combos_CategoryId",
                table: "Combos");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Combos");
        }
    }
}
