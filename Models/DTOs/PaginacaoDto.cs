namespace LojaApi.Models.DTOs;

public class PaginacaoDto<T>
{
    public int Pagina { get; set; }
    public int TotalPaginas { get; set; }
    public int TotalItens { get; set; }
    public List<T> Itens { get; set; } = new();
}