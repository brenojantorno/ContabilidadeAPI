using System;
using WebApp.Models.Interfaces;

namespace WebApp.Models
{
    public class Expenditure : IBaseEntity
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public string Observation { get; set; }
        public bool Fixed { get; set; }
        public DateTime Date { get; set; }
        public DateTime ExpirationDate { get; set; }
        public virtual Person Person { get; set; }
        public int IdPerson { get; set; }
        public virtual NatureExpenditure NatureExpenditure { get; set; }
        public int IdNatureExpenditure { get; set; }
    }
}