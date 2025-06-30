using Microsoft.EntityFrameworkCore;
using TotemPWA.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
        // .AddJsonOptions(options =>
        //     {
        //         options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        //         options.JsonSerializerOptions.MaxDepth = 4;
        //     });
        
// Add SQL Server Connection 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
   // options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
      options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  // Adiciona o Swagger


// Aqui você registra o serviço de sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de vida da sessão
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    // Apaga o banco de dados completamente
    // context.Database.EnsureDeleted(); 

    // Aplica as migrações do zero
    context.Database.Migrate();       

    // Executa o Seed (inicialização de dados)
    // await DbInitializer.InitializeAsync(context);
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    // Aplica as migrações do zero
    context.Database.Migrate();       

    // Executa o Seed (inicialização de dados de categorias + cupons)
    await DbInitializer.InitializeAsync(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production 
    // scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();  // Habilita o Swagger
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TotemPWA API v1"));  // Interface do Swagger

app.UseHttpsRedirection();
app.UseRouting();

// Aqui você ativa o middleware
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

// Rota específica para Personalizar com slug
app.MapControllerRoute(
    name: "personalizar",
    pattern: "Home/Personalizar/{slug}",
    defaults: new { controller = "Home", action = "Personalizar" });

// Rota específica para Cardapio
app.MapControllerRoute(
    name: "cardapio",
    pattern: "Cardapio/{categorySlug?}/{subcategorySlug?}",
    defaults: new { controller = "Home", action = "Cardapio" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
)
.WithStaticAssets();




app.Run();
