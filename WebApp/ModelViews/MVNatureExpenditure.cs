using WebApp.Models;
using WebApp.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebApp.ModelViews
{
    public class MVNatureExpenditure
    {
        public int? id { get; set; }
        [Required(ErrorMessage = "O campo 'Nome' é necessário")]
        [MaxLength(100, ErrorMessage = "O tamanho máximo é de 100 caracteres")]
        public string name { get; set; }


        public async static Task<NatureExpenditure> LoadObjectAsync(IRepository repository, MVNatureExpenditure model)
        {
            var obj = await repository.LoadAsync<NatureExpenditure>(model.id);

            if (obj != null)
                return obj;

            obj = new NatureExpenditure
            {
                Name = model.name
            };

            return obj;
        }
    }
}