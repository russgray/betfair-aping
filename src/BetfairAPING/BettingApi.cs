﻿using System.Collections.Generic;
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

        public async Task<ClearedOrderSummaryReport> ListClearedOrdersAsync(dynamic payload = null)
        {
            return await SendRequest<ClearedOrderSummaryReport>("listClearedOrders", payload: payload);
        }

        public async Task<List<EventResult>> ListEventsAsync(dynamic payload = null)
        {
            return await SendRequest<List<EventResult>>("listEvents", payload: payload);
        }

        public async Task<List<EventTypeResult>> ListEventTypesAsync(dynamic payload = null)
        {
            return await SendRequest<List<EventTypeResult>>("listEventTypes", payload: payload);
        }

        public async Task<List<MarketBook>> ListMarketBookAsync(dynamic payload = null)
        {
            return await SendRequest<List<MarketBook>>("listMarketBook", payload: payload);
        }

        public async Task<List<MarketCatalogue>> ListMarketCatalogueAsync(dynamic payload = null)
        {
            return await SendRequest<List<MarketCatalogue>>("listMarketCatalogue", payload: payload);
        }
    }
}