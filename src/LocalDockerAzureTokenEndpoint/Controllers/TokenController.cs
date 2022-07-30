using Azure.Core;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LocalDockerAzureTokenEndpoint.Controllers
{
    [ApiController]
    [Route("/metadata/identity/oauth2/token")]
    public class TokenController : ControllerBase
    {
        [HttpGet]
        public async Task<dynamic> GetAsync()
        {
            var resource = HttpContext.Request.Query["resource"].Single();
            var credential = new AzureCliCredential();
            var tokenReply = await credential.GetTokenAsync(new TokenRequestContext(new string[] { resource }));
            return new
            {
                access_token = tokenReply.Token,
                refresh_token = string.Empty,
                expires_in = (tokenReply.ExpiresOn - DateTimeOffset.UtcNow).TotalSeconds,
                expires_on = tokenReply.ExpiresOn.ToUnixTimeSeconds(),
                not_before = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                resource = resource,
                token_type = "Bearer"
            };
        }
    }
}