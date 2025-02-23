using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Entities
{
    public class Service : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<ServiceProvider> ServiceProviders { get; set; } = new List<ServiceProvider>();

        public Service()
        {
            ServiceProviders = new List<ServiceProvider>();
            Appointments = new List<Appointment>();
        }
    }

}
