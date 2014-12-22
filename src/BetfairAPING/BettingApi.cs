using System.Threading.Tasks;

namespace BetfairAPING
{
    public class BettingApi : BetfairApiNextGen
    {
        public BettingApi(string appKey, string subdomain = null, string sessionToken = null)
            : base(appKey, subdomain ?? "api", "betting", sessionToken)
        {
            
        }

        public async Task<CompetitionResult> ListCompetitionsAsync(dynamic payload = null)
        {
            return await SendRequest<CompetitionResult>("listCompetitions", payload: payload);
        }
    }

    public class CompetitionResult
    {
    }
}