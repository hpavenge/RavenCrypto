using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RavenCryptoConsole.Model;

namespace RavenCryptoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            #region OldMethod
            //GETS DATA FROM API
            //IList<RestHelper.Exchange> result = await RestHelper.GetExchanges();


            //AddDifferenceToDatabase();
            // price diff = price A - price B

            #endregion
            DatabaseHelper databaseHelper = new DatabaseHelper();

            #region DownloadExchanges
            //            IList<Exchange> exchanges = await RestHelper.DownLoadExchanges();
            //            DatabaseHelper databaseHelper = new DatabaseHelper();
            //            databaseHelper.InsertExchanges(exchanges);
            #endregion

            #region DownloadMarkets
            //            DbSet<ExchangeDb> exchangeDbs = databaseHelper.GetAllExchanges();
            //            foreach (ExchangeDb exchange in exchangeDbs)
            //            {
            //                IList<Market> markets = await RestHelper.DownloadMarkets(exchange.exch_code);
            //                DatabaseHelper databaseHelper2 = new DatabaseHelper();
            //                databaseHelper2.InsertMarkets(markets);
            //            }
            #endregion

            #region DownloadPrices
            //            DbSet<MarketsDb> marketsDb = databaseHelper.GetAllMarkets();
            //            foreach (MarketsDb market in marketsDb)
            //            {
            //                List<Price> prices = await RestHelper.DownloadPrices(market.exch_code, market.mkt_name);
            //                DatabaseHelper databaseHelper2 = new DatabaseHelper();
            //                if (prices != null)
            //                {
            //                    databaseHelper2.InsertPrices(prices);
            //                }
            //            }
            #endregion

            #region THEMAGIC
            // calculate price diff between pairs
            // find pairs multiple exchanges
            DbSet<PriceDb> prices  = databaseHelper.GetAllPrices();
            DbSet<MarketPairsDistinct> pairs = databaseHelper.GetAllPairs();
            //haal voor elke pair de prices en markets op
            foreach (MarketPairsDistinct marketPairsDistinct in pairs)
            {
                IQueryable<PriceDb> priceDbs = prices.Where(x => x.market == marketPairsDistinct.market && x.exchange != "CCEX" && x.exchange != "YOBT");

                // laagste koopprijs
                double buy = Convert.ToDouble(priceDbs.Min(x => x.ask));
                PriceDb buyDb = priceDbs.OrderBy(m => m.ask).FirstOrDefault();
                //hoogste sellprijs
                double sell = Convert.ToDouble(priceDbs.Max(x => x.bid));
                PriceDb sellDb = priceDbs.OrderByDescending(o => o.bid).FirstOrDefault();
                if (buy < sell)
                {
                        double difference = (sell - buy) / sell * 100;
                    // difference bigger than 10% and lower than 20%
                    if (difference < 20 && difference > 10)
                    {
                        //volume atleast 1000
                        if (Convert.ToDouble(buyDb.current_volume) > 1000 && Convert.ToDouble(sellDb.current_volume) > 1000)
                        {
                            Console.WriteLine("Chance: " + marketPairsDistinct.market + " Buy on " + buyDb.exchange + " for :" + buy + " Sell on " + sellDb.exchange + " for " + sell + "Difference:" + difference);
                        }
                        
                    }
                        
                }
            }

            #endregion
        }

        private static void AddDifferenceToDatabase()
        {
            //CALC differences with Database
            DatabaseHelper database = new DatabaseHelper();
            //var distinctNames = (from d in YourList select d).Distinct();
            List<string> marketNames = database.getDistinctListMarket();
            // list every market foreach getMarket
            foreach (string market in marketNames)
            {
                IEnumerable<CryptoMarket> marketsBLC = database.getMarketsByMarket(market);
                // get highest and lowest
                var highestSell = marketsBLC.OrderByDescending(item => item.price).First();
                var lowestBuy = marketsBLC.OrderBy(x => x.buyPrice).First();
                double highestSellNumber = Convert.ToDouble(highestSell.price);
                double lowestBuyNumber = Convert.ToDouble(lowestBuy.buyPrice);
                double differencePercentage = (highestSellNumber - lowestBuyNumber) / highestSellNumber;
                CryptoMarket buyMarketInfo = marketsBLC.OrderBy(x => x.buyPrice).First();
                CryptoMarket SellMarketInfo = marketsBLC.OrderByDescending(item => item.price).First();
                if (differencePercentage > 0.20)
                {
                    CryptoMarket SellMarket = marketsBLC.OrderByDescending(item => item.price).First();
                    CryptoMarket buyMarket = marketsBLC.OrderBy(x => x.buyPrice).First();
                    database.insertChance(SellMarket.name, buyMarket.name, differencePercentage, highestSellNumber, lowestBuyNumber, market
                        );
                }
            }
        }
    }
}
