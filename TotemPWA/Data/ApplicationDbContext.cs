using Microsoft.EntityFrameworkCore;
using TotemPWA.Models;

namespace TotemPWA.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Variation> Variations { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<ProductIngredient> ProductIngredients { get; set; }
        public DbSet<Cupom> Cupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define self-referencing relationship for Category
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.Subcategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.NoAction);  // Allow parent category to be null

            // Define 1-to-many relationship between Product and Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Define 1-to-many relationship between Product and Variation
            modelBuilder.Entity<Variation>()
                .HasOne(v => v.Product)
                .WithMany(p => p.Variations)
                .HasForeignKey(v => v.ProductId);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Variation>()
                .Property(v => v.AdditionalPrice)
                .HasColumnType("decimal(18,2)");


            modelBuilder.Entity<Client>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Client>("Client")
                .HasValue<Employee>("Employee");

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Category>().HasData(
                // Categorias principais
                new Category { Id = 1, Name = "Lanches", ParentCategoryId = null },
                new Category { Id = 2, Name = "Combos", ParentCategoryId = null },
                new Category { Id = 3, Name = "Sobremesas", ParentCategoryId = null },
                new Category { Id = 4, Name = "Bebidas", ParentCategoryId = null },
                new Category { Id = 5, Name = "Extras", ParentCategoryId = null },
                new Category { Id = 6, Name = "Molhos", ParentCategoryId = null },

                // Subcategorias de Lanches (1)
                new Category { Id = 7, Name = "Smash Burgers", ParentCategoryId = 1 },
                new Category { Id = 8, Name = "Hambúrguer Artesanal", ParentCategoryId = 1 },
                new Category { Id = 9, Name = "Vegetarianos", ParentCategoryId = 1 },
                new Category { Id = 10, Name = "Frango", ParentCategoryId = 1 },
                new Category { Id = 11, Name = "Duplos e Triplo Burgers", ParentCategoryId = 1 },

                // Subcategorias de Combos (2)
                new Category { Id = 12, Name = "Combo Econômico", ParentCategoryId = 2 },
                new Category { Id = 13, Name = "Combo Casal", ParentCategoryId = 2 },
                new Category { Id = 14, Name = "Combo Infantil", ParentCategoryId = 2 },
                new Category { Id = 15, Name = "Combo Veggie", ParentCategoryId = 2 },
                new Category { Id = 16, Name = "Combo Premium", ParentCategoryId = 2 },

                // Subcategorias de Sobremesas (3)
                new Category { Id = 17, Name = "Sundaes", ParentCategoryId = 3 },
                new Category { Id = 18, Name = "Milkshakes", ParentCategoryId = 3 },
                new Category { Id = 19, Name = "Brownies", ParentCategoryId = 3 },
                new Category { Id = 20, Name = "Tortas Geladas", ParentCategoryId = 3 },
                new Category { Id = 21, Name = "Sorvetes e Gelatos", ParentCategoryId = 3 },

                // Subcategorias de Bebidas (4)
                new Category { Id = 22, Name = "Refrigerantes", ParentCategoryId = 4 },
                new Category { Id = 23, Name = "Sucos Naturais", ParentCategoryId = 4 },
                new Category { Id = 24, Name = "Energéticos", ParentCategoryId = 4 },
                new Category { Id = 25, Name = "Bebidas Quentes", ParentCategoryId = 4 },
                new Category { Id = 26, Name = "Bebidas Zero Açúcar", ParentCategoryId = 4 },

                // Subcategorias de Extras (5)
                new Category { Id = 27, Name = "Batatas Fritas", ParentCategoryId = 5 },
                new Category { Id = 28, Name = "Onion Rings", ParentCategoryId = 5 },
                new Category { Id = 29, Name = "Nuggets", ParentCategoryId = 5 },
                new Category { Id = 30, Name = "Queijo Empanado", ParentCategoryId = 5 },
                new Category { Id = 31, Name = "Salada Extra", ParentCategoryId = 5 },

                // Subcategorias de Molhos (6)
                new Category { Id = 32, Name = "Molhos Clássicos", ParentCategoryId = 6 },
                new Category { Id = 33, Name = "Molhos Picantes", ParentCategoryId = 6 },
                new Category { Id = 34, Name = "Molhos Doces", ParentCategoryId = 6 },
                new Category { Id = 35, Name = "Molhos Especiais da Casa", ParentCategoryId = 6 },
                new Category { Id = 36, Name = "Molhos Veganos", ParentCategoryId = 6 }
            );
        
        

           modelBuilder.Entity<Product>().HasData(
            // Lanches: Subcategoria 7 a 11
            new Product { Id = 1, Name = "C# Burguer", CategoryId = 7, Price = 18.50m, Description = "Burguer orientado a objetos com queijo derretido" },
            new Product { Id = 2, Name = "Python Stack", CategoryId = 7, Price = 17.90m, Description = "Lanche organizado por indentação com sabor dinâmico" },
            new Product { Id = 3, Name = "PHP Buguer", CategoryId = 8, Price = 15.90m, Description = "Bagunçado, mas funcional" },
            new Product { Id = 4, Name = "Java Sandwich", CategoryId = 8, Price = 18.00m, Description = "Sanduíche robusto com tipagem forte" },
            new Product { Id = 5, Name = "Crunch#", CategoryId = 11, Price = 19.90m, Description = "Burger crocante com padrão de arquitetura limpa" },
            new Product { Id = 6, Name = "Ruby Royale", CategoryId = 9, Price = 22.00m, Description = "Requintado, elegante e poderoso" },
            new Product { Id = 7, Name = "Go Burguer", CategoryId = 10, Price = 17.50m, Description = "Prático, rápido e direto ao ponto" },
            new Product { Id = 8, Name = "Go Burguer Cheddar", CategoryId = 10, Price = 18.90m, Description = "Com cheddar intenso, goroutines de sabor" },
            new Product { Id = 9, Name = "Go Burguer Veggie", CategoryId = 9, Price = 16.50m, Description = "Opção vegetariana com performance" },

            // Combos: Subcategoria 12 a 16
            new Product { Id = 10, Name = "Combo Full Stack", CategoryId = 16, Price = 39.90m, Description = "JS Double Shot + Snake.py + Byte-Cola" },
            new Product { Id = 11, Name = "Dev Meal", CategoryId = 14, Price = 34.90m, Description = "Combo perfeito para codar e matar a fome" },
            new Product { Id = 12, Name = "Refatoração Total", CategoryId = 15, Price = 37.90m, Description = "Combo com itens refatorados do menu" },
            new Product { Id = 13, Name = "Go Burguers", CategoryId = 13, Price = 31.90m, Description = "Combo com dois Go Burguers e batata" },
            new Product { Id = 14, Name = "Cloud Combo", CategoryId = 12, Price = 32.99m, Description = "Para quem vive na nuvem (literalmente)" },
            new Product { Id = 15, Name = "API Meal", CategoryId = 12, Price = 33.50m, Description = "Interface deliciosa entre fome e felicidade" },

            // Molhos: Subcategoria 32 a 36
            new Product { Id = 16, Name = "Barbecue++", CategoryId = 32, Price = 3.50m, Description = "Molho com sabor aprimorado" },
            new Product { Id = 17, Name = "Chipotle.js", CategoryId = 33, Price = 3.50m, Description = "Molho picante, mas reativo" },
            new Product { Id = 18, Name = "MayoAI", CategoryId = 34, Price = 3.00m, Description = "Maionese inteligente que aprende seu gosto" },
            new Product { Id = 19, Name = "KetchAPI", CategoryId = 32, Price = 2.90m, Description = "Molho básico com endpoints deliciosos" },
            new Product { Id = 20, Name = "Salsa Script", CategoryId = 35, Price = 4.00m, Description = "Molho com sabor dinâmico e animado" },
            new Product { Id = 21, Name = "Css Cream", CategoryId = 36, Price = 4.00m, Description = "Visualmente bonito e bem formatado" },

            // Sobremesas: Subcategoria 17 a 21
            new Product { Id = 22, Name = "MilkShake SQL", CategoryId = 18, Price = 10.00m, Description = "Bate tudo e retorna o melhor resultado" },
            new Product { Id = 23, Name = "Petit Gateau da Nuvem", CategoryId = 17, Price = 12.90m, Description = "Sobremesa quente hospedada com amor" },
            new Product { Id = 24, Name = "Casquinha Bootstrap", CategoryId = 21, Price = 6.00m, Description = "Sempre responsiva e cremosa" },
            new Product { Id = 25, Name = "Cache Cookies", CategoryId = 19, Price = 7.50m, Description = "Doces armazenados para carregamento rápido" },
            new Product { Id = 26, Name = "Python Pie", CategoryId = 20, Price = 9.00m, Description = "Fácil de consumir e muito poderosa" },
            new Product { Id = 27, Name = "Cake++", CategoryId = 20, Price = 10.50m, Description = "Bolo orientado a delícias" },

            // Bebidas: Subcategoria 22 a 26
            new Product { Id = 28, Name = "ColaScript", CategoryId = 22, Price = 5.50m, Description = "Refrigerante com sintaxe refrescante" },
            new Product { Id = 29, Name = "JavaJuice", CategoryId = 23, Price = 6.00m, Description = "Suco forte e robusto" },
            new Product { Id = 30, Name = "React Refresco", CategoryId = 24, Price = 6.50m, Description = "Refresca na hora e reage ao seu gosto" },
            new Product { Id = 31, Name = "SmoothieScript", CategoryId = 24, Price = 7.00m, Description = "Bebida funcional e saborosa" },
            new Product { Id = 32, Name = "Ruby Lemonade", CategoryId = 26, Price = 6.90m, Description = "Limonada elegante com sabor refinado" },
            new Product { Id = 33, Name = "Swift Sake", CategoryId = 25, Price = 8.00m, Description = "Bebida ágil e intensa" }
        );

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    User = "Admin",
                    Password = "123456", // <- senha "123456" com hash base64
                    Type = "admin"
                }
            );

            modelBuilder.Entity<ProductIngredient>()
                .HasKey(pi => new { pi.ProductId, pi.IngredientId });

            modelBuilder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductIngredients)
                .HasForeignKey(pi => pi.ProductId);

            modelBuilder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Ingredient)
                .WithMany(i => i.ProductIngredients)
                .HasForeignKey(pi => pi.IngredientId);

            // dotnet ef migrations add SeedProducts
            // dotnet ef database update

        }
    }
}


