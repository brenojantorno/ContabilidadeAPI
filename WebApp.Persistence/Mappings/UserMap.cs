using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Models;

namespace WebApp.Persistence.Mappings
{
    internal class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("USER");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
            .HasColumnName("ID_USER")
            .IsRequired();

            builder.Property(x => x.Login)
            .HasColumnName("LOGIN")
            .IsRequired();

            builder.Property(x => x.Password)
            .HasColumnName("PASSWORD")
            .IsRequired();

            builder.HasMany(x => x.Sessions);

            builder.HasOne(r => r.Person)
            .WithMany(p => p.Users)
            .HasForeignKey(r => r.IdPerson)
            .HasPrincipalKey(p => p.Id);
        }
    }
}
