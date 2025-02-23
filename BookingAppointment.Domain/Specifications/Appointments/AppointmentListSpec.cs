using BookingAppointment.Application.Parameters;
using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Specifications.Appointments
{
    public class AppointmentListSpec : BaseSpecification<Appointment>
    {
        public AppointmentListSpec(AppointmentSpecParams specParams)
            : base(a =>
                (string.IsNullOrEmpty(specParams.Search) || a.Customer.FirstName.ToLower().Contains(specParams.Search) ||
                 a.Customer.LastName.ToLower().Contains(specParams.Search)) &&
                (!specParams.CustomerId.HasValue || a.CustomerId == specParams.CustomerId) &&
                (!specParams.ServiceProviderId.HasValue || a.ServiceProviderId == specParams.ServiceProviderId) &&
                (!specParams.Status.HasValue || a.Status == specParams.Status) &&
                (!specParams.FromDate.HasValue || a.AppointmentDateTime >= specParams.FromDate) &&
                (!specParams.ToDate.HasValue || a.AppointmentDateTime <= specParams.ToDate))
        {
            AddIncludes(a => a.Customer);
            AddIncludes(a => a.Service);
            AddIncludes(a => a.ServiceProvider);

            if (specParams.Sort?.ToLower() == "date")
                ApplyOrderBy(a => a.AppointmentDateTime);
            else if (specParams.Sort?.ToLower() == "date-desc")
                ApplyOrderByDescending(a => a.AppointmentDateTime);

            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }
    }
}
