using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Interfaces;
using WebApp.Security;

namespace WebApp.Models
{
    public class User : IBaseEntity
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Passsword { get; set; }
        public int IdPerson { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<SessionUser> Sessions { get; set; }
    }
}
