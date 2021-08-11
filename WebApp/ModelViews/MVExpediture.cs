using WebApp.Models;
using WebApp.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;

namespace WebApp.ModelViews
{
    public class MVExpenditure
    {
        public int? id { get; set; }

        [Required(ErrorMessage = "O campo 'Valor' é necessário")]
        public decimal value { get; set; }
        [Required(ErrorMessage = "O campo 'Observação' é necessário")]
        [MaxLength(4000, ErrorMessage = "O tamanho máximo é de 4000 caracteres")]
        public string observation { get; set; }

        [Required(ErrorMessage = "O campo 'Despesa Fixa' é necessário")]
        public bool fixed_expenditure { get; set; }

        [Required(ErrorMessage = "O campo 'Data' é necessário")]
        public DateTime date { get; set; }

        [Required(ErrorMessage = "O campo 'Data expiração' é necessário")]
        public DateTime expirationDate { get; set; }

        [Required(ErrorMessage = "O campo 'Natureza de Despesa' é necessário")]
        public int idNatureExpenditure { get; set; }

        public async static Task<Expenditure> LoadObjectAsync(IRepository repository, MVExpenditure model)
        {
            var obj = await repository.LoadAsync<Expenditure>(model.id);

            if (obj != null)
                return obj;

            obj = new Expenditure
            {
                Value = model.value,
                Observation = model.observation,
                Date = model.date,
                ExpirationDate = model.expirationDate,
                IdNatureExpenditure = model.idNatureExpenditure,
            };

            return obj;
        }
    }
}