using System;
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

    public class CurrentOrderSummaryReport
    {
        List<CurrentOrderSummary> CurrentOrders { get; set; }
        bool MoreAvailable { get; set; }
    }
    public class CurrentOrderSummary
    {
        public string BetId { get; set; }
        public string MarketId { get; set; }
        public long SelectionId { get; set; }
        public double Handicap { get; set; }
        public PriceSize PriceSize { get; set; }
        public double BspLiability { get; set; }
        public string Side { get; set; }
        public string Status { get; set; }
        public string PersistenceType { get; set; }
        public string OrderType { get; set; }
        public DateTime PlacedDate { get; set; }
        public DateTime MatchedDate { get; set; }
        public double AveragePriceMatched { get; set; }
        public double SizeMatched { get; set; }
        public double SizeRemaining { get; set; }
        public double SizeLapsed { get; set; }
        public double SizeCancelled { get; set; }
        public double SizeVoided { get; set; }
        public string RegulatorAuthCode { get; set; }
        public string RegulatorCode { get; set; }

    }

    public class PriceSize
    {
        public double Price { get; set; }
        public double Size { get; set; }
    }
}