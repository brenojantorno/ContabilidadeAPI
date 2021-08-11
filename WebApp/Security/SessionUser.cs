using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Models;
using Microsoft.AspNetCore.Http;

namespace WebApp.Security
{
    public class SessionUser : ISessionUser
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Browser { get; set; }
        public DateTime Expires { get; set; }
        public int idUser { get; set; }
        public DateTime Data { get; set; }
        public virtual User User { get; set; }
    }
}
