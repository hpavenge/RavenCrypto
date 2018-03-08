using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RavenCryptoConsole.Model;

namespace RavenCryptoConsole
{
    public static class RestHelper
    {
        static string apiUrl = "https://api.coinigy.com/api/v1/";
        static string apiKey = "f9bff044bfc56a488e14789a5b73d4c0";
        static string apiSecret = "2a68747c8b36351e1644e060020db291";
        static DatabaseHelper databaseHelper = new DatabaseHelper();

        public static async Task<List<Price>> DownloadPrices(string exchCode, string exchMarket)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
                client.DefaultRequestHeaders.Add("X-API-SECRET", apiSecret);

                var content = new StringContent("{  \"exchange_code\": \"" + exchCode + "\", \"exchange_market\": \"" + exchMarket + "\"}", System.Text.Encoding.Default, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiUrl+ "ticker", content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    PriceJson rootResult = JsonConvert.DeserializeObject<PriceJson>(result);
                    List<Price> prices = rootResult.data;
                    return prices;
                }
            }
            return null;
        }

        public static async Task<IList<Exchange>> DownLoadExchanges()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
                client.DefaultRequestHeaders.Add("X-API-SECRET", apiSecret);
                HttpResponseMessage response = await client.PostAsync(apiUrl+ "exchanges", null);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    JsonData rootResult = JsonConvert.DeserializeObject<JsonData>(result);
                    IList<Exchange> exchanges = rootResult.data;
                    return exchanges;
                }
            }
            return null;
        }

        public static async Task<List<Market>> DownloadMarkets(string exchCode)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
                client.DefaultRequestHeaders.Add("X-API-SECRET", apiSecret);

                var content = new StringContent("{  \"exchange_code\": \"" + exchCode + "\"}", System.Text.Encoding.Default, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiUrl+ "markets", content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    RootMarket rootResult = JsonConvert.DeserializeObject<RootMarket>(result);
                    List<Market> markets = rootResult.data;
                    return markets;
                }
            }
            return null;
        }

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

        private static async Task AddMarkets(Exchange exchange, string exch_code)
        {
            List<Market> markets = await GetMarkets(exch_code);
            foreach (Market market in markets)
            {
                MarketPrice marketPrice = new MarketPrice();
                if (market.mkt_name != null)
                {

                    marketPrice.marketName = market.mkt_name;
                    List<Price> prices = await GetPrices(market.mkt_name, exch_code);
                    if (prices != null)
                    {
                        marketPrice.marketPrice = prices[0].bid; //vraagprijs
                        marketPrice.BuyPrice = prices[0].ask;
                        exchange.mktName.Add(marketPrice);
                        DatabaseHelper database = new DatabaseHelper();
                        if (exch_code != null && market.mkt_name != null && marketPrice.marketPrice != null)
                        {
                            database.AddMarket(exch_code, market.mkt_name, marketPrice.marketPrice,marketPrice.BuyPrice);
                        }
                    }
                    
                }
            }
        }

        public static async Task<List<Market>> GetMarkets(string exchCode)
        {
            string apiUrl = "https://api.coinigy.com/api/v1/markets";
            string apiKey = "f9bff044bfc56a488e14789a5b73d4c0";
            string apiSecret = "2a68747c8b36351e1644e060020db291";
            //

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
                client.DefaultRequestHeaders.Add("X-API-SECRET", apiSecret);

                //var content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                var content = new StringContent("{  \"exchange_code\": \"" + exchCode + "\"}", System.Text.Encoding.Default, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    RootMarket rootResult = JsonConvert.DeserializeObject<RootMarket>(result);
                    List<Market> markets = rootResult.data;

                    //foreach (Market market in markets)
                    //{
                    //    string mkt_name = market.mkt_name;
                    //    // call markets https://api.coinigy.com/api/v1/markets -> needs code
                    //    // logica kijk welke markets crossen en exchanges erbij horen
                    //    //https://api.coinigy.com/api/v1/ticker needs code(GDAX) + market (BTC/USD)

                    //}
                    return markets;
                }
            }
            return null;
        }

        public static async Task<List<Price>> GetPrices(string marketname, string exchCode)
        {
            string apiUrl = "https://api.coinigy.com/api/v1/ticker";
            string apiKey = "f9bff044bfc56a488e14789a5b73d4c0";
            string apiSecret = "2a68747c8b36351e1644e060020db291";
            //

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
                client.DefaultRequestHeaders.Add("X-API-SECRET", apiSecret);

                //var content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                var content = new StringContent("{  \"exchange_code\": \"" + exchCode + "\", \"exchange_market\": \"" + marketname + "\"}", System.Text.Encoding.Default, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    PriceJson rootResult = JsonConvert.DeserializeObject<PriceJson>(result);
                    List<Price> prices = rootResult.data;

                    //foreach (Market market in markets)
                    //{
                    //    string mkt_name = market.mkt_name;
                    //    // call markets https://api.coinigy.com/api/v1/markets -> needs code
                    //    // logica kijk welke markets crossen en exchanges erbij horen
                    //    //https://api.coinigy.com/api/v1/ticker needs code(GDAX) + market (BTC/USD)

                    //}
                    return prices;
                }
            }
            return null;
        }


    }
}
