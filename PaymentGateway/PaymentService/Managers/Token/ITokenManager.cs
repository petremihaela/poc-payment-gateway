using System.Threading.Tasks;

namespace PaymentService.Managers.Token
{
    public interface ITokenManager
    {
        Task<bool> ValidateTokenAsync(string token);
    }
}