using System.Collections.Generic;

namespace TraversalCore.Areas.Admin.Models
{
    public class ExchangesViewModel
    {
        public string base_currency_date { get; set; }
        public string base_currency { get; set; }
        public List<ExchangeRates> exchange_rates { get; set; }
    }
    public class ExchangeRates
    {
        public string exchange_rate_buy { get; set; }
        public string currency { get; set; }
    }
}
