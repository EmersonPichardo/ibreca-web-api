using System.Linq;

namespace ibreca_web_api.SettingsModels
{
    public class SecurityApiConfiguration
    {
        public string BaseUrl { get; set; }
        public Endpoint[] Endpoints { get; set; }

        public string GetEndPoint(EndpointName endpointName)
        {
            string a = BaseUrl + Endpoints.SingleOrDefault(endpoint => endpoint.Name == endpointName.ToString()).Url;
            return a;
        }

        public class Endpoint
        {
            public string Name { get; set; }
            public string Url { get; set; }
            public string Method { get; set; }
        }

        public enum EndpointName
        {
            Register_user,
            Get_user,
            Login,
            Validate_token
        }
    }
}
