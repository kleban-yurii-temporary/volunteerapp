using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerRequestApp.Shared.Dtos.Request
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public bool IsMain { get; set; }
        public string? Path { get; set; }
    }
}
