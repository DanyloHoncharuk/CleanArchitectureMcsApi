namespace AuthService.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwt(string username);
    }
}
