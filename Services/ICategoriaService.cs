namespace LojaApi.Services;

public interface ICategoriaService
{
    List<Categoria> GetAll();
    Categoria GetById(int id);
    Categoria Create(Categoria Categoria);
    Categoria Update(int id, AtualizarCategoriaDto dto);
    bool Delete(int id);
}