using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerRequestApp.Shared.Dtos.Request
{
    public class _RequestCardViewDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Details { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public StatusDto? Status { get; set; }
        public double? NeedSum { get; set; }
        public double? CurrentSum { get; set; }
        public string? TargetMilitary { get; set; }
        public bool IsFavorite { get; set; }


        //public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();
        //public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
        //public virtual ICollection<Photo>? Photos { get; set; }
    }
}
