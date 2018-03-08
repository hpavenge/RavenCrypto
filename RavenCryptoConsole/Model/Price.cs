using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenCryptoConsole.Model
{
    public class Price
    {
        public string exchange { get; set; }
        public string market { get; set; }
        public string last_trade { get; set; }
        public string high_trade { get; set; }
        public string low_trade { get; set; }
        public string current_volume { get; set; }
        public string timestamp { get; set; }
        public string ask { get; set; }
        public string bid { get; set; }
    }

    public class PriceJson
    {
        public List<Price> data { get; set; }
    }
}
