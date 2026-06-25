namespace LojaApi.Models;

public class Categoria
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    [MinLength(3, ErrorMessage = "Nome deve ter no mínimo 3 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [JsonIgnore]
    public List<Produto> Produtos { get; set; } = new();
}