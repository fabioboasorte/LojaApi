namespace LojaApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>().HasData(
        new Categoria { Id = 1, Nome = "Informática" },
        new Categoria { Id = 2, Nome = "Periféricos" },
        new Categoria { Id = 3, Nome = "Notebooks" }
    );

    modelBuilder.Entity<Produto>().HasData(
        new Produto { Id = 1, Nome = "Notebook", Preco = 3500m, SKU = "SKNOTE11909", Marca = "Dell" },
        new Produto { Id = 2, Nome = "Mouse", Preco = 150m, SKU = "SKMOUSE1190119", Marca = "Logitech" },
        new Produto { Id = 3, Nome = "Teclado", Preco = 350m, SKU = "SKKEYBRD1122909", Marca = "Logitech" }
    );

    // tabela intermediária ProdutoCategorias
    modelBuilder.Entity<Produto>()
        .HasMany(p => p.Categorias)
        .WithMany(c => c.Produtos)
        .UsingEntity(j => j.HasData(
            new { ProdutosId = 1, CategoriasId = 1 }, // Notebook -> Informática
            new { ProdutosId = 1, CategoriasId = 3 }, // Notebook -> Notebooks
            new { ProdutosId = 2, CategoriasId = 2 }, // Mouse -> Periféricos
            new { ProdutosId = 3, CategoriasId = 2 }  // Teclado -> Periféricos
        ));
    }
}