using System.Text;
using WebApp.Models;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using WebApp.Data;
using System;
using System.Threading.Tasks;

namespace WebApp.Security
{
    public class SecuritySettings
    {
        private static string Secret = "F9327F9D21F97C2171CF06D8E79F8054";
        public static byte[] KeyEncoding => Encoding.ASCII.GetBytes(Secret);
        public static string Encript(string message)
        {
            var sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(message));
            return BitConverter.ToString(hash).Replace("-", String.Empty);
        }
    }
}