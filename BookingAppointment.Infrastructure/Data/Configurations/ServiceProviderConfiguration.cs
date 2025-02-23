using BookingAppointment.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Infrastructure.Data.Configurations
{
    public class ServiceProviderConfiguration : IEntityTypeConfiguration<ServiceProvider>
    {
        public void Configure(EntityTypeBuilder<ServiceProvider> builder)
        {
            builder.HasKey(sp => sp.Id);

            builder.Property(sp => sp.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(sp => sp.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(sp => sp.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(sp => sp.Phone)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(sp => sp.Schedule)
                .WithOne(s => s.ServiceProvider)
                .HasForeignKey<Schedule>(s => s.ServiceProviderId);
        }
    
    }
}
