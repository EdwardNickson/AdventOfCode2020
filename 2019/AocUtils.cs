using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace _2019
{
    public static class AocUtils
    {
        private static HttpClient client;
        public static async Task<string> LoadInputAsync(int day, int year)
        {
            var sessionCookie = File.ReadAllText(@"..\..\..\..\SessionCookie.txt");

            var inputFile = $@"..\..\..\Inputs\day{day}.txt";
            if (File.Exists(inputFile))
            {
                return File.ReadAllText(inputFile);
            }
            else
            {
                if (client == null)
                {
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Cookie", $"session={sessionCookie}");
                }
                var url = $"https://adventofcode.com/{year}/day/{day}/input";
                var input = await client.GetStringAsync(url);
                File.WriteAllText(inputFile, input.Trim());
                return input.Trim();
            }
        }
    }
}
