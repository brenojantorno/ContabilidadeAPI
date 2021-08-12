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
using Newtonsoft.Json;

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


        [HttpPost("Login")]
        [AllowAnonymous]
        [SwaggerResponse(200, "Login efetuado com sucesso", typeof(MVUser))]
        [SwaggerResponse(500, "Error interno", typeof(GenericError))]
        public async Task<IActionResult> Login(MVUser model)
        {
            if (ModelState.IsValid)
            {
                var encriptPassword = Security.SecuritySettings.Encript(model.password);
                var user = await _repository.LoadAsyncCondition<User>(u => u.Passsword.Equals(encriptPassword) && u.Login.Equals(model.login));

                if (user == null)
                    return BadRequest("Usuário não encontrado");

                var token = Token.GenerateToken(user);
                _session.Token = token;
                _session.User = user;
                _session.Browser = HttpContext.Request.Headers["User-Agent"];
                _session.Data = DateTime.Now;
                _session.Expires = DateTime.Now.AddSeconds(30);

                var sessionUser = new SessionUser()
                {
                    Token = token,
                    User = user,
                    Browser = _session.Browser,
                    Data = _session.Data,
                    Expires = _session.Expires
                };

                var x = await _repository.AddAsync<SessionUser>(sessionUser);
                var obj = new { token = token };

                var result = JsonConvert.SerializeObject(obj);

                return Ok(result);
            }
            return BadRequest();
        }
    }
}
