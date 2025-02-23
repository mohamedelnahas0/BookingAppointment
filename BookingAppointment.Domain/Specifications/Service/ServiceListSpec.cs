using BookingAppointment.Domain.Entities;
using BookingAppointment.Domain.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Specifications.ServiceProvider
{
    public class ServiceListSpec : BaseSpecification<Service>
    {
        public ServiceListSpec(ServiceSpecParams specParams)
            : base(s =>
                string.IsNullOrEmpty(specParams.Search) ||
                s.Name.ToLower().Contains(specParams.Search.ToLower()))
        {
            Includes.Add(s => s.ServiceProviders);

            if (specParams.Sort?.ToLower() == "price")
                ApplyOrderBy(s => s.Price);
            else if (specParams.Sort?.ToLower() == "price-desc")
                ApplyOrderByDescending(s => s.Price);
            else
                ApplyOrderBy(s => s.Name);

            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }
    }
}
