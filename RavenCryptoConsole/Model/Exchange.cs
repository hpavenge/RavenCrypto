using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenCryptoConsole.Model
{
    public class Exchange
    {
        public string exch_id { get; set; }
        public string exch_name { get; set; }
        public string exch_code { get; set; }
        public string exch_fee { get; set; }
        public string exch_trade_enabled { get; set; }
        public string exch_balance_enabled { get; set; }
        public string exch_url { get; set; }

        public List<MarketPrice> mktName = new List<MarketPrice>();
    }

    public class JsonData
    {
        public IList<Exchange> data { get; set; }
    }
}
