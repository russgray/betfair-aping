using System;
using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;
using BetfairAPING.Console.Options;
using BetfairAPING.Entities.Betting;
using CommandLine;
using Newtonsoft.Json;
using NLog;
using NLog.Fluent;

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
            var valid = Parser.Default.ParseArguments(
                args, options,
                (verb, subOptions) =>
                {
                    invokedVerb = verb;
                    invokedVerbInstance = subOptions;
                });
            if (!valid)
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

            if (invokedVerbInstance is CommonOptions && ((CommonOptions)invokedVerbInstance).PerfReport)
                System.Console.Write(MethodTimeLogger.HumanReport());

            if (Debugger.IsAttached)
            {
                System.Console.WriteLine("Press any key...");
                System.Console.ReadKey();
            }
        }

        static async Task Process(string verb, object subOptions)
        {
            var commonOptions = (CommonOptions)subOptions;
            ReconfigureLoggers(commonOptions.Verbose ? LogLevel.Trace : LogLevel.Info);

            var authenticator = new Authenticator();
            var credentials = authenticator.ResolveCredentials(
                commonOptions.Username,
                commonOptions.CredentialStoreName,
                commonOptions.CertPath,
                commonOptions.AppKey);

            if (string.IsNullOrEmpty(credentials.Password))
                credentials.Password = GetPassword();

            var authenticationResponse = await authenticator.Authenticate(credentials);
            var accountsApi = new AccountsApi(credentials.AppKey, sessionToken: authenticationResponse.SessionToken);
            var bettingApi = new BettingApi(credentials.AppKey, sessionToken: authenticationResponse.SessionToken);

            object result = null;
            switch (verb)
            {
                case "selftest":
                    // Run through a variety of readonly requests
                    PrintResult(await accountsApi.GetAccountDetailsAsync(), commonOptions.JsonOutput);
                    PrintResult(await accountsApi.GetAccountFundsAsync(), commonOptions.JsonOutput);
                    PrintResult(await accountsApi.GetAccountStatementAsync(itemDateRange: TimeRange.Since(TimeSpan.FromDays(30))), commonOptions.JsonOutput);
                    PrintResult(await accountsApi.ListCurrencyRatesAsync(), commonOptions.JsonOutput);
                    PrintResult(await bettingApi.ListCompetitionsAsync(new { filter = new MarketFilter { TextQuery = "Boxing" } }), commonOptions.JsonOutput);
                    PrintResult(await bettingApi.ListCountriesAsync(new { filter = new MarketFilter { TextQuery = "Boxing" } }), commonOptions.JsonOutput);
                    PrintResult(await bettingApi.ListEventsAsync(new { filter = new MarketFilter { TextQuery = "Boxing" } }), commonOptions.JsonOutput);
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

                #region Betting API

                case "listcompetitions":
                    result = await bettingApi.ListCompetitionsAsync(
                        new
                        {
                            filter = ((MarketFilterSubOptions) subOptions).ToMarketFilter()
                        });
                    break;
                case "listcountries":
                    result = await bettingApi.ListCountriesAsync(
                        new
                        {
                            filter = ((MarketFilterSubOptions)subOptions).ToMarketFilter()
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
                            orderProjection = cmdSubOptions.OrderProjection,
                            fromRecord = cmdSubOptions.FromRecord,
                            recordCount = cmdSubOptions.RecordCount,
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
                            side = cmdSubOptions.Side,
                            groupBy = cmdSubOptions.GroupBy,
                            includeItemDescription = cmdSubOptions.IncludeItemDescription,
                            fromRecord = cmdSubOptions.FromRecord,
                            recordCount = cmdSubOptions.RecordCount,
                        });
                    break;
                }
                case "listevents":
                    result = await bettingApi.ListEventsAsync(
                        new
                        {
                            filter = ((MarketFilterSubOptions)subOptions).ToMarketFilter()
                        });
                    break;
                case "listeventtypes":
                    result = await bettingApi.ListEventTypesAsync(
                        new
                        {
                            filter = ((MarketFilterSubOptions)subOptions).ToMarketFilter()
                        });
                    break;
                case "listmarketbook":
                {
                    var cmdSubOptions = (ListMarketBookSubOptions) subOptions;
                    result = await bettingApi.ListMarketBookAsync(
                        new
                        {
                            marketIds = cmdSubOptions.MarketIdsAsSet,
                            //priceProjection = cmdSubOptions.PriceProjection,
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
                            filter = cmdSubOptions.ToMarketFilter(),
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

                #endregion

                default:
                    System.Console.WriteLine("Can't handle {0} API call", verb);
                    break;
            }

            PrintResult(result, commonOptions.JsonOutput);
        }

        private static void PrintResult(object result, bool json)
        {
            if (result != null)
            {
                var rs = result as IEnumerable;
                if (rs != null)
                {
                    foreach (var r in rs)
                    {
                        var s = json ? JsonConvert.SerializeObject(r, Formatting.Indented) : r.ToString();
                        _logger.Info(s);
                    }
                }
                else
                {
                    var s = json ? JsonConvert.SerializeObject(result, Formatting.Indented) : result.ToString();
                    _logger.Info(s);
                }
            }
        }

        static void ReconfigureLoggers(LogLevel level)
        {
            foreach (var rule in LogManager.Configuration.LoggingRules)
            {
                rule.EnableLoggingForLevel(level);
            }

            LogManager.ReconfigExistingLoggers();
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
    }
}
