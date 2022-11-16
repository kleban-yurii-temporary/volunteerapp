using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerRequestApp.Shared.Dtos.Currency
{
    public class CurrencyPairReadDto
    {
        public int Id { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public double? CurrentValue { get; set; }
    }
}
