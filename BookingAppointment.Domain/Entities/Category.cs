﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookingAppointment.Domain.Entities
{
    public class Category: BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
            [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; } 
    }
}
