using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Json.JsonClasses;

using Newtonsoft.Json;

namespace Json
{
    internal static class Json
    {
        private static async Task Main()
        {
            var client = new HttpClient();
            var respons = await client.GetAsync("https://restcountries.eu/rest/v2/region/americas");
            var json = respons.Content.ReadAsStringAsync().Result;
            var countries = JsonConvert.DeserializeObject<Country[]>(json);

            var totalPopulation = countries.Sum(c => c.Population);
            Console.WriteLine($"Общая численность населения: {totalPopulation}");

            var currencies = countries.ToDictionary(c => c.Name, c => c.Currencies[0].Name).ToList();

            foreach (var (country, currency) in currencies)
            {
                Console.WriteLine($"У страны {country} валюта: {currency}");
            }
        }
    }
}