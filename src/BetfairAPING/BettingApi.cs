using System.Collections.Generic;
using System.Threading.Tasks;
using BetfairAPING.Entities.Betting;

namespace BetfairAPING
{
    public class BettingApi : BetfairApiNextGen
    {
        public BettingApi(string appKey, string subdomain = null, string sessionToken = null)
            : base(appKey, subdomain ?? "api", "betting", sessionToken)
        {
            
        }

        public async Task<List<CompetitionResult>> ListCompetitionsAsync(dynamic payload = null)
        {
            return await SendRequest<List<CompetitionResult>>("listCompetitions", payload: payload);
        }

        public async Task<List<CountryCodeResult>> ListCountriesAsync(dynamic payload = null)
        {
            return await SendRequest<List<CountryCodeResult>>("listCountries", payload: payload);
        }

        public async Task<CurrentOrderSummaryReport> ListCurrentOrdersAsync(dynamic payload = null)
        {
            return await SendRequest<CurrentOrderSummaryReport>("listCurrentOrders", payload: payload);
        }
    }
}