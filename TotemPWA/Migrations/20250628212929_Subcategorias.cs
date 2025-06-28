using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TotemPWA.Migrations
{
    /// <inheritdoc />
    public partial class Subcategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Name", "Slug" },
                values: new object[] { "Smash Burgers", "smash-burgers" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Name", "Slug" },
                values: new object[] { "Hambúrguer Artesanal", "hambrguer-artesanal" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Name", "Slug" },
                values: new object[] { "Lanches Vegetarianos", "lanches-vegetarianos" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Name", "Slug" },
                values: new object[] { "Lanches com Frango", "lanches-com-frango" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Name", "ParentCategoryId", "Slug" },
                values: new object[] { "Duplos e Triplo Burgers", 1, "duplos-e-triplo-burgers" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Name", "ParentCategoryId", "Slug" },
                values: new object[] { "Combo Econômico", 2, "combo-econmico" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Name", "ParentCategoryId", "Slug" },
                values: new object[] { "Combo Casal", 2, "combo-casal" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Name", "ParentCategoryId", "Slug" },
                values: new object[] { "Combo Infantil", 2, "combo-infantil" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Name", "ParentCategoryId", "Slug" },
                values: new object[] { "Combo Veggie", 2, "combo-veggie" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Name", "ParentCategoryId", "Slug" },
                values: new object[] { "Combo Premium", 2, "combo-premium" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Name", "ParentCategoryId", "Slug" },
                values: new object[] { "Sundaes", 3, "sundaes" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Slug" },
                values: new object[,]
                {
                    { 18, "Milkshakes", 3, "milkshakes" },
                    { 19, "Brownies", 3, "brownies" },
                    { 20, "Tortas Geladas", 3, "tortas-geladas" },
                    { 21, "Sorvetes e Gelatos", 3, "sorvetes-e-gelatos" },
                    { 22, "Refrigerantes", 4, "refrigerantes" },
                    { 23, "Sucos Naturais", 4, "sucos-naturais" },
                    { 24, "Energéticos", 4, "energticos" },
                    { 25, "Bebidas Quentes", 4, "bebidas-quentes" },
                    { 26, "Bebidas Zero Açúcar", 4, "bebidas-zero-acar" },
                    { 27, "Batatas Fritas", 5, "batatas-fritas" },
                    { 28, "Onion Rings", 5, "onion-rings" },
                    { 29, "Nuggets", 5, "nuggets" },
                    { 30, "Queijo Empanado", 5, "queijo-empanado" },
                    { 31, "Salada Extra", 5, "salada-extra" },
                    { 32, "Molhos Clássicos", 6, "molhos-clssicos" },
                    { 33, "Molhos Picantes", 6, "molhos-picantes" },
                    { 34, "Molhos Doces", 6, "molhos-doces" },
                    { 35, "Molhos Especiais da Casa", 6, "molhos-especiais-da-casa" },
                    { 36, "Molhos Veganos", 6, "molhos-veganos" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 7, "Lanche organizado por indentação com sabor dinâmico", "Python Stack", 17.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 8, "Bagunçado, mas funcional", "PHP Buguer", 15.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 8, "Sanduíche robusto com tipagem forte", "Java Sandwich", 18.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 11, "Burger crocante com padrão de arquitetura limpa", "Crunch#", 19.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 9, "Requintado, elegante e poderoso", "Ruby Royale", 22.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 10, "Prático, rápido e direto ao ponto", "Go Burguer", 17.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Com cheddar intenso, goroutines de sabor", "Go Burguer Cheddar", 18.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 9, "Opção vegetariana com performance", "Go Burguer Veggie", 16.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CategoryId", "Name" },
                values: new object[] { 16, "Combo Full Stack" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 14, "Combo perfeito para codar e matar a fome", "Dev Meal", 34.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 15, "Combo com itens refatorados do menu", "Refatoração Total", 37.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 13, "Combo com dois Go Burguers e batata", "Go Burguers", 31.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 12, "Para quem vive na nuvem (literalmente)", "Cloud Combo", 32.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 12, "Interface deliciosa entre fome e felicidade", "API Meal", 33.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 32, "Molho com sabor aprimorado", "Barbecue++", 3.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 33, "Molho picante, mas reativo", "Chipotle.js", 3.50m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Image", "Name", "Price" },
                values: new object[,]
                {
                    { 23, 17, "Sobremesa quente hospedada com amor", null, "Petit Gateau da Nuvem", 12.90m },
                    { 18, 34, "Maionese inteligente que aprende seu gosto", null, "MayoAI", 3.00m },
                    { 19, 32, "Molho básico com endpoints deliciosos", null, "KetchAPI", 2.90m },
                    { 20, 35, "Molho com sabor dinâmico e animado", null, "Salsa Script", 4.00m },
                    { 21, 36, "Visualmente bonito e bem formatado", null, "Css Cream", 4.00m },
                    { 22, 18, "Bate tudo e retorna o melhor resultado", null, "MilkShake SQL", 10.00m },
                    { 24, 21, "Sempre responsiva e cremosa", null, "Casquinha Bootstrap", 6.00m },
                    { 25, 19, "Doces armazenados para carregamento rápido", null, "Cache Cookies", 7.50m },
                    { 26, 20, "Fácil de consumir e muito poderosa", null, "Python Pie", 9.00m },
                    { 27, 20, "Bolo orientado a delícias", null, "Cake++", 10.50m },
                    { 28, 22, "Refrigerante com sintaxe refrescante", null, "ColaScript", 5.50m },
                    { 29, 23, "Suco forte e robusto", null, "JavaJuice", 6.00m },
                    { 30, 24, "Refresca na hora e reage ao seu gosto", null, "React Refresco", 6.50m },
                    { 31, 24, "Bebida funcional e saborosa", null, "SmoothieScript", 7.00m },
                    { 32, 26, "Limonada elegante com sabor refinado", null, "Ruby Lemonade", 6.90m },
                    { 33, 25, "Bebida ágil e intensa", null, "Swift Sake", 8.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Name", "Slug" },
                values: new object[] { "C#", "c" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Name", "Slug" },
                values: new object[] { "Java", "java" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Name", "Slug" },
                values: new object[] { "Python", "python" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Name", "Slug" },
                values: new object[] { "JavaScript", "javascript" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Name", "ParentCategoryId", "Slug" },
                values: new object[] { "Refrigerantes", 4, "refrigerantes" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Name", "ParentCategoryId", "Slug" },
                values: new object[] { "Cafés", 4, "cafs" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Name", "ParentCategoryId", "Slug" },
                values: new object[] { "Acompanhamentos", 5, "acompanhamentos" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Name", "ParentCategoryId", "Slug" },
                values: new object[] { "Anéis de Loop", 5, "anis-de-loop" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Name", "ParentCategoryId", "Slug" },
                values: new object[] { "Tipos de Molhos", 6, "tipos-de-molhos" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Name", "ParentCategoryId", "Slug" },
                values: new object[] { "Molhos Picantes", 15, "molhos-picantes" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Name", "ParentCategoryId", "Slug" },
                values: new object[] { "Molhos Doces", 15, "molhos-doces" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 2, "C# Burguer + Loop Fries + Byte-Cola", "Combo Stack Overflow", 32.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 12, "Café forte com robusta implementação", "JavaBean Espresso", 7.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 13, "Batatas em looping infinito", "Loop Fries", 9.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 6, "Só aparece quando você não espera", "NullPointer Molho", 3.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 8, "Sabor não encontrado, mas delicioso", "Burguer 404", 19.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 9, "Sanduíche dinâmico e legível", "Snake.py Sanduíche", 17.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "Name", "Price" },
                values: new object[] { "Burguer assíncrono com dois hambúrgueres", "JS Double Shot", 21.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 11, "Refrigerante com sabor binário", "Byte-Cola", 5.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CategoryId", "Name" },
                values: new object[] { 2, "Combo Dev Full Stack" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 3, "Sobremesa que quebra qualquer dieta", "Exception Sundae", 12.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 3, "Doce processado com zero erros", "Compiler Cheesecake", 10.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 12, "Café com operadores booleanos", "Latte Lógico", 6.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 13, "Batatas fritas com comportamento inesperado", "Bug Fries", 8.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 7, "Frango funcional com sabor puro", "Lambda Chicken", 16.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 16, "Molho picante que trata qualquer exceção", "Spicy Try-Catch", 4.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CategoryId", "Description", "Name", "Price" },
                values: new object[] { 17, "Molho doce com final em recursão infinita", "Sweet Loop", 4.00m });
        }
    }
}
