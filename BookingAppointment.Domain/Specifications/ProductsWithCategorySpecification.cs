using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Specifications
{
    public class ProductsWithCategorySpecification  : BaseSpecification<Product>
    {
        public ProductsWithCategorySpecification()
        {
           AddIncludes(p => p.Category);
        }

        public ProductsWithCategorySpecification(int id):base(P=>P.Id == id)
        {
            AddIncludes(p => p.Category);
        }
    }
}
