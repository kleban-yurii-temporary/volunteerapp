using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerRequestApp.Server.Core
{
    public class CurrencyPair
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? СurrencyFrom { get; set; }
        public string? СurrencyTo { get; set; }
        public virtual ICollection<ExchangeRate>? Records { get; }
        public bool IsActive { get; set; }

        [NotMapped]
        public double TopRate { get { return (double)Records.MaxBy(x => x.CreatedOn).Value; }  }
    }
}
