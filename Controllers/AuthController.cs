namespace LojaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var token = _authService.Login(request);
        if (token is null) return Unauthorized(new { mensagem = "Usuário ou senha inválidos" });
        return Ok(new { token });
    }
}