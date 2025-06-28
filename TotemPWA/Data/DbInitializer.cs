using System.Text.Json;
using System.Text.Json.Nodes;
using TotemPWA.Models;

namespace TotemPWA.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context)
        {
            if (context.Categories.Any() && context.Cupons.Any())
                return;

            var json = await File.ReadAllTextAsync("Data/SeedData.json");

            // Lê o arquivo como JsonNode para acessar as partes separadas
            var rootArray = JsonNode.Parse(json)?.AsArray();
            if (rootArray == null) return;

            foreach (var element in rootArray)
            {
                // Verifica se é um objeto com "Cupons"
                if (element?["Cupom"] is JsonArray cuponsArray)
                {
                    // Só adiciona se ainda não tiver cupons no banco
                    if (!context.Cupons.Any())
                    {
                        foreach (var c in cuponsArray)
                        {
                            var cupom = new Cupom
                            {
                                Code = c?["Code"]?.ToString() ?? "",
                                Discount = c?["Discount"]?.GetValue<decimal>() ?? 0,
                                ValidUntil = c?["ValidUntil"]?.GetValue<DateTime?>(),
                                Active = c?["Active"]?.GetValue<bool>() ?? false
                            };
                            context.Cupons.Add(cupom);
                        }
                    }
                }
                // Se for uma categoria, processa normalmente
                else if (element?["name"] != null)
                {
                    var categorySeed = element.Deserialize<CategorySeed>();
                    if (categorySeed != null)
                        await CreateCategoryRecursiveAsync(context, categorySeed, null);
                }
            }

            await context.SaveChangesAsync();
        }

        private static async Task CreateCategoryRecursiveAsync(ApplicationDbContext context, CategorySeed seed, int? parentId)
        {
            var category = new Category
            {
                Name = seed.Name,
                ParentCategoryId = parentId
            };

            context.Categories.Add(category);
            await context.SaveChangesAsync(); // necessário para obter o Id

            foreach (var productSeed in seed.Products ?? new List<ProductSeed>())
            {
                var product = new Product
                {
                    Name = productSeed.Name,
                    Price = productSeed.Price,
                    Description = productSeed.Description,
                    CategoryId = category.Id
                };

                context.Products.Add(product);
                await context.SaveChangesAsync();

                foreach (var variationSeed in productSeed.Variations ?? new List<VariationSeed>())
                {
                    var variation = new Variation
                    {
                        Description = variationSeed.Description,
                        AdditionalPrice = variationSeed.AdditionalPrice,
                        ProductId = product.Id
                    };

                    context.Variations.Add(variation);
                }
            }

            foreach (var subcategorySeed in seed.Subcategories ?? new List<CategorySeed>())
            {
                await CreateCategoryRecursiveAsync(context, subcategorySeed, category.Id);
            }
        }
    }
}
