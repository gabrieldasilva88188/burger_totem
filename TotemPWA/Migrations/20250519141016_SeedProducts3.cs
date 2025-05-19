using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TotemPWA.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 6, 7, "Pão, carne, queijo e molho especial", "Cheeseburger 2", 15.99m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
