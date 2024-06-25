using eMuhasebeServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eMuhasebeServer.Infrastructure.Configurations
{
    public sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(p => p.TaxNumber).HasMaxLength(11).HasColumnType("nvarchar(11)");
            builder.Property(p => p.TaxOffice).HasMaxLength(100);
            builder.HasQueryFilter(p => !p.IsDeleted);
            builder.OwnsOne(p => p.Database, builder =>
            {
                builder.Property(p => p.Server).HasColumnName("Server");
                builder.Property(p => p.DatabaseName).HasColumnName("DatabaseName");
                builder.Property(p => p.UserId).HasColumnName("UserId");
                builder.Property(p => p.Password).HasColumnName("Password");
            });
        }
    }
}
