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
                new Category { Id = 1, Name = "Lanches", ParentCategoryId = null },
                new Category { Id = 2, Name = "Combos", ParentCategoryId = null },
                new Category { Id = 3, Name = "Sobremesas", ParentCategoryId = null },
                new Category { Id = 4, Name = "Bebidas", ParentCategoryId = null },
                new Category { Id = 5, Name = "Extras", ParentCategoryId = null },
                new Category { Id = 6, Name = "Molhos", ParentCategoryId = null },

                new Category { Id = 7, Name = "C#", ParentCategoryId = 1 },
                new Category { Id = 8, Name = "Java", ParentCategoryId = 1 },
                new Category { Id = 9, Name = "Python", ParentCategoryId = 1 },
                new Category { Id = 10, Name = "JavaScript", ParentCategoryId = 1 },

                new Category { Id = 11, Name = "Refrigerantes", ParentCategoryId = 4 },
                new Category { Id = 12, Name = "Cafés", ParentCategoryId = 4 },

                new Category { Id = 13, Name = "Acompanhamentos", ParentCategoryId = 5 },
                new Category { Id = 14, Name = "Anéis de Loop", ParentCategoryId = 5 },

                new Category { Id = 15, Name = "Tipos de Molhos", ParentCategoryId = 6 },
                new Category { Id = 16, Name = "Molhos Picantes", ParentCategoryId = 15 },
                new Category { Id = 17, Name = "Molhos Doces", ParentCategoryId = 15 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "C# Burguer", CategoryId = 7, Price = 18.50m, Description = "Burguer orientado a objetos com queijo derretido" },
                new Product { Id = 2, Name = "Combo Stack Overflow", CategoryId = 2, Price = 32.99m, Description = "C# Burguer + Loop Fries + Byte-Cola" },
                new Product { Id = 3, Name = "JavaBean Espresso", CategoryId = 12, Price = 7.50m, Description = "Café forte com robusta implementação" },
                new Product { Id = 4, Name = "Loop Fries", CategoryId = 13, Price = 9.00m, Description = "Batatas em looping infinito" },
                new Product { Id = 5, Name = "NullPointer Molho", CategoryId = 6, Price = 3.00m, Description = "Só aparece quando você não espera" },
                new Product { Id = 6, Name = "Burguer 404", CategoryId = 8, Price = 19.99m, Description = "Sabor não encontrado, mas delicioso" },
                new Product { Id = 7, Name = "Snake.py Sanduíche", CategoryId = 9, Price = 17.00m, Description = "Sanduíche dinâmico e legível" },
                new Product { Id = 8, Name = "JS Double Shot", CategoryId = 10, Price = 21.00m, Description = "Burguer assíncrono com dois hambúrgueres" },
                new Product { Id = 9, Name = "Byte-Cola", CategoryId = 11, Price = 5.50m, Description = "Refrigerante com sabor binário" },
                new Product { Id = 10, Name = "Combo Dev Full Stack", CategoryId = 2, Price = 39.90m, Description = "JS Double Shot + Snake.py + Byte-Cola" },
                new Product { Id = 11, Name = "Exception Sundae", CategoryId = 3, Price = 12.00m, Description = "Sobremesa que quebra qualquer dieta" },
                new Product { Id = 12, Name = "Compiler Cheesecake", CategoryId = 3, Price = 10.50m, Description = "Doce processado com zero erros" },
                new Product { Id = 13, Name = "Latte Lógico", CategoryId = 12, Price = 6.00m, Description = "Café com operadores booleanos" },
                new Product { Id = 14, Name = "Bug Fries", CategoryId = 13, Price = 8.90m, Description = "Batatas fritas com comportamento inesperado" },
                new Product { Id = 15, Name = "Lambda Chicken", CategoryId = 7, Price = 16.99m, Description = "Frango funcional com sabor puro" },
                new Product { Id = 16, Name = "Spicy Try-Catch", CategoryId = 16, Price = 4.50m, Description = "Molho picante que trata qualquer exceção" },
                new Product { Id = 17, Name = "Sweet Loop", CategoryId = 17, Price = 4.00m, Description = "Molho doce com final em recursão infinita" }
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




   
               
            // dotnet ef migrations add SeedProducts
            // dotnet ef database update

        }
    }
}


