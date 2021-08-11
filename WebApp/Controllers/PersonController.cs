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
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IRepository _repository;

        public PersonController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get([FromRoute] int id)
        {
            var obj = await _repository.LoadAsync<Person>(id);

            if (obj == null)
                return NotFound("Objeto não encontrado");

            return Ok(new { nome= obj.CompleteName, id = obj.Id});
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> Get()
        {
            return await _repository.CollectionAsync<Person>();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Person>> Post([FromBody] MVPerson model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = await MVPerson.LoadObjectAsync(_repository, model);

                    if (model.modelUser != null)
                    {
                        var loginExiste = await _repository.CollectionAsNoTrackingAsync<User>(x => x.Login.ToLower().Contains(model.modelUser.login.ToLower()));
                        if (loginExiste.Any())
                            return BadRequest("Login existente");

                        var user = await MVUser.LoadObjectAsync(_repository, model.modelUser);
                        user.Passsword = Security.SecuritySettings.Encript(user.Passsword);
                        obj.Users.Add(user);
                    }

                    if (obj != null)
                    {
                        var idsResult = await _repository.AddAsync<Person>(obj);
                        return Ok(new { sucess = true, login = obj.CurrentUser.Login, nomeCompleto= obj.CompleteName});
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
        public async Task<ActionResult<Person>> Put([FromBody] MVPerson model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = await MVPerson.LoadObjectAsync(_repository, model);
                    if (obj != null)
                    {
                        obj.Email = model.email;
                        obj.Name = model.name;
                        obj.Surname = model.surname;
                        await _repository.UpdateAsync(obj);
                    }
                    return Ok(obj);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> Remove([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var obj = await _repository.LoadAsync<Person>(id);
                    if (obj != null)
                    {
                        if (obj.Expenditures.Any())
                            await _repository.RemoveRangeAsync<Expenditure>(obj.Expenditures);

                        if (obj.FinancialTransfers.Any())
                            await _repository.RemoveRangeAsync<FinancialTransfer>(obj.FinancialTransfers);

                        await _repository.RemoveAsync<Person>(obj);
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
