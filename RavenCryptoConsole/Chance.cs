//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RavenCryptoConsole
{
    using System;
    using System.Collections.Generic;
    
    public partial class Chance
    {
        public int id { get; set; }
        public string ExchangeToSell { get; set; }
        public string ExchangeToBuy { get; set; }
        public Nullable<double> Difference { get; set; }
        public Nullable<double> SellPrice { get; set; }
        public Nullable<double> BuyPrice { get; set; }
        public string Market { get; set; }
    }
}
