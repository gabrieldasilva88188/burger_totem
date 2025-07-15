using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TotemPWA.Migrations
{
    /// <inheritdoc />
    public partial class ImagemIngre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Ingredients",
                type: "BLOB",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Ingredients");
        }
    }
}
