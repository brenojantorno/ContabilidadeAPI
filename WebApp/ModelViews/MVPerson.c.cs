using WebApp.Models;
using WebApp.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebApp.ModelViews
{
    public class MVPerson
    {
        public int? id { get; set; }

        [Required(ErrorMessage = "O campo 'Nome' é necessário")]
        [MaxLength(100, ErrorMessage = "O tamanho máximo é de 100 caracteres")]
        public string name { get; set; }
        [Required(ErrorMessage = "O campo 'Sobrenome' é necessário")]
        [MaxLength(100, ErrorMessage = "O tamanho máximo é de 100 caracteres")]
        public string surname { get; set; }
        [Required(ErrorMessage = "O campo 'Email' é necessário")]
        [MaxLength(150, ErrorMessage = "O tamanho máximo é de 100 caracteres")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
        public string email { get; set; }

        public MVUser modelUser { get; set; }

        public async static Task<Person> LoadObjectAsync(IRepository repository, MVPerson model)
        {
            var obj = await repository.LoadAsync<Person>(model.id);

            if (obj != null)
                return obj;

            obj = new Person
            {
                Name = model.name,
                Surname = model.surname,
                Email = model.email
            };

            return obj;
        }
    }
}