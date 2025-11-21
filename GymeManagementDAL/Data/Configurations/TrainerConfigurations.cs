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
    public class TrainerConfigurations : GymeUserConfigurations<Trainer>, IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(m => m.CreatedAt)
                .HasColumnName("HireDate")
                .HasDefaultValueSql("GETDATE()");

            base.Configure(builder);
        }
    }
}
