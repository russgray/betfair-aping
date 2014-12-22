using System;
using BetfairAPING.Entities.Betting;
using CommandLine;

namespace BetfairAPING.Console
{
    class Options
    {
        [Option("username", HelpText = "Betfair username")]
        public string Username { get; set; }

        [Option("password", HelpText = "Betfair password")]
        public string Password { get; set; }

        [Option("cert", HelpText = "Path to Betfair API certificate")]
        public string CertPath { get; set; }

        [Option("app-key", HelpText = "Betfair app key")]
        public string AppKey { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();

            if (!Parser.Default.ParseArguments(args, options))
                return;

            var password = options.Password ?? GetPassword();

            var t = Authenticator.Authenticate(options.Username, password, options.CertPath).Result;

            var accountsApi = new AccountsApi(options.AppKey, sessionToken: t.SessionToken);
            //var details = accountsApi.GetAccountDetailsAsync().Result;
            //var funds = accountsApi.GetAccountFundsAsync().Result;
            //var stmt = accountsApi.GetAccountStatementAsync(new { recordCount = 5, itemDateRange = TimeRange.Since(TimeSpan.FromDays(365)) }).Result;

            //var currencies = accountsApi.ListCurrencyRatesAsync().Result;

            var bettingApi = new BettingApi(options.AppKey, sessionToken: t.SessionToken);
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

            var orders = bettingApi.ListCurrentOrdersAsync().Result;
            System.Console.WriteLine("Press any key...");
            System.Console.ReadKey();
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
