using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerRequestApp.Server.Core
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public double? Value { get; set; }

        [ForeignKey("CurrencyPair")]
        public int CurrencyPairId { get; set; }
        public CurrencyPair? CurrenciesPair { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
