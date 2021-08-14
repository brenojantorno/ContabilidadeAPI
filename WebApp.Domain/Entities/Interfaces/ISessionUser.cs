using System;
using WebApp.Models;

namespace WebApp.Security
{
    public interface ISessionUser
    {
        int Id { get; set; }
        string Token { get; set; }
        string Browser { get; set; }
        DateTime Expires { get; set; }
        int idUser { get; set; }
        DateTime Data { get; set; }
        User User { get; set; }
    }
}
