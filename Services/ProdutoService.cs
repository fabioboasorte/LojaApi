namespace LojaApi.Services;

public class ProdutoService : IProdutoService
{
    private readonly AppDbContext _context;

    public ProdutoService(AppDbContext context)
    {
        _context = context;
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

    public bool Delete(int id)
    {
        var produto = GetById(id);
        _context.Produtos.Remove(produto);
        _context.SaveChanges();
        return true;
    }
}