using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerRequestApp.Shared.Dtos.Request
{
    public class DonationCreateDto
    {
        public string? UserName { get; set; }
        public double? Amount { get; set; }

    }

    public class DonationReadDto
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? UserName { get; set; }
        public double? Amount { get; set; }

    }
}
