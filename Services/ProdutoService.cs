namespace LojaApi.Services;

public class ProdutoService : IProdutoService
{
    private readonly AppDbContext _context;
    private readonly ILogger<ProdutoService> _logger;

    public ProdutoService(AppDbContext context, ILogger<ProdutoService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public List<Produto> GetAll() =>
        _context.Produtos
            .Include(p => p.Categorias)
            .ToList();

    public Produto GetById(int id) =>
        _context.Produtos
            .Include(p => p.Categorias)
            .FirstOrDefault(p => p.Id == id)
            ?? throw new ProdutoNotFoundException(id);

    public Produto Create(CriarProdutoDto dto)
    {
        var produto = new Produto
        {
            Nome = dto.Nome,
            Preco = dto.Preco,
            Marca = dto.Marca,
            SKU = dto.SKU,
            Categorias = _context.Categorias
                .Where(c => dto.CategoriaIds.Contains(c.Id))
                .ToList()
        };

        _context.Produtos.Add(produto);
        _context.SaveChanges();
        _logger.LogInformation("Produto criado: {Id} - {Nome}", produto.Id, produto.Nome);
        return produto;
    }

    public Produto Update(int id, AtualizarProdutoDto dto) // atualizado
    {
        var produto = _context.Produtos
            .Include(p => p.Categorias)
            .FirstOrDefault(p => p.Id == id)
            ?? throw new ProdutoNotFoundException(id);

        produto.Nome = dto.Nome;
        produto.Preco = dto.Preco;
        produto.Marca = dto.Marca;
        produto.SKU = dto.SKU;

        produto.Categorias = _context.Categorias
            .Where(c => dto.CategoriaIds.Contains(c.Id))
            .ToList();

        _context.SaveChanges();
        return produto;
    }

    public PaginacaoDto<Produto> GetAll(int pagina = 1, int tamanhoPagina = 10)
    {
        var total = _context.Produtos.Count();
        var itens = _context.Produtos
            .Include(p => p.Categorias)
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToList();

        return new PaginacaoDto<Produto>
        {
            Pagina = pagina,
            TotalPaginas = (int)Math.Ceiling(total / (double)tamanhoPagina),
            TotalItens = total,
            Itens = itens
        };
    }

    public bool Delete(int id)
    {
        var produto = GetById(id);
        _context.Produtos.Remove(produto);
        _context.SaveChanges();
        return true;
    }
}