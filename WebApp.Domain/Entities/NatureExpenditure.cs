using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using WebApp.Models.Interfaces;

namespace WebApp.Models
{
    public class NatureExpenditure : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Expenditure> Expenditures { get; set; }
    }
}