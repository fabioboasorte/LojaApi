namespace LojaApi.Exceptions;

public class CategoriaNotFoundException : Exception
{
    public CategoriaNotFoundException(int id)
        : base($"Categoria com id {id} não encontrada") { }
}