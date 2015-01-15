using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;
using BetfairAPING.Entities.Betting;
using BetfairAPING.Exceptions;
using MethodTimer;

namespace BetfairAPING
{
    public class BettingApi : BetfairApiNextGen
    {
        public BettingApi(string appKey, string subdomain = null, string sessionToken = null)
            : base(appKey, subdomain ?? "api", "betting", sessionToken)
        {
            
        }

        private async Task<T> SendRequest<T>(string operation, object payload = null, string sessionToken = null) where T : new()
        {
            return await SendRequest<T, BettingApiError>(operation, payload, sessionToken);
        }

        [Time]
        public async Task<List<CompetitionResult>> ListCompetitionsAsync(dynamic payload = null)
        {
            return await SendRequest<List<CompetitionResult>>("listCompetitions", payload: payload);
        }

        [Time]
        public async Task<List<CountryCodeResult>> ListCountriesAsync(dynamic payload = null)
        {
            return await SendRequest<List<CountryCodeResult>>("listCountries", payload: payload);
        }

        [Time]
        public async Task<CurrentOrderSummaryReport> ListCurrentOrdersAsync(dynamic payload = null)
        {
            return await SendRequest<CurrentOrderSummaryReport>("listCurrentOrders", payload: payload);
        }

        [Time]
        public async Task<ClearedOrderSummaryReport> ListClearedOrdersAsync(dynamic payload = null)
        {
            return await SendRequest<ClearedOrderSummaryReport>("listClearedOrders", payload: payload);
        }

        [Time]
        public async Task<List<EventResult>> ListEventsAsync(dynamic payload = null)
        {
            return await SendRequest<List<EventResult>>("listEvents", payload: payload);
        }

        [Time]
        public async Task<List<EventTypeResult>> ListEventTypesAsync(dynamic payload = null)
        {
            return await SendRequest<List<EventTypeResult>>("listEventTypes", payload: payload);
        }

        [Time]
        public async Task<List<MarketBook>> ListMarketBookAsync(dynamic payload = null)
        {
            return await SendRequest<List<MarketBook>>("listMarketBook", payload: payload);
        }

        [Time]
        public async Task<List<MarketCatalogue>> ListMarketCatalogueAsync(dynamic payload = null)
        {
            return await SendRequest<List<MarketCatalogue>>("listMarketCatalogue", payload: payload);
        }

        [Time]
        public async Task<List<MarketProfitAndLoss>> ListMarketProfitAndLossAsync(dynamic payload = null)
        {
            return await SendRequest<List<MarketProfitAndLoss>>("listMarketProfitAndLoss", payload: payload);
        }
    }
}