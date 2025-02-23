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
    public class DeliveryInfoConfiguration : IEntityTypeConfiguration<DeliveryInfo>
    {
        public void Configure(EntityTypeBuilder<DeliveryInfo> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Address)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(d => d.TrackingNumber)
                .HasMaxLength(50);

            builder.Property(d => d.Status)
                .IsRequired();

            builder.HasOne(d => d.Order)
                .WithOne(o => o.DeliveryInfo)
                .HasForeignKey<DeliveryInfo>(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
