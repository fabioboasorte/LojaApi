namespace LojaApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _service;

    // injeção de dependência pelo construtor, igual @Autowired no Spring
    public CategoriasController(ICategoriaService service)
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
    public IActionResult Create(Categoria categoria)
    {
        var criado = _service.Create(categoria);
        return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, AtualizarCategoriaDto dto) =>
        Ok(_service.Update(id, dto));

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return NoContent();
    }
}