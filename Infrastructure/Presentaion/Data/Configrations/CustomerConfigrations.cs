using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Moduls;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistance.Data.Configrations
{
    public class CustomerConfigrations : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(N => N.Name).IsRequired();
            builder.Property(PH=>PH.PhoneNumber).IsRequired();
            builder.Property(E => E.Email).IsRequired();
            builder.OwnsOne(A => A.Address, Address =>
            {
                Address.Property(S=>S.City).HasMaxLength(100);
                Address.Property(S=>S.Street).HasMaxLength(100);
                Address.Property(S=>S.Country).HasMaxLength(100);
            });
                   
            builder.HasMany(O=>O.Orders)
                   .WithOne(C=>C.Customer)
                   .OnDelete(DeleteBehavior.Cascade);
        
           
        
        }
        
    }
}
