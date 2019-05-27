using FakeTokenService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FakeTokenService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        private readonly IConfiguration _configuration;

        public TokensController(ITokenService tokenService, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<string> GetToken()
        {
            var token = _configuration.GetSection("FakeToken");
            return Ok(token.Value);
        }

        [HttpGet("{token}", Name = nameof(ValidateToken))]
        public ActionResult<bool> ValidateToken(string token)
        {
            var isValid = _tokenService.IsValidToken(token);
            return isValid;
        }
    }
}