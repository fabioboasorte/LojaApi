namespace LojaApi.Models.DTOs;

public class CriarProdutoDto
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [MinLength(3, ErrorMessage = "Nome deve ter no mínimo 3 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Preço é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser maior que zero")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "Marca é obrigatória")]
    [MinLength(3, ErrorMessage = "Marca deve ter no mínimo 3 caracteres")]
    public string Marca { get; set; } = string.Empty;

    [Required(ErrorMessage = "SKU é obrigatória")]
    [MinLength(3, ErrorMessage = "SKU deve ter no mínimo 3 caracteres")]
    public string SKU { get; set; } = string.Empty;

    public List<int> CategoriaIds { get; set; } = new();
}