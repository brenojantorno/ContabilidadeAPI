using System.Reflection.PortableExecutable;
using System;
using WebApp.Models.Interfaces;
namespace WebApp.Models
{
    public class FinancialTransfer : IBaseEntity
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public DateTime RegisterDate { get; set; }
        public virtual Person Person { get; set; }
        public int IdPerson { get; set; }
    }
}