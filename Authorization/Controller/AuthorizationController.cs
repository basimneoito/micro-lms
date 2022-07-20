using System.Threading.Tasks;
using Authorization.Services;
using Common.Dtos.Authorization.Models;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Controller;
[ApiController]
[Route("api/[controller]")]
public class AuthorizationController : ControllerBase
{
    private IAuthService _service;
    public AuthorizationController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginUserRequest request) => Ok(await _service.LoginAsync(request));

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterRequest request) => Ok(await _service.RegisterAsync(request));

    
}