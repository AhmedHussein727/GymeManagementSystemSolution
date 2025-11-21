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
    public class HealthRecordConfigurations : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.ToTable("Members")
                .HasKey(h=>h.Id);

            builder.HasOne<Member>()
                .WithOne(m => m.HealthRecord)
                .HasForeignKey<HealthRecord>(h => h.Id);

        }
    }
}
