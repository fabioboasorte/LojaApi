namespace LojaApi.Services;

public class CategoriaService : ICategoriaService
{
    private readonly AppDbContext _context;

    public CategoriaService(AppDbContext context)
    {
        _context = context;
    }

    public List<Categoria> GetAll() =>
        _context.Categorias
            .ToList();

    public Categoria GetById(int id) =>
        _context.Categorias
            .FirstOrDefault(p => p.Id == id)
            ?? throw new ProdutoNotFoundException(id);

    public Categoria Create(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        _context.SaveChanges();
        return categoria;
    }

    public Categoria Update(int id, AtualizarCategoriaDto dto)
    {
        var categoria = _context.Categorias
            .FirstOrDefault(p => p.Id == id)
            ?? throw new ProdutoNotFoundException(id);

        categoria.Nome = categoria.Nome;

        _context.SaveChanges();
        return categoria;
    }

    public bool Delete(int id)
    {
        var categoria = GetById(id);
        _context.Categorias.Remove(categoria);
        _context.SaveChanges();
        return true;
    }
}