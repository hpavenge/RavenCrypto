using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RavenCoinApi
{
    public class DatabaseHelper
    {

        public static async Task<IList<Exchange>> GetExchanges()
        {
            string apiUrl = "https://api.coinigy.com/api/v1/exchanges";
            string apiKey = "f9bff044bfc56a488e14789a5b73d4c0";
            string apiSecret = "2a68747c8b36351e1644e060020db291";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
                client.DefaultRequestHeaders.Add("X-API-SECRET", apiSecret);
                HttpResponseMessage response = await client.PostAsync(apiUrl, null);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    JsonData rootResult = JsonConvert.DeserializeObject<JsonData>(result);
                    IList<Exchange> exchanges = rootResult.data;
                    foreach (Exchange exchange in exchanges)
                    {
                        string exch_code = exchange.exch_code;
                        if (exch_code != null)
                        {
                            await AddMarkets(exchange, exch_code);
                            // exchanges filled with market prices hier

                            Exchange atm = exchange;
                            // call markets https://api.coinigy.com/api/v1/markets -> needs code
                            // logica kijk welke markets crossen en exchanges erbij horen
                            //https://api.coinigy.com/api/v1/ticker needs code(GDAX) + market (BTC/USD)
                            Console.WriteLine("saved exchange" + exchange.exch_name);
                        }

                    }
                    return exchanges;
                }
            }
            return null;
        }
    }
}
