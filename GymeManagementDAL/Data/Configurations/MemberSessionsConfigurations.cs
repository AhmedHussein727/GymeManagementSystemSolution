using GymeManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


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
                .HasForeignKey(ms => ms.MemberId);

            builder.HasOne(ms => ms.sessions)
                .WithMany(s=>s.MemberSessions)
                .HasForeignKey(ms=>ms.SessionId);

            builder.HasKey(x=>x.Id);

            
                
        }
    }
}
