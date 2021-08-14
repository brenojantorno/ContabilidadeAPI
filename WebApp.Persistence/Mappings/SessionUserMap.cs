using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Security;

namespace WebApp.Persistence.Mappings
{
    internal class SessionUserMap : IEntityTypeConfiguration<SessionUser>
    {
        public void Configure(EntityTypeBuilder<SessionUser> builder)
        {
            builder.ToTable("SESSION_USER");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
           .IsRequired()
           .HasColumnName("id_Session");

            builder.Property(x => x.Browser)
            .HasColumnName("BROWSER")
            .IsRequired();

            builder.Property(x => x.Data)
            .HasColumnName("DATA")
            .IsRequired();

            builder.Property(x => x.Token)
            .HasColumnName("TOKEN")
            .IsRequired();

            builder.Property(x => x.Expires)
            .HasColumnName("EXPIRES")
            .IsRequired();

            builder.HasOne(r => r.User)
            .WithMany(p => p.Sessions)
            .HasForeignKey(r => r.idUser)
            .HasPrincipalKey(p => p.Id);
        }
    }
}
