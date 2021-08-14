using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Models;

namespace WebApp.Persistence.Mappings
{
    internal class ExpenditureMap : IEntityTypeConfiguration<Expenditure>
    {
        public void Configure(EntityTypeBuilder<Expenditure> builder)
        {
            builder.ToTable("EXPENDITURE");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
           .HasColumnName("ID_EXPENDITURE")
           .IsRequired();

            builder.Property(x => x.Value)
            .HasColumnName("VALUE")
            .HasColumnType("NUMERIC")
            .HasPrecision(15, 2)
            .IsRequired();

            builder.Property(x => x.Date)
            .HasColumnName("DATE")
            .IsRequired();

            builder.Property(x => x.IdPerson)
            .HasColumnName("ID_PERSON")
            .IsRequired();

            builder.Property(x => x.IdNatureExpenditure)
            .HasColumnName("ID_NATURE_EXPENDITURE")
            .IsRequired();

            builder.Property(x => x.Date)
            .HasColumnName("DATE")
            .IsRequired();

            builder.Property(x => x.ExpirationDate)
            .HasColumnName("EXPIRATION_DATE")
            .IsRequired();

            builder.Property(x => x.Fixed)
            .HasColumnName("FIXED")
            .IsRequired();

            builder.Property(x => x.Observation)
            .HasColumnName("OBSERVATION")
            .HasMaxLength(2000);

            builder.HasOne(r => r.Person)
            .WithMany(p => p.Expenditures)
            .HasForeignKey(r => r.IdPerson)
            .HasPrincipalKey(p => p.Id)
            .IsRequired();

            builder.HasOne(r => r.NatureExpenditure)
            .WithMany(p => p.Expenditures)
            .HasForeignKey(r => r.IdNatureExpenditure)
            .HasPrincipalKey(p => p.Id)
            .IsRequired();
        }
    }
}
