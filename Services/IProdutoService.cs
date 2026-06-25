namespace LojaApi.Services;

public interface IProdutoService
{
    List<Produto> GetAll();
    Produto GetById(int id);
    Produto Create(CriarProdutoDto dto);
    Produto Update(int id, AtualizarProdutoDto dto);
    bool Delete(int id);
}