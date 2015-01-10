﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using System.Threading.Tasks;
using BetfairAPING.Console.Options;
using BetfairAPING.Entities.Betting;
using CommandLine;
using CredentialManagement;

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
                    result = await accountsApi.GetAccountStatementAsync(
                        new
                        {
                            recordCount = cmdSubOptions.RecordCount,
                            itemDateRange = TimeRange.Since(TimeSpan.FromDays(cmdSubOptions.FromDays))
                        });
                    break;
                }
                case "listcurrencyrates":
                    result = await accountsApi.ListCurrencyRatesAsync();
                    break;

                #endregion

                case "listcompetitions":
                {
                    var cmdSubOptions = (ListCompetitionsSubOptions) subOptions;
                    result = await bettingApi.ListCompetitionsAsync(
                        new
                        {
                            filter = new MarketFilter
                            {
                                TextQuery = cmdSubOptions.TextQuery,
                                EventTypeIds = cmdSubOptions.EventTypeIds == null ? null : new HashSet<string>(cmdSubOptions.EventTypeIds.Split(','))
                            }
                        });
                    break;
                }
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
