using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Shared
{
    public  class CountryValidation
    {
        public static async Task<bool> Validate(string countryName)
        {
            string apiUrl =string.Format( "https://restcountries.eu/rest/v2/name/{0}?fullText=true",countryName);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);

                var httpResult = await client.GetAsync(apiUrl);

                if (httpResult.StatusCode == HttpStatusCode.NotFound)
                    return false;

                return true;
            }
        }
    }
}
