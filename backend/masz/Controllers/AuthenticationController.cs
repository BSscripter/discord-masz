using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace masz.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> logger;

        public AuthenticationController(ILogger<AuthenticationController> logger)
        {
            this.logger = logger;
        }

        [HttpGet("/login")]
        public async Task<IActionResult> Login([FromQuery] string ReturnUrl)
        {
            if (ReturnUrl == null || ReturnUrl.Length == 0) {
                    ReturnUrl = $"/";
            }

            var properties = new AuthenticationProperties()
            {
                
                // actual redirect endpoint for your app
                RedirectUri = ReturnUrl,
                Items =
                {
                    { "LoginProvider", "Discord" },
                },
                AllowRefresh = true,
            };
            return this.Challenge(properties, "Discord");
        }
    }
}