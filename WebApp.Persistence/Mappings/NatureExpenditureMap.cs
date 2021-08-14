using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Models;

namespace WebApp.Persistence.Mappings
{
    internal class NatureExpenditureMap : IEntityTypeConfiguration<NatureExpenditure>
    {
        public void Configure(EntityTypeBuilder<NatureExpenditure> builder)
        {
            builder.ToTable("NATURE_EXPENDITURE");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
           .HasColumnName("ID_NATURE_EXPENDITURE")
           .IsRequired();

            builder.Property(x => x.Name)
           .HasColumnName("NAME")
           .HasMaxLength(150)
           .IsRequired();

            builder.HasMany(x => x.Expenditures);
        }
    }
}
