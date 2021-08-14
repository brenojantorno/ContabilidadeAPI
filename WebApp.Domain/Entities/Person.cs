using System.Collections.Generic;
using WebApp.Models.Interfaces;
using System.Linq;

namespace WebApp.Models
{
    public class Person : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public virtual ICollection<FinancialTransfer> FinancialTransfers { get; set; }
        public virtual ICollection<Expenditure> Expenditures { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public Person()
        {
            FinancialTransfers = new List<FinancialTransfer>();
            Expenditures = new List<Expenditure>();
            Users = new List<User>();
        }

        public string CompleteName => Name + " " + Surname;

        public User CurrentUser => Users.FirstOrDefault();
    }
}