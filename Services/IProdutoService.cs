namespace LojaApi.Services;

public interface IProdutoService
{
    List<Produto> GetAll();
    Produto GetById(int id);
    Produto Create(CriarProdutoDto dto);
    Produto Update(int id, AtualizarProdutoDto dto);
    PaginacaoDto<Produto> GetAll(int pagina = 1, int tamanhoPagina = 10);

    bool Delete(int id);
}