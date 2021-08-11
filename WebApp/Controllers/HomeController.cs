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

                return Ok(new { sucess = true, token =  token });
            }
            return BadRequest();
        }
    }
}
