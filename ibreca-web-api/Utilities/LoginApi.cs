using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ibreca_web_api
{
    public sealed class LoginApi
    {
        private static readonly Lazy<LoginApi> lazy = new Lazy<LoginApi>(() => new LoginApi());
        private static readonly HttpClient httpClient = new HttpClient();
        private static IConfiguration appSetting;
        private static readonly string contentType = "application/json";
        private static string applicationToken;

        public static LoginApi Instance { get { return lazy.Value; } }

        private LoginApi()
        {
            appSetting =
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false, true)
                    .Build();

            bool isDeveploment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            httpClient.BaseAddress = new Uri(appSetting.GetValue<string>($"SecurityApi:BaseUrl{(isDeveploment ? "-dev" : "")}"));
            applicationToken = appSetting.GetValue<string>($"SecurityApi:ApiToken{(isDeveploment ? "-dev" : "")}");
        }

        public async Task<ActionResult<LoginResult>> Login(string email, string password)
        {
            return await Send<LoginResult>(Method.post, "login", new { email, password });
        }

        private async Task<ActionResult<T>> Send<T>(Method method, string url, object content = null)
        {
            try
            {
                HttpMethod httpMethod = new HttpMethod(method.ToString());
                HttpRequestMessage request = new HttpRequestMessage(httpMethod, url);
                request.Headers.Add("ApplicationToken", applicationToken);

                if (content != null)
                {
                    request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, contentType);
                }

                HttpResponseMessage response = await httpClient.SendAsync(request);
                string json = await response.Content.ReadAsStringAsync();

                return new ContentResult()
                {
                    StatusCode = (int)response.StatusCode,
                    Content = json,
                    ContentType = contentType
                };
            }
            catch (Exception exception)
            {
                return new ObjectResult(exception);
            }
        }

        public class LoginResult
        {
            public string Token { get; set; }
            public string Name { get; set; }
        }

        private enum Method
        {
            get,
            post
        }
    }
}
