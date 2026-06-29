namespace LojaApi.Tests.Services;

public class ProdutoServiceTests
{
    // cria um DbContext em memória para cada teste — sem banco real
    private AppDbContext CriarContexto()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // banco novo por teste
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public void GetAll_DeveRetornarTodosProdutos()
    {
        // Arrange
        var context = CriarContexto();
        context.Produtos.AddRange(
            new Produto { Id = 1, Nome = "Notebook", Preco = 3500m, Marca = "Dell", SKU = "SKNOTE11909" },
            new Produto { Id = 2, Nome = "Mouse", Preco = 150m, Marca = "Logitech", SKU = "SKMOUSE1190119" }
        );
        context.SaveChanges();

        var service = new ProdutoService(context);

        // Act
        var resultado = service.GetAll();

        // Assert
        Assert.Equal(2, resultado.TotalItens);
        Assert.Equal(2, resultado.Itens.Count);
    }

    [Fact]
    public void GetAll_DevePaginarCorretamente()
    {
        // Arrange
        var context = CriarContexto();
        context.Produtos.AddRange(
            new Produto { Id = 1, Nome = "Notebook", Preco = 3500m, Marca = "Dell", SKU = "SKNOTE11909" },
            new Produto { Id = 2, Nome = "Mouse", Preco = 150m, Marca = "Logitech", SKU = "SKMOUSE1190119" },
            new Produto { Id = 3, Nome = "Teclado", Preco = 350m, Marca = "Logitech", SKU = "SKKEYBRD1122909" }
        );
        context.SaveChanges();

        var service = new ProdutoService(context);

        // Act
        var resultado = service.GetAll(pagina: 1, tamanhoPagina: 2);

        // Assert
        Assert.Equal(3, resultado.TotalItens);
        Assert.Equal(2, resultado.TotalPaginas);
        Assert.Equal(2, resultado.Itens.Count);
        Assert.Equal(1, resultado.Pagina);
    }

    [Fact]
    public void GetAll_SegundaPagina_DeveRetornarItemRestante()
    {
        // Arrange
        var context = CriarContexto();
        context.Produtos.AddRange(
            new Produto { Id = 1, Nome = "Notebook", Preco = 3500m, Marca = "Dell", SKU = "SKNOTE11909" },
            new Produto { Id = 2, Nome = "Mouse", Preco = 150m, Marca = "Logitech", SKU = "SKMOUSE1190119" },
            new Produto { Id = 3, Nome = "Teclado", Preco = 350m, Marca = "Logitech", SKU = "SKKEYBRD1122909" }
        );
        context.SaveChanges();

        var service = new ProdutoService(context);

        // Act
        var resultado = service.GetAll(pagina: 2, tamanhoPagina: 2);

        // Assert
        Assert.Equal(1, resultado.Itens.Count);
        Assert.Equal("Teclado", resultado.Itens[0].Nome);
    }

    [Fact]
    public void GetById_ProdutoExistente_DeveRetornarProduto()
    {
        // Arrange
        var context = CriarContexto();
        context.Produtos.Add(new Produto { Id = 1, Nome = "Notebook", Preco = 3500m, Marca = "Dell", SKU = "SKNOTE11909" });
        context.SaveChanges();

        var service = new ProdutoService(context);

        // Act
        var produto = service.GetById(1);

        // Assert
        Assert.NotNull(produto);
        Assert.Equal("Notebook", produto.Nome);
        Assert.Equal("SKNOTE11909", produto.SKU);
    }

    [Fact]
    public void GetById_ProdutoInexistente_DeveLancarExcecao()
    {
        // Arrange
        var context = CriarContexto();
        var service = new ProdutoService(context);

        // Act & Assert
        Assert.Throws<ProdutoNotFoundException>(() => service.GetById(999));
    }

    [Fact]
    public void Create_DeveSalvarERetornarProduto()
    {
        // Arrange
        var context = CriarContexto();
        var service = new ProdutoService(context);
        var novoProduto = new CriarProdutoDto
        {
            Nome = "Notebook",
            Preco = 3500m,
            SKU = "SKNOTE11909",
            Marca = "Dell"
        };

        // Act
        var criado = service.Create(novoProduto);

        // Assert
        Assert.NotNull(criado);
        Assert.Equal("Notebook", criado.Nome);
        Assert.Equal("SKNOTE11909", criado.SKU);
        Assert.Equal(1, context.Produtos.Count());
    }

    [Fact]
    public void Update_ProdutoExistente_DeveAtualizarDados()
    {
        // Arrange
        var context = CriarContexto();
        context.Produtos.Add(new Produto { Id = 1, Nome = "Notebook", Preco = 3500m, Marca = "Dell", SKU = "SKNOTE11909" });
        context.SaveChanges();

        var service = new ProdutoService(context);
        var produtoAtualizado = new AtualizarProdutoDto
        {
            Nome = "Notebook Gamer",
            Preco = 5000m,
            Marca = "Dell"
        };

        // Act
        var resultado = service.Update(1, produtoAtualizado);

        // Assert
        Assert.Equal("Notebook Gamer", resultado.Nome);
        Assert.Equal(5000m, resultado.Preco);
    }

    [Fact]
    public void Update_ProdutoInexistente_DeveLancarExcecao()
    {
        // Arrange
        var context = CriarContexto();
        var service = new ProdutoService(context);
        var produtoAtualizado = new AtualizarProdutoDto
        {
            Nome = "Notebook Gamer",
            Preco = 5000m,
            Marca = "Dell"
        };

        // Act & Assert
        Assert.Throws<ProdutoNotFoundException>(() => service.Update(999, produtoAtualizado));
    }

    [Fact]
    public void Delete_ProdutoExistente_DeveRemoverProduto()
    {
        // Arrange
        var context = CriarContexto();
        context.Produtos.Add(new Produto { Id = 1, Nome = "Notebook", Preco = 3500m, Marca = "Dell", SKU = "SKNOTE11909" });
        context.SaveChanges();

        var service = new ProdutoService(context);

        // Act
        service.Delete(1);

        // Assert
        Assert.Equal(0, context.Produtos.Count());
    }

    [Fact]
    public void Delete_ProdutoInexistente_DeveLancarExcecao()
    {
        // Arrange
        var context = CriarContexto();
        var service = new ProdutoService(context);

        // Act & Assert
        Assert.Throws<ProdutoNotFoundException>(() => service.Delete(999));
    }
}