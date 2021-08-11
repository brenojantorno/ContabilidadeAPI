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
    public class NatureExpenditureController : ControllerBase
    {
        private readonly IRepository _repository;

        public NatureExpenditureController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NatureExpenditure>> Get(int id)
        {
            var obj = await _repository.LoadAsync<NatureExpenditure>(id);

            if (obj == null)
                return NotFound("Objeto não encontrado");

            return Ok(obj);
        }

        [HttpGet]
        public async Task<IEnumerable<NatureExpenditure>> Get()
        {
            return await _repository.CollectionAsync<NatureExpenditure>();
        }

        [HttpPost]
        public async Task<ActionResult<NatureExpenditure>> Post([FromBody] MVNatureExpenditure model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = await MVNatureExpenditure.LoadObjectAsync(_repository, model);
                    if (obj != null)
                    {
                        var idsResult = await _repository.AddAsync<NatureExpenditure>(obj);
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
        public async Task<ActionResult<NatureExpenditure>> Put([FromBody] MVNatureExpenditure model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = await MVNatureExpenditure.LoadObjectAsync(_repository, model);
                    obj.Name = model.name;
                    await _repository.UpdateAsync<NatureExpenditure>(obj);
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
        public async Task<ActionResult<NatureExpenditure>> Remove([FromBody] int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = await _repository.LoadAsync<NatureExpenditure>(id);
                    if (obj != null)
                    {
                        if (obj.Expenditures.Any())
                            await _repository.RemoveRangeAsync<Expenditure>(obj.Expenditures);

                        await _repository.RemoveAsync<NatureExpenditure>(obj);
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
