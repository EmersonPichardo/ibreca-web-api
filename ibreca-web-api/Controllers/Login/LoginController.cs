using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ibreca_web_api.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<LoginApi.LoginResult>> Login(Credentials credentials)
        {
            return await LoginApi.Instance.Login(credentials.Email, credentials.Password);
        }

        [HttpGet(), ValidateUserToken]
        public ActionResult<bool> IsTokenValid()
        {
            return true;
        }
    }
}
