using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerRequestApp.Server.Core
{
    public class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Title { get; set; }

        [DataType(DataType.Html)]
        public string? Details { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? OpenDate { get; set; } = DateTime.UtcNow;
        public DateTime? CloseDate { get; set; } = DateTime.UtcNow;
        public Status? Status { get; set; }
        public double? NeedSum { get; set; } = 0;
        public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();
        public virtual ICollection<Photo>? Photos { get; set; }
        public bool IsFavorite { get; set; }
    }
}
