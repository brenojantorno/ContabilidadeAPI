using System.Reflection.PortableExecutable;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.Interfaces;
using WebApp.ModelViews;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepasseFinanceiroController : ControllerBase
    {
        private readonly IRepository _repository;

        public RepasseFinanceiroController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinancialTransfer>> Get([FromRoute] int id)
        {
            var obj = await _repository.LoadAsync<FinancialTransfer>(id);

            if (obj == null)
                return NotFound("Objeto não encontrado");

            return Ok(obj);
        }

        [HttpGet]
        public async Task<IEnumerable<FinancialTransfer>> Get()
        {
            return await _repository.CollectionAsync<FinancialTransfer>();
        }

        [HttpPost]
        public async Task<ActionResult<FinancialTransfer>> Post([FromBody] MVFinancialTransfer model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = await MVFinancialTransfer.LoadObjectAsync(_repository, model);
                    if (obj != null)
                    {
                        obj.RegisterDate = DateTime.Now;
                        var idsResult = await _repository.AddAsync<FinancialTransfer>(obj);
                        return Ok(obj);
                    }
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return BadRequest();
        }


        [HttpPut]
        public async Task<ActionResult<FinancialTransfer>> Put([FromBody] MVFinancialTransfer model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = await MVFinancialTransfer.LoadObjectAsync(_repository, model);
                    obj.RegisterDate = DateTime.Now;
                    await _repository.UpdateAsync<FinancialTransfer>(obj);
                    return Ok(obj);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult<FinancialTransfer>> Remove([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = _repository.LoadAsync<FinancialTransfer>(id);
                    if (obj.IsCompletedSuccessfully)
                    {
                        await _repository.RemoveAsync<FinancialTransfer>(obj.Result);
                        return Ok();
                    }
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return BadRequest();
        }
    }
}
