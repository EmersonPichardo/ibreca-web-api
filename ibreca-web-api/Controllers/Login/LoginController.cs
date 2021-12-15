using ibreca_web_api.SettingsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace ibreca_web_api.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SecurityApiConfiguration securityApiConfiguration;

        public LoginController(IOptions<SecurityApiConfiguration> securityApiConfiguration)
        {
            this.securityApiConfiguration = securityApiConfiguration.Value;
        }

        [HttpPost]
        public async Task<ActionResult<LoginApi.LoginResult>> Login(Credentials credentials)
        {
            return await LoginApi.Instance.Login(credentials.Email, credentials.Password);
        }
    }
}
