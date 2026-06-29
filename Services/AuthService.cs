namespace LojaApi.Services;

public class AuthService : IAuthService
{
    private const string UsuarioValido = "admin";
    private const string SenhaValida = "123456";
    private readonly IConfiguration _config;

    public AuthService(IConfiguration config)
    {
        _config = config;
    }

    public string? Login(LoginRequest request)
    {
        if (request.Usuario != UsuarioValido || request.Senha != SenhaValida)
            return null;

        return GerarToken(request.Usuario);
    }

    private string GerarToken(string usuario)
    {
        var chave = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:ChaveSecreta"]!));
        var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, usuario),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credenciais
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}