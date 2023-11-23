using AirAstana.API.Core.Contracts;
using AirAstana.API.Core.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace AirAstana.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAuthManager _authManager;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IAuthManager authManager, ILogger<AccountController> logger)
    {
        _authManager = authManager;
        _logger = logger;
    }

    // POST: api/account/login
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var authResponse = await _authManager.Login(loginDto);
        if (authResponse == null)
        {
            return Unauthorized();
        }

        return Ok(authResponse);
    }
}