using System;
using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;
using BetfairAPING.Console.Options;
using CommandLine;

namespace BetfairAPING.Console
{
    class Program
    {
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
                    System.Console.WriteLine(e);
            }


            //var comps = bettingApi.ListCompetitionsAsync(
            //    new
            //    {
            //        filter = new MarketFilter
            //        {
            //            TurnInPlayEnabled = true
            //        }
            //    }).Result; 

            //var comps = bettingApi.ListCountriesAsync(
            //    new
            //    {
            //        filter = new MarketFilter
            //                 {
            //                     TurnInPlayEnabled = true
            //                 }
            //    }).Result;

            //var orders = bettingApi.ListCurrentOrdersAsync().Result;

            if (Debugger.IsAttached)
            {
                System.Console.WriteLine("Press any key...");
                System.Console.ReadKey();
            }
        }

        static async Task Process(string verb, object subOptions)
        {
            var password = GetPassword();
            var commonOptions = (CommonOptions)subOptions;

            var authenticationResponse = await Authenticator.Authenticate(commonOptions.Username, password, commonOptions.CertPath);
            var accountsApi = new AccountsApi(commonOptions.AppKey, sessionToken: authenticationResponse.SessionToken);
            var bettingApi = new BettingApi(commonOptions.AppKey, sessionToken: authenticationResponse.SessionToken);

            object result = null;
            switch (verb)
            {
                case "getaccountdetails":
                    result = await accountsApi.GetAccountDetailsAsync();
                    break;
                case "getaccountfunds":
                    result = await accountsApi.GetAccountFundsAsync();
                    break;
                case "getaccountstatement":
                    var cmdSubOptions = (AccountStatementSubOptions)subOptions;
                    result = await accountsApi.GetAccountStatementAsync(new { recordCount = cmdSubOptions.RecordCount, itemDateRange = TimeRange.Since(TimeSpan.FromDays(cmdSubOptions.FromDays)) });
                    break;
                case "listcurrencyrates":
                    result = await accountsApi.ListCurrencyRatesAsync();
                    break;
                default:
                    System.Console.WriteLine("Can't handle {0} API call", verb);
                    break;
            }

            if (result != null)
            {
                var rs = result as IEnumerable;
                if (rs != null)
                    foreach (var r in rs)
                        System.Console.WriteLine(r);
                else
                    System.Console.WriteLine(result);
            }
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
