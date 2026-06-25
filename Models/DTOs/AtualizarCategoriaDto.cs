namespace LojaApi.Models.DTOs;

public class AtualizarCategoriaDto
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [MinLength(3, ErrorMessage = "Nome deve ter no mínimo 3 caracteres")]
    public string Nome { get; set; } = string.Empty;
}