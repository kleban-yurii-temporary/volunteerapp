using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerRequestApp.Shared.Dtos.Currency
{
    public class LatestCurrencyData
    {
        public string? Disclaimer { get; set; }
        public string? License { get; set; }
        public string? Base { get; set; }
        public long Timestamp { get; set; }
        public Dictionary<string, decimal>? Rates { get; set; }
    }
}
