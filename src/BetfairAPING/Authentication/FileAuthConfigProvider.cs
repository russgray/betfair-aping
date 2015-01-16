using System;
using System.IO;
using Newtonsoft.Json;

namespace BetfairAPING.Authentication
{
    internal class FileAuthConfigProvider : IAuthConfigProvider
    {
        public LoginCredentials ReadConfig()
        {
            var homeConfig = Path.Combine(Environment.ExpandEnvironmentVariables("%HOME%"), "bf.json");

            LoginCredentials json = null;
            // Look for config file in current dir first
            if (File.Exists("bf.json"))
                json = JsonConvert.DeserializeObject<LoginCredentials>(File.ReadAllText("bf.json"));
            else if (File.Exists(homeConfig))
                json = JsonConvert.DeserializeObject<LoginCredentials>(File.ReadAllText(homeConfig));

            return json;
        }
    }
}