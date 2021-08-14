using System;
using WebApp.Models;

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

        //public SessionUser(string token, string browser, DateTime expires, int idUser, DateTime data, User user)
        //{
        //    Token = token;
        //    Browser = browser;
        //    Expires = expires;
        //    this.idUser = idUser;
        //    Data = data;
        //    User = user;
        //}
    }
}
