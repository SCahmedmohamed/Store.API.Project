using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.About_Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServiceManager _serviceManager) : ControllerBase
    {

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var res = await _serviceManager.AuthService.LoginAsync(request);
            return Ok(res);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var res = await _serviceManager.AuthService.RegisterAsync(request);
            return Ok(res);
        }
    }
}
