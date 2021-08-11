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
    public class ExpenditureController : ControllerBase
    {
        private readonly IRepository _repository;

        public ExpenditureController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Expenditure>> Get(int id)
        {
            var obj = await _repository.LoadAsync<Expenditure>(id);

            if (obj == null)
                return NotFound("Objeto não encontrado");

            return Ok(obj);
        }

        [HttpGet]
        public async Task<IEnumerable<Expenditure>> Get()
        {
            return await _repository.CollectionAsync<Expenditure>();
        }

        [HttpPost]
        public async Task<ActionResult<Expenditure>> Post([FromBody] MVExpenditure model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = await MVExpenditure.LoadObjectAsync(_repository, model);
                    if (obj != null)
                    {
                        var idsResult = await _repository.AddAsync<Expenditure>(obj);
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
        public async Task<ActionResult<Expenditure>> Put([FromBody] MVExpenditure model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = await MVExpenditure.LoadObjectAsync(_repository, model);
                    await _repository.UpdateAsync<Expenditure>(obj);
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
        public async Task<ActionResult<Expenditure>> Remove([FromBody] int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = await _repository.LoadAsync<Expenditure>(id);
                    if (obj != null)
                    {
                        await _repository.RemoveAsync<Expenditure>(obj);
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
