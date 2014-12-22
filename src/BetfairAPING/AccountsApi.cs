using System.Collections.Generic;
using System.Threading.Tasks;
using BetfairAPING.Entities;

namespace BetfairAPING
{
    public class AccountsApi : BetfairApiNextGen
    {
        public AccountsApi(string appKey, string subdomain = null, string sessionToken = null)
            : base(appKey, subdomain ?? "api", "account", sessionToken)
        {
            
        }

        public async Task<AccountDetails> GetAccountDetailsAsync()
        {
            return await SendRequest<AccountDetails>("getAccountDetails");
        }

        public async Task<AccountFunds> GetAccountFundsAsync()
        {
            return await SendRequest<AccountFunds>("getAccountFunds");
        }

        public async Task<AccountStatementReport> GetAccountStatementAsync(dynamic payload = null)
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

        public async Task<List<CurrencyRate>> ListCurrencyRatesAsync(dynamic payload = null)
        {
            return await SendRequest<List<CurrencyRate>>("listCurrencyRates", payload: payload);
        }

        public async Task<TransferResponse> TransferFundsAsync(dynamic payload = null)
        {
            return await SendRequest<TransferResponse>("transferFunds", payload: payload);
        }
    }
}