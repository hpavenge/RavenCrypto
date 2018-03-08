using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using RavenCryptoConsole.Model;

namespace RavenCryptoConsole
{
    public class DatabaseHelper
    {
        private CryptoDBEntities database;

        public DatabaseHelper()
        {
            this.database = new CryptoDBEntities();
        }

        public void AddMarket(string exchange, string name, string price, string buyprice)
        {
            CryptoMarket cryptoMarket = new CryptoMarket()
            {
                name = exchange,
                market = name,
                price = price,
                buyPrice = buyprice
                
            };

            database.CryptoMarkets.Add(cryptoMarket);
            database.SaveChanges();
        }
        public List<string> getDistinctListMarket()
        {
            List<CryptoMarket> cryptoMarkets = new List<CryptoMarket>(database.CryptoMarkets);
            List<string> marketNames = cryptoMarkets
                .GroupBy(c => c.market)
                .Where(grp => grp.Count() > 1)
                .Select(grp => grp.Key).ToList();
           // List<string> marketNames = cryptoMarkets.Select(o => o.market).Distinct().ToList();

            return marketNames;
        }

        public void insertChance(string ExchangeToSell, string ExchangeToBuy, double differencePercentage, double SellPrice, double BuyPrice, string market)
        {
            Chance chance = new Chance()
            {
                BuyPrice = BuyPrice,
                Difference = differencePercentage,
                ExchangeToBuy = ExchangeToBuy,
                ExchangeToSell = ExchangeToSell,
                Market = market,
                SellPrice = SellPrice
            };
            database.Chances.Add(chance);
            database.SaveChanges();
            //TODO FINISH THIS SHIT 
        }
        public IEnumerable<CryptoMarket> getMarketsByMarket(string market)
        {
            List<CryptoMarket> cryptoMarkets = new List<CryptoMarket>(database.CryptoMarkets);
            IEnumerable<CryptoMarket> cryptoMarketsSorted = cryptoMarkets.Where(x => x.market == market);
            return cryptoMarketsSorted;
        }


        public void InsertExchanges(IList<Exchange> exchanges)
        {
            foreach (Exchange exchange in exchanges)
            {
                ExchangeDb exchangeDb = new ExchangeDb();
                if (exchange.exch_code != null) exchangeDb.exch_code = exchange.exch_code;
                if (exchange.exch_name != null) exchangeDb.exch_name = exchange.exch_name;
                if (exchange.exch_fee != null) exchangeDb.exch_fee = exchange.exch_fee;
                if (exchange.exch_trade_enabled != null) exchangeDb.exch_trade_enabled = exchange.exch_trade_enabled;
                if (exchange.exch_balance_enabled != null) exchangeDb.exch_balance_enabled = exchange.exch_balance_enabled;
                if (exchange.exch_url != null) exchangeDb.exch_url = exchange.exch_url;
                database.ExchangeDbs.Add(exchangeDb);
                database.SaveChanges();
            }

        }

        public void InsertMarkets(IList<Market> markets)
        {
            foreach (Market market in markets)
            {
                MarketsDb marketsDb = new MarketsDb();
                if (market.exch_id != null) marketsDb.exch_id = market.exch_id;
                if (market.exch_code != null) marketsDb.exch_code = market.exch_code;
                if (market.mkt_id != null) marketsDb.mkt_id = market.mkt_id;
                if (market.mkt_name != null) marketsDb.mkt_name = market.mkt_name;
                if (market.exchmkt_id != null) marketsDb.exchmkt_id = market.exchmkt_id;
                database.MarketsDbs.Add(marketsDb);
                database.SaveChanges();
            }

        }

        public void InsertPrices(List<Price> prices)
        {
            foreach (Price price in prices)
            {
                if (price != null)
                {
                    PriceDb priceDb = new PriceDb();
                    if (price.exchange != null) priceDb.exchange = price.exchange;
                    if (price.market != null) priceDb.market = price.market;
                    if (price.last_trade != null) priceDb.last_trade = price.last_trade;
                    if (price.high_trade != null) priceDb.high_trade = price.high_trade;
                    if (price.low_trade != null) priceDb.low_trade = price.low_trade;
                    if (price.current_volume != null) priceDb.current_volume = price.current_volume;
                    if (price.timestamp != null) priceDb.timestamp = price.timestamp;
                    if (price.ask != null) priceDb.ask = price.ask;
                    if (price.bid != null) priceDb.bid = price.bid;
                    database.PriceDbs.Add(priceDb);
                    database.SaveChanges();
                }
            }

        }

        public DbSet<ExchangeDb> GetAllExchanges()
        {
            return database.ExchangeDbs;
        }
        public DbSet<MarketsDb> GetAllMarkets()
        {
            return database.MarketsDbs;
        }
        public DbSet<PriceDb> GetAllPrices()
        {
            return database.PriceDbs;
        }

        public DbSet<MarketPairsDistinct> GetAllPairs()
        {
            return database.MarketPairsDistincts;
        }
    }
}
