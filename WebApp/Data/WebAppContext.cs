using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Security;

namespace WebApp.Data
{
    public class WebAppContext : DbContext
    {
        public WebAppContext(DbContextOptions<WebAppContext> options) : base(options) { }
        public DbSet<Person> Person { get; set; }
        public DbSet<Expenditure> Expenditure { get; set; }
        public DbSet<NatureExpenditure> NatureExpenditure { get; set; }
        public DbSet<FinancialTransfer> FinancialTransfer { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(person =>
            {
                person.HasKey(x => x.Id);
                person.Property(x => x.Id)
               .IsRequired()
               .HasColumnName("id_Person");

                person.Property(x => x.Name)
                .IsRequired();
                person.Property(x => x.Surname)
                .IsRequired();
                person.Property(x => x.Email)
                .IsRequired();

                person.HasMany(x => x.Expenditures);
                person.HasMany(x => x.FinancialTransfers);
                person.HasMany(x => x.Users);
            });

            modelBuilder.Entity<User>(user =>
            {
                user.HasKey(x => x.Id);
                user.Property(x => x.Id)
               .IsRequired()
               .HasColumnName("id_User");

                user.Property(x => x.Login)
                .IsRequired();
                user.Property(x => x.Passsword)
                .IsRequired();

                user.HasMany(x => x.Sessions);

                user.HasOne(r => r.Person)
                .WithMany(p => p.Users)
                .HasForeignKey(r => r.IdPerson)
                .HasPrincipalKey(p => p.Id);
            });

            modelBuilder.Entity<SessionUser>(session =>
            {
                session.HasKey(x => x.Id);
                session.Property(x => x.Id)
               .IsRequired()
               .HasColumnName("id_Session");

                session.Property(x => x.Browser)
                .IsRequired();
                session.Property(x => x.Data)
                .IsRequired();
                session.Property(x => x.Token)
                .IsRequired();
                session.Property(x => x.Expires)
                .IsRequired();

                session.HasOne(r => r.User)
                .WithMany(p => p.Sessions)
                .HasForeignKey(r => r.idUser)
                .HasPrincipalKey(p => p.Id);
            });

            modelBuilder.Entity<FinancialTransfer>(transfer =>
            {
                transfer.HasKey(x => x.Id);
                transfer.Property(x => x.Id)
               .IsRequired()
               .HasColumnName("id_FinancialTransfer");

                transfer.Property(x => x.Value)
                .HasColumnType("numeric")
                .HasPrecision(15, 2)
                .IsRequired();
                transfer.Property(x => x.Date)
                .IsRequired();
                transfer.Property(x => x.RegisterDate)
                .IsRequired();
                transfer.Property(x => x.IdPerson)
                .HasColumnName("id_Person")
                .IsRequired();

                transfer.HasOne(r => r.Person)
                .WithMany(p => p.FinancialTransfers)
                .HasForeignKey(r => r.IdPerson)
                .HasPrincipalKey(p => p.Id);
            });

            modelBuilder.Entity<Expenditure>(expenditure =>
           {
               expenditure.HasKey(x => x.Id);
               expenditure.Property(x => x.Id)
              .IsRequired()
              .HasColumnName("id_Expenditure");

               expenditure.Property(x => x.Value)
               .HasColumnType("numeric")
               .HasPrecision(15, 2)
               .IsRequired();
               expenditure.Property(x => x.Date)
               .IsRequired();
               expenditure.Property(x => x.IdPerson)
               .HasColumnName("id_Pessoa")
               .IsRequired();
               expenditure.Property(x => x.IdNatureExpenditure)
               .HasColumnName("id_NatureExpenditure")
               .IsRequired();
               expenditure.Property(x => x.Date)
               .IsRequired();
               expenditure.Property(x => x.ExpirationDate)
               .IsRequired();
               expenditure.Property(x => x.Fixed)
               .IsRequired();

               expenditure.Property(x => x.Observation);

               expenditure.HasOne(r => r.Person)
               .WithMany(p => p.Expenditures)
               .HasForeignKey(r => r.IdPerson)
               .HasPrincipalKey(p => p.Id)
               .IsRequired();

               expenditure.HasOne(r => r.NatureExpenditure)
               .WithMany(p => p.Expenditures)
               .HasForeignKey(r => r.IdNatureExpenditure)
               .HasPrincipalKey(p => p.Id)
               .IsRequired();
           });


            modelBuilder.Entity<NatureExpenditure>(nature =>
        {
            nature.HasKey(x => x.Id);
            nature.Property(x => x.Id)
           .IsRequired()
           .HasColumnName("id_NatureExpenditure");

            nature.Property(x => x.Name)
            .IsRequired();
            nature.HasMany(x => x.Expenditures);
        });
        }
    }
}