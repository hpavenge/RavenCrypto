using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenCryptoConsole.Model
{
    public class Market
    {
        public string exch_id { get; set; }
        public string exch_name { get; set; }
        public string exch_code { get; set; }
        public string mkt_id { get; set; }
        public string mkt_name { get; set; }
        public string exchmkt_id { get; set; }
    }

    public class RootMarket
    {
        public List<Market> data { get; set; }
    }
}
