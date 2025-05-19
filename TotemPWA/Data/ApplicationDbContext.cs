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

                modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Burgers", ParentCategoryId = null },
                new Category { Id = 2, Name = "Combos", ParentCategoryId = null },
                new Category { Id = 3, Name = "Bebidas", ParentCategoryId = null },
                new Category { Id = 4, Name = "Acompanhamentos", ParentCategoryId = null },
                new Category { Id = 5, Name = "Molhos", ParentCategoryId = null },

                new Category { Id = 6, Name = "Artesanais", ParentCategoryId = 1 },
                new Category { Id = 7, Name = "Tradicionais", ParentCategoryId = 1 },

                new Category { Id = 8, Name = "Refrigerantes", ParentCategoryId = 3 },
                new Category { Id = 9, Name = "Sucos", ParentCategoryId = 3 },

                new Category { Id = 10, Name = "Batatas", ParentCategoryId = 4 },
                new Category { Id = 11, Name = "Onion Rings", ParentCategoryId = 4 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Cheeseburger", CategoryId = 6, Price = 15.99m, Description = "Pão, carne, queijo e molho especial" },
                new Product { Id = 2, Name = "Combo Cheeseburger", CategoryId = 2, Price = 25.99m, Description = "Cheeseburger + Batata + Refrigerante" },
                new Product { Id = 3, Name = "Refrigerante Lata", CategoryId = 8, Price = 6.00m, Description = "350ml" },
                new Product { Id = 4, Name = "Batata Frita", CategoryId = 10, Price = 8.00m, Description = "Porção média" },
                new Product { Id = 5, Name = "Molho Barbecue", CategoryId = 5, Price = 2.00m, Description = "Molho especial barbecue" },
                new Product { Id = 6, Name = "Cheeseburger 2", CategoryId = 7, Price = 15.99m, Description = "Pão, carne, queijo e molho especial" }
                
            );        
               
            // dotnet ef migrations add SeedProducts
            // dotnet ef database update

        }
    }
}


