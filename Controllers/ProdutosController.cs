namespace LojaApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _service;

    // injeção de dependência pelo construtor, igual @Autowired no Spring
    public ProdutosController(IProdutoService service)
    {
        _service = service;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetAll() => Ok(_service.GetAll());

    [HttpGet("{id}")]
    [AllowAnonymous]
    public IActionResult GetById(int id) => Ok(_service.GetById(id));


    [HttpPost]
    public IActionResult Create(CriarProdutoDto dto)
    {
        var criado = _service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, AtualizarProdutoDto dto) =>
        Ok(_service.Update(id, dto));

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return NoContent();
    }
}