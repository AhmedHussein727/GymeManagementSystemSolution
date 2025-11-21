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
    public class MemberSessionsConfigurations : IEntityTypeConfiguration<MemberSessions>
    {
        public void Configure(EntityTypeBuilder<MemberSessions> builder)
        {
            builder.Property(ms => ms.CreatedAt)
                .HasColumnName("BookingDate")
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(ms => ms.Member)
                .WithMany(m => m.MemberSessions)
                .HasForeignKey(ms => ms.MemberID);

            builder.HasOne(ms => ms.sessions)
                .WithMany(s=>s.MemberSessions)
                .HasForeignKey(ms=>ms.SessionID);

            builder.HasKey(ms => new {ms.MemberID,ms.SessionID});

            builder.Ignore(ms => ms.Id);
                
        }
    }
}
