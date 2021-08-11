using System.ComponentModel;
using WebApp.Models;
using WebApp.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;
namespace WebApp.ModelViews
{
    public class MVFinancialTransfer
    {
        public int? id { get; set; }
        [Required(ErrorMessage = "O campo 'Valor' é necessário")]
        public decimal value { get; set; }
        [Required(ErrorMessage = "O campo 'Data' é necessário")]
        public DateTime date { get; set; }
        public DateTime? registerDate { get; set; }
        [Required(ErrorMessage = "É necessário ter uma pessoa vinculada ao repasse financeiro")]
        public int idPerson { get; set; }

        public async static Task<FinancialTransfer> LoadObjectAsync(IRepository repository, MVFinancialTransfer model)
        {
            var obj = await repository.LoadAsync<FinancialTransfer>(model.id);

            if (obj != null)
                return obj;

            obj = new FinancialTransfer
            {
                Value = model.value,
                IdPerson = model.idPerson,
                Date = model.date
            };
            return obj;
        }
    }
}