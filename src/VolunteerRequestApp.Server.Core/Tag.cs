using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerRequestApp.Server.Core
{
    public class Tag
    {
        [Key]
        public string? Title { get; set; }
        public virtual ICollection<Request>? Requests { get; set; } = new List<Request>();
    }
}
