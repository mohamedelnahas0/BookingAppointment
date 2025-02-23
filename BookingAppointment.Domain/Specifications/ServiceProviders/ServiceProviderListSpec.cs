using BookingAppointment.Domain.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Specifications.ServiceProviders
{
    public class ServiceProviderListSpec : BaseSpecification<BookingAppointment.Domain.Entities.ServiceProvider>
    {
        public ServiceProviderListSpec(ServiceProviderSpecParams specParams)
            : base(sp =>
                (string.IsNullOrEmpty(specParams.Search) ||
                 (sp.FirstName + " " + sp.LastName).ToLower().Contains(specParams.Search.ToLower())) &&
                (!specParams.ServiceId.HasValue || sp.Services.Any(s => s.Id == specParams.ServiceId)))
        {
            Includes.Add(sp => sp.Services);
            Includes.Add(sp => sp.Schedule);

            ApplyOrderBy(sp => sp.FirstName);
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }
    
}
}
