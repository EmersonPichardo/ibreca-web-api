using ibreca_web_api.SettingsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Text;
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
        public async Task<ActionResult<UserData>> Login(Credentials credentials)
        {
            HttpClient client = new HttpClient();

            try
            {
                StringContent postData = new StringContent(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(securityApiConfiguration.GetEndPoint(SecurityApiConfiguration.EndpointName.Login), postData);
                string json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return Ok(JsonConvert.DeserializeObject<UserData>(json));
                }
                else if(response.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound(json);
                }
                else
                {
                    return Problem(json);
                }
            }
            catch (Exception exception)
            {
                return Problem(title: exception.Message, detail: exception.StackTrace);
            }
            finally
            {
                client.Dispose();
            }
        }
    }
}
