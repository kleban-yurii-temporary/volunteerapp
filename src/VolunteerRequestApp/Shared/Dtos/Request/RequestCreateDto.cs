using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerRequestApp.Shared.Dtos.Request
{
    public class RequestCreateDto
    {
        [Required(ErrorMessage = "Введіть назву нового запиту")]
        [MinLength(5, ErrorMessage = "Мінімальна довжина поля 5 символів")]
        public string? Title { get; set; }
    }
}
