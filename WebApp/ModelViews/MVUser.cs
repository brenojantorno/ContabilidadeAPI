using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.Interfaces;

namespace WebApp.ModelViews
{
    public class MVUser
    {
        public int? id { get; set; }

        [Required(ErrorMessage = "Um nome é necessário")]
        [MinLength(6, ErrorMessage = "Necessário um minimo de 6 dígitos")]
        public string login { get; set; }

        [Required(ErrorMessage = "Uma senha é necessária")]
        [DataType(DataType.Password)]
        public string password { get; set; }


        public async static Task<User> LoadObjectAsync(IRepository repository, MVUser model)
        {
            var obj = await repository.LoadAsync<User>(model.id);

            if (obj != null)
                return obj;

            obj = new User
            {
                Login = model.login,
                Passsword = model.password,
            };

            return obj;
        }
    }
}