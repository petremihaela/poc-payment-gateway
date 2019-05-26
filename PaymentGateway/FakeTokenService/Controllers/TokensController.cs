using FakeTokenService.Services;
using Microsoft.AspNetCore.Mvc;

namespace FakeTokenService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokensController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet("{token}", Name = nameof(ValidateToken))]
        public ActionResult<bool> ValidateToken(string token)
        {
            var isValid = _tokenService.IsValidToken(token);
            return isValid;
        }
    }
}