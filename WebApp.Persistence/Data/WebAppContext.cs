using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Persistence.Mappings;
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
        public DbSet<SessionUser> SessionUser { get; set; }
        public DbSet<User> User { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExpenditureMap());
            modelBuilder.ApplyConfiguration(new FinancialTransferMap());
            modelBuilder.ApplyConfiguration(new NatureExpenditureMap());
            modelBuilder.ApplyConfiguration(new PersonMap());
            modelBuilder.ApplyConfiguration(new SessionUserMap());
            modelBuilder.ApplyConfiguration(new UserMap());


            base.OnModelCreating(modelBuilder);
        }
    }
}