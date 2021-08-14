using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Interfaces;
using WebApp.Security;
using WebApp.ModelViews;
using WebApp.Models;
using Swashbuckle.AspNetCore.Annotations;
using WebApp.DTO;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        private readonly IRepository _repository;
        private ISessionUser _session;

        public HomeController(IRepository repository, ISessionUser session)
        {
            _repository = repository;
            _session = session;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        [SwaggerResponse(200, "Login efetuado com sucesso", typeof(PersonDTO))]
        [SwaggerResponse(500, "Erro interno", typeof(GenericError))]
        public async Task<IActionResult> Login(MVUser model)
        {
            if (ModelState.IsValid)
            {
                var encriptPassword = SecuritySettings.Encript(model.password);
                var user = await _repository.LoadAsyncCondition<User>(u => u.Password.Equals(encriptPassword) && u.Login.Equals(model.login));
                var erros = new List<string>();

                if (user == null)
                    erros.Add("Usuário não encontrado");

                if (user != null)
                {
                    var token = Token.GenerateToken(user);
                    var sessionUser = new SessionUser()
                    {
                        Token = token,
                        User = user,
                        Browser = HttpContext.Request.Headers["User-Agent"],
                        Data = DateTime.Now,
                        Expires = DateTime.Now.AddMinutes(60)
                    };

                    _session = sessionUser;

                    await _repository.AddAsync<SessionUser>(sessionUser);

                    return Ok(new PersonDTO(user.Person.CompleteName, user.Person.Email, token, !erros.Any(), erros));
                }
            }
            return BadRequest();
        }
    }
}
