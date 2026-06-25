namespace LojaApi.Exceptions;

public class ProdutoNotFoundException : Exception
{
    public ProdutoNotFoundException(int id)
        : base($"Produto com id {id} não encontrado") { }
}