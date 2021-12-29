using Microsoft.AspNetCore.Mvc;

namespace ibreca_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController, ValidateUserToken]
    public class AuthControllerBase : ControllerBase { }
}
