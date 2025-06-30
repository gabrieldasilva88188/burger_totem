using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TotemPWA.Migrations
{
    /// <inheritdoc />
    public partial class AddSlugToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Slug = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    ParentCategoryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Variations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    AdditionalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Variations_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Slug" },
                values: new object[,]
                {
                    { 1, "Lanches", null, "lanches" },
                    { 2, "Combos", null, "combos" },
                    { 3, "Sobremesas", null, "sobremesas" },
                    { 4, "Bebidas", null, "bebidas" },
                    { 5, "Extras", null, "extras" },
                    { 6, "Molhos", null, "molhos" },
                    { 7, "C#", 1, "c" },
                    { 8, "Java", 1, "java" },
                    { 9, "Python", 1, "python" },
                    { 10, "JavaScript", 1, "javascript" },
                    { 11, "Refrigerantes", 4, "refrigerantes" },
                    { 12, "Cafés", 4, "cafs" },
                    { 13, "Acompanhamentos", 5, "acompanhamentos" },
                    { 14, "Anéis de Loop", 5, "anis-de-loop" },
                    { 15, "Tipos de Molhos", 6, "tipos-de-molhos" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 2, 2, "C# Burguer + Loop Fries + Byte-Cola", "Combo Stack Overflow", 32.99m },
                    { 5, 6, "Só aparece quando você não espera", "NullPointer Molho", 3.00m },
                    { 10, 2, "JS Double Shot + Snake.py + Byte-Cola", "Combo Dev Full Stack", 39.90m },
                    { 11, 3, "Sobremesa que quebra qualquer dieta", "Exception Sundae", 12.00m },
                    { 12, 3, "Doce processado com zero erros", "Compiler Cheesecake", 10.50m }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Slug" },
                values: new object[,]
                {
                    { 16, "Molhos Picantes", 15, "molhos-picantes" },
                    { 17, "Molhos Doces", 15, "molhos-doces" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 7, "Burguer orientado a objetos com queijo derretido", "C# Burguer", 18.50m },
                    { 3, 12, "Café forte com robusta implementação", "JavaBean Espresso", 7.50m },
                    { 4, 13, "Batatas em looping infinito", "Loop Fries", 9.00m },
                    { 6, 8, "Sabor não encontrado, mas delicioso", "Burguer 404", 19.99m },
                    { 7, 9, "Sanduíche dinâmico e legível", "Snake.py Sanduíche", 17.00m },
                    { 8, 10, "Burguer assíncrono com dois hambúrgueres", "JS Double Shot", 21.00m },
                    { 9, 11, "Refrigerante com sabor binário", "Byte-Cola", 5.50m },
                    { 13, 12, "Café com operadores booleanos", "Latte Lógico", 6.00m },
                    { 14, 13, "Batatas fritas com comportamento inesperado", "Bug Fries", 8.90m },
                    { 15, 7, "Frango funcional com sabor puro", "Lambda Chicken", 16.99m },
                    { 16, 16, "Molho picante que trata qualquer exceção", "Spicy Try-Catch", 4.50m },
                    { 17, 17, "Molho doce com final em recursão infinita", "Sweet Loop", 4.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Variations_ProductId",
                table: "Variations",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Variations");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
