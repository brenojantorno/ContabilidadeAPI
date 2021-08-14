using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Models;

namespace WebApp.Persistence.Mappings
{
    internal class PersonMap : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("PERSON");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
           .IsRequired()
           .HasColumnName("ID_PERSON");

            builder.Property(x => x.Name)
            .HasColumnName("NAME")
            .IsRequired();

            builder.Property(x => x.Surname)
            .HasColumnName("SURNAME")
            .IsRequired();

            builder.Property(x => x.Email)
            .HasColumnName("EMAIL")
            .IsRequired();

            builder.HasMany(x => x.Expenditures);
            builder.HasMany(x => x.FinancialTransfers);
            builder.HasMany(x => x.Users);
        }
    }
}
