using BookingAppointment.Application.DTOS.Schedule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.DTOS.ServiceProvider
{
    public class CreateServiceProviderDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Please enter a valid phone number")]
        public string Phone { get; set; }

        public List<int> ServiceIds { get; set; } = new();

        [Required]
        public UpdateScheduleDto Schedule { get; set; }
    }
}
