using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerRequestApp.Shared.Dtos.Request
{
    public class RequestUpdateDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле Назва обов'язкове")]
        [MinLength(5, ErrorMessage = "Мінімальна довжина поля 5 символів")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Поле Детальна інформація обов'язкове")]
        [MinLength(50, ErrorMessage = "Мінімальна довжина поля 50 символів")]
        public string? Details { get; set; }

        [Required(ErrorMessage = "Введіть дату початку збору коштів")]
        public DateTime? OpenDate { get; set; }

        [Required(ErrorMessage = "Введіть дату завершення збору коштів")]
        public DateTime? CloseDate { get; set; }          
        public StatusDto? Status { get; set; }

        [Required(ErrorMessage = "Поле Сума збору обов'язкове")]
        [Range(1, 1000000, ErrorMessage = "Дозволені лише значення у діапазоні [1, 10000000]")]
        public double? NeedSum { get; set; }
    }
}
