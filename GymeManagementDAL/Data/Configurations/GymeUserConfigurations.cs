using GymeManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeManagementDAL.Data.Configurations
{
    public class GymeUserConfigurations<T> : IEntityTypeConfiguration<T> where T : GymeUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(g=>g.Name).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(g => g.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(g => g.UpdatedAt).HasDefaultValueSql("GETDATE()");


            builder.Property(g => g.Email).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(g=>g.Phone).HasColumnType("varchar").HasMaxLength(11);

            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("checkValidEmail", "Email like '_%@_%._%'");
                tb.HasCheckConstraint("checkValidPhone", "Phone like '01%' And Phone Not like '%[^0-9]%'");
            });

            builder.HasIndex(g => g.Name).IsUnique();
            builder.HasIndex(g => g.Phone).IsUnique();

            builder.OwnsOne(g => g.Address, ab =>
            {
                ab.Property(a => a.Streat).HasColumnName("Streat").HasColumnType("varchar").HasMaxLength(30);

                ab.Property(a => a.City).HasColumnName("City").HasColumnType("varchar").HasMaxLength(30);

                ab.Property(a => a.BuildingNumber).HasColumnName("BuildingNumber").HasColumnType("varchar").HasMaxLength(30);

            });





        }
    }
}
