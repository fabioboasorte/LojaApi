namespace LojaApi.Services;

public interface IAuthService
{
    string? Login(LoginRequest request);
}