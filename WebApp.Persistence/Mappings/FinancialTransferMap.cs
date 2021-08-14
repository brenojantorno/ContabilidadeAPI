using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Models;

namespace WebApp.Persistence.Mappings
{
    internal class FinancialTransferMap : IEntityTypeConfiguration<FinancialTransfer>
    {
        public void Configure(EntityTypeBuilder<FinancialTransfer> builder)
        {
            builder.ToTable("FINANCIAL_TRANSFER");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
           .IsRequired()
           .HasColumnName("ID_FINANCIAL_TRANSFER");

            builder.Property(x => x.Value)
            .HasColumnName("VALUE")
            .HasColumnType("NUMERIC")
            .HasPrecision(15, 2)
            .IsRequired();

            builder.Property(x => x.Date)
            .HasColumnName("DATE")
            .IsRequired();

            builder.Property(x => x.RegisterDate)
            .HasColumnName("RESIGTER_DATE")
            .IsRequired();

            builder.Property(x => x.IdPerson)
            .HasColumnName("ID_PERSON")
            .IsRequired();

            builder.HasOne(r => r.Person)
            .WithMany(p => p.FinancialTransfers)
            .HasForeignKey(r => r.IdPerson)
            .HasPrincipalKey(p => p.Id);
        }
    }
}
