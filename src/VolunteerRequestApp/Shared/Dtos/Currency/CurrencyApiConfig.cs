using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerRequestApp.Shared.Dtos.Currency
{
    public class CurrencyApiConfig
    {
        public string? ApiKey { get; set; }
        public string? ApiUrl { get; set; }
        public string? BaseCurrency { get; set; }
        public string? SecondaryCurrency { get; set; }
    }
}
