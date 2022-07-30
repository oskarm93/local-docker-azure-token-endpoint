using System.IdentityModel.Tokens.Jwt;
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
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(tokenReply.Token);
            return new
            {
                access_token = tokenReply.Token,
                refresh_token = string.Empty,
                expires_in = ((int)(jwt.ValidTo - jwt.ValidFrom).TotalSeconds).ToString(),
                expires_on = jwt.Claims.Single(e => e.Type == "exp").Value,
                not_before = jwt.Claims.Single(e => e.Type == "nbf").Value,
                resource = resource,
                token_type = "Bearer"
            };
        }
    }
}