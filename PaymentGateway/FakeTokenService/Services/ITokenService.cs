namespace FakeTokenService.Services
{
    public interface ITokenService
    {
        bool IsValidToken(string tokenId);
    }
}