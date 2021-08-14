using WebApp.Models;
using System.Collections.Generic;
namespace WebApp.DTO
{
    public class PersonDTO : MessageResponse
    {
        public string CompleteName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public PersonDTO(string completeName, string email, string token, bool success, List<string> businessValidations) : base(success, businessValidations)
        {
            CompleteName = completeName;
            Email = email;
            Token = token;
        }
    }
}
