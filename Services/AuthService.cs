using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using LojaApi.Models;

namespace LojaApi.Services;

public class AuthService : IAuthService
{
    // usuário fixo por enquanto — no Dia 8 vem do banco
    private const string UsuarioValido = "admin";
    private const string SenhaValida = "123456";
    private const string ChaveSecreta = "chave-super-secreta-minimo-32-caracteres!";

    public string? Login(LoginRequest request)
    {
        if (request.Usuario != UsuarioValido || request.Senha != SenhaValida)
            return null;

        return GerarToken(request.Usuario);
    }

    private string GerarToken(string usuario)
    {
        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ChaveSecreta));
        var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, usuario),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var token = new JwtSecurityToken(
            issuer: "LojaApi",
            audience: "LojaApi",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credenciais
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}