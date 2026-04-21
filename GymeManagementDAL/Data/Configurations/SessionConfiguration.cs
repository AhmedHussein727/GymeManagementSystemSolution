using GymeManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GymeManagementDAL.Data.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Sessions>
    {
        public void Configure(EntityTypeBuilder<Sessions> builder)
        {
            builder.ToTable(tb=>
            {
                tb.HasCheckConstraint("checkCpacity", "Capacity between 1 and 25");
                tb.HasCheckConstraint("checkSsessionDate", "EndDate > StartDate");
            });

            builder.HasOne(s => s.Category)
                .WithMany(c => c.Sessions)
                .HasForeignKey(s => s.CategoryId);

            builder.HasOne(s => s.Trainer)
                .WithMany(t => t.TrainerSessions)
                .HasForeignKey(s => s.TrainerId);

            builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
