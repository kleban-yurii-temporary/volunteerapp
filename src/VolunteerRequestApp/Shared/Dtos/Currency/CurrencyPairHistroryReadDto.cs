using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerRequestApp.Shared.Dtos.Currency
{
    public class CurrencyPairHistroryReadDto
    {
        public int Id { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public IEnumerable<ExchangeRateReadDto>? Records { get; set; }
    }

    public class ExchangeRateReadDto
    {
        public int Id { get; set; }
        public double? Value { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
