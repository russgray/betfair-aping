using System.Collections.Generic;
using System.Threading.Tasks;
using BetfairAPING.Entities.Accounts;
using BetfairAPING.Exceptions;
using MethodTimer;

namespace BetfairAPING
{
    public class AccountsApi : BetfairApiNextGen
    {
        public AccountsApi(string appKey, string subdomain = null, string sessionToken = null)
            : base(appKey, subdomain ?? "api", "account", sessionToken)
        {
            
        }

        private async Task<T> SendRequest<T>(string operation, object payload = null, string sessionToken = null) where T : new()
        {
            return await SendRequest<T, AccountsApiError>(operation, payload, sessionToken);
        }

        [Time]
        public async Task<AccountDetails> GetAccountDetailsAsync()
        {
            return await SendRequest<AccountDetails>("getAccountDetails");
        }

        [Time]
        public async Task<AccountFunds> GetAccountFundsAsync()
        {
            return await SendRequest<AccountFunds>("getAccountFunds");
        }

        [Time]
        public async Task<AccountStatementReport> GetAccountStatementAsync(string locale = null, int? fromRecord = null, int? recordCount = null, TimeRange itemDateRange = null, string includeItem = null, string wallet = null)
        {
            return await GetAccountStatementAsync(
                new
                {
                    locale,
                    fromRecord,
                    recordCount, 
                    itemDateRange,
                    includeItem,
                    wallet
                });
        }

        protected async Task<AccountStatementReport> GetAccountStatementAsync(dynamic payload = null)
        {
            /*
             * Sample request:
             * 
             * [{
             *   "method": "AccountAPING/v1.0/getAccountStatement", 
             *   "params": {
             *     "locale":"en",
             *     "recordCount":"5",
             *     "itemDateRange":{"from":"2014-12-01T00:00:00Z","to":"2014-12-22T00:00:00Z"},
             *     "includeItem":"ALL"
             *   }, 
             *   "id": 1
             * }]
             * 
             */
            return await SendRequest<AccountStatementReport>("getAccountStatement", payload: payload);
        }

        [Time]
        public async Task<List<CurrencyRate>> ListCurrencyRatesAsync(dynamic payload = null)
        {
            return await SendRequest<List<CurrencyRate>>("listCurrencyRates", payload: payload);
        }

        [Time]
        public async Task<TransferResponse> TransferFundsAsync(dynamic payload = null)
        {
            return await SendRequest<TransferResponse>("transferFunds", payload: payload);
        }
    }
}