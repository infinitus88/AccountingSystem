using AccountingSystem.Api.Models.Login;
using AccountingSystem.Api.Models.Users;
using AccountingSystem.Filters;
using AccountingSystem.Queries.Queries;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Server.RestAPI
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginQueryProcessor _loginQueryProcessor;
        private readonly IMapper _mapper;

        public LoginController(ILoginQueryProcessor loginQueryProcessor, IMapper mapper)
        {
            _loginQueryProcessor = loginQueryProcessor;
            _mapper = mapper;
        }

        [HttpPost("Authenticate")]
        [ValidateModel]
        public UserWithTokenModel Authenticate([FromBody] LoginModel model)
        {
            var result = _loginQueryProcessor.Authenticate(model.Email, model.Password);

            var resultModel = _mapper.Map<UserWithTokenModel>(result);

            return resultModel;
        }

        [HttpPost("Register")]
        [ValidateModel]
        public async Task<UserModel> Register([FromBody] RegisterModel model)
        {
            var result = await _loginQueryProcessor.Register(model);
            var resultModel = _mapper.Map<UserModel>(result);
            return resultModel;
        }

        [HttpPost("Password")]
        [Authorize]
        public async Task ChangePassword([FromBody] ChangeUserPasswordModel requestModel)
        {
            await _loginQueryProcessor.ChangePassword(requestModel);
        }
    }
}
