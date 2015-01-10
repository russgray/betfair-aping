using System.Net;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;

namespace BetfairAPING
{
    public abstract class BetfairApiNextGen
    {
        private readonly string _appKey;
        protected readonly string _subdomain;
        protected readonly string _sessionToken;
        protected readonly RestClient _client;
        protected readonly string _resourceRoot;
        private readonly CamelCaseJsonSerializer _serializer;

        protected BetfairApiNextGen(string appKey, string subdomain, string apiType, string sessionToken = null)
        {
            _appKey = appKey;
            _subdomain = subdomain;
            _sessionToken = sessionToken;
            _resourceRoot = string.Format("exchange/{0}/rest/v1.0", apiType);
            _client = new RestClient(string.Format("https://{0}.betfair.com", _subdomain));
            _serializer = new CamelCaseJsonSerializer();
        }

        protected async Task<T> SendRequest<T>(string operation, dynamic payload = null, string sessionToken = null) where T: new()
        {
            var req =  new RestRequest(string.Format("{0}/{1}/", _resourceRoot, operation), Method.POST)
            {
                JsonSerializer = _serializer
            };
            SetHeaders(req, sessionToken);
            if (payload != null)
                req.AddJsonBody(payload);

            var resp = await _client.ExecutePostTaskAsync<T>(req);

            if (resp.StatusCode != HttpStatusCode.OK)
            {
                // Attempt deserialisation again, this time as an error object
                var js = new JsonDeserializer();
                var error = js.Deserialize<BetfairApiException.ApiError>(resp);
                throw new BetfairApiException(error);
            }

            return resp.Data;
        }

        private void SetHeaders(IRestRequest request, string sessionToken)
        {
            request.AddHeader("X-Authentication", sessionToken ?? _sessionToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-Application", _appKey);
        }
    }
}