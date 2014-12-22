using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using BetfairAPING.Entities;
using RestSharp;

namespace BetfairAPING
{
    public static class Authenticator
    {
        private static readonly RestClient _client = new RestClient("https://identitysso.betfair.com/api/certlogin");

        public static async Task<AuthenticationResponse> Authenticate(string username, string password, string pathToCert)
        {
            var req = new RestRequest(Method.POST);

            req.AddHeader("X-Application", "betfair-aping");
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            req.AddParameter("username", username);
            req.AddParameter("password", password);

            var cert = new X509Certificate2(pathToCert);
            _client.ClientCertificates = new X509Certificate2Collection(cert);

            var resp = await _client.ExecutePostTaskAsync<AuthenticationResponse>(req);
            return resp.Data;
        }
    }
}
