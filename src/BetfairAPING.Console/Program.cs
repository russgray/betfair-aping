using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using BetfairAPING.Console.Options;
using BetfairAPING.Entities.Betting;
using CommandLine;
using CredentialManagement;
using NLog;

namespace BetfairAPING.Console
{
    class Program
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            var options = new Options.Options();
            string invokedVerb = null;
            object invokedVerbInstance = null;
            if (!Parser.Default.ParseArguments(
                args, options,
                (verb, subOptions) =>
                {
                    invokedVerb = verb;
                    invokedVerbInstance = subOptions;
                }))
            Environment.Exit(Parser.DefaultExitCodeFail);

            try
            {
                Process(invokedVerb, invokedVerbInstance).Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                    _logger.Error(e);
            }

            if (Debugger.IsAttached)
            {
                System.Console.WriteLine("Press any key...");
                System.Console.ReadKey();
            }
        }

        static async Task Process(string verb, object subOptions)
        {
            var commonOptions = (CommonOptions)subOptions;
            string username, password;
            if (string.IsNullOrEmpty(commonOptions.CredentialStoreName))
            {
                username = commonOptions.Username;
                password = GetPassword();
            }
            else
            {
                var creds = GetCredentials(commonOptions.CredentialStoreName);
                username = creds.Username;
                password = creds.Password;
            }

            var authenticationResponse = await Authenticator.Authenticate(username, password, commonOptions.CertPath);
            var accountsApi = new AccountsApi(commonOptions.AppKey, sessionToken: authenticationResponse.SessionToken);
            var bettingApi = new BettingApi(commonOptions.AppKey, sessionToken: authenticationResponse.SessionToken);

            object result = null;
            switch (verb)
            {
                case "selftest":
                    // Run through a variety of readonly requests
                    PrintResult(await accountsApi.GetAccountDetailsAsync());
                    PrintResult(await accountsApi.GetAccountFundsAsync());
                    PrintResult(await accountsApi.GetAccountStatementAsync(itemDateRange: TimeRange.Since(TimeSpan.FromDays(30))));
                    PrintResult(await accountsApi.ListCurrencyRatesAsync());
                    PrintResult(await bettingApi.ListCompetitionsAsync(new {filter = new MarketFilter {TextQuery = "Boxing"}}));
                    PrintResult(await bettingApi.ListCountriesAsync(new {filter = new MarketFilter {TextQuery = "Boxing"}}));
                    PrintResult(await bettingApi.ListEventsAsync(new {filter = new MarketFilter {TextQuery = "Boxing"}}));
                    return;

                #region Accounts API

                case "getaccountdetails":
                    result = await accountsApi.GetAccountDetailsAsync();
                    break;
                case "getaccountfunds":
                    result = await accountsApi.GetAccountFundsAsync();
                    break;
                case "getaccountstatement":
                {
                    var cmdSubOptions = (AccountStatementSubOptions) subOptions;
                    result = await accountsApi.GetAccountStatementAsync(recordCount: cmdSubOptions.RecordCount, itemDateRange: TimeRange.Since(TimeSpan.FromDays(cmdSubOptions.FromDays)));
                    break;
                }
                case "listcurrencyrates":
                    result = await accountsApi.ListCurrencyRatesAsync();
                    break;

                #endregion

                case "listcompetitions":
                    result = await bettingApi.ListCompetitionsAsync(
                        new
                        {
                            filter = CreateMarketFilterFromOptions((MarketFilterSubOptions) subOptions)
                        });
                    break;
                case "listcountries":
                    result = await bettingApi.ListCountriesAsync(
                        new
                        {
                            filter = CreateMarketFilterFromOptions((MarketFilterSubOptions) subOptions)
                        });
                    break;
                case "listcurrentorders":
                {
                    var cmdSubOptions = (ListCurrentOrdersSubOptions) subOptions;
                    result = await bettingApi.ListCurrentOrdersAsync(
                        new
                        {
                            betIds = cmdSubOptions.BetIdsAsSet,
                            marketIds = cmdSubOptions.MarketIdsAsSet,
                        });
                    break;
                }
                case "listclearedorders":
                {
                    var cmdSubOptions = (ListClearedOrdersSubOptions) subOptions;
                    result = await bettingApi.ListClearedOrdersAsync(
                        new
                        {
                            betStatus = cmdSubOptions.BetStatus,
                            eventTypeIds = cmdSubOptions.EventTypeIdsAsSet,
                            eventIds = cmdSubOptions.EventIdsAsSet,
                            marketIds = cmdSubOptions.MarketIdsAsSet,
                            runnerIds = cmdSubOptions.RunnerIdsAsSet,
                            betIds = cmdSubOptions.BetIdsAsSet,
                        });
                    break;
                }
                case "listevents":
                    result = await bettingApi.ListEventsAsync(
                        new
                        {
                            filter = CreateMarketFilterFromOptions((MarketFilterSubOptions)subOptions),
                        });
                    break;
                case "listeventtypes":
                    result = await bettingApi.ListEventTypesAsync(
                        new
                        {
                            filter = CreateMarketFilterFromOptions((MarketFilterSubOptions) subOptions)
                        });
                    break;
                case "listmarketbook":
                {
                    var cmdSubOptions = (ListMarketBookSubOptions) subOptions;
                    result = await bettingApi.ListMarketBookAsync(
                        new
                        {
                            marketIds = cmdSubOptions.MarketIdsAsSet,
                            priceProjection = cmdSubOptions.PriceProjection,
                            orderProjection = cmdSubOptions.OrderProjection,
                            matchProjection = cmdSubOptions.MatchProjection,
                        });
                    break;
                }
                case "listmarketcatalogue":
                {
                    var cmdSubOptions = (ListMarketCatalogueSubOptions)subOptions;
                    result = await bettingApi.ListMarketCatalogueAsync(
                        new
                        {
                            filter = CreateMarketFilterFromOptions(cmdSubOptions),
                            marketProjection = cmdSubOptions.MarketProjectionAsSet,
                            sort = cmdSubOptions.MarketSort,
                            maxResults = cmdSubOptions.MaxResults,
                        });
                    break;
                }
                case "listmarketprofitandloss":
                {
                    var cmdSubOptions = (ListMarketProfitAndLossSubOptions)subOptions;
                    result = await bettingApi.ListMarketProfitAndLossAsync(
                        new
                        {
                            marketIds = cmdSubOptions.MarketIdsAsSet,
                        });
                    break;
                }
                default:
                    System.Console.WriteLine("Can't handle {0} API call", verb);
                    break;
            }

            PrintResult(result);
        }

        private static void PrintResult(object result)
        {
            if (result != null)
            {
                var rs = result as IEnumerable;
                if (rs != null)
                    foreach (var r in rs)
                        _logger.Info(r);
                else
                    _logger.Info(result);
            }
        }

        static MarketFilter CreateMarketFilterFromOptions(MarketFilterSubOptions options)
        {
            return new MarketFilter
            {
                TextQuery = options.TextQuery,
                EventTypeIds = ParseCommandLineList(options.EventTypeIds),
                EventIds = ParseCommandLineList(options.EventIds),
                CompetitionIds = ParseCommandLineList(options.CompetitionIds),
                MarketIds = ParseCommandLineList(options.MarketIds),
            };
        }

        static HashSet<string> ParseCommandLineList(string option)
        {
            return option == null ? null : new HashSet<string>(option.Split(','));
        }

        static UserPass GetCredentials(string credentialStoreName)
        {
            var cm = new Credential { Target = credentialStoreName };
            if (!cm.Exists())
                return null;

            cm.Load();
            var up = new UserPass(cm);
            return up;
        }

        static string GetPassword()
        {
            System.Console.Write("Enter password: ");
            var password = "";
            var info = System.Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    password += info.KeyChar;
                    info = System.Console.ReadKey(true);
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password.Substring
                            (0, password.Length - 1);
                    }
                    info = System.Console.ReadKey(true);
                }
            }

            System.Console.WriteLine(new string('*', password.Length));
            
            return password;
        }

        internal class UserPass
        {
            public UserPass(Credential cm)
            {
                Username = cm.Username;
                Password = cm.Password;
            }

            public string Username { get; private set; }
            public string Password { get; private set; }
        }
    }
}
