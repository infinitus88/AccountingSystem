using AccountingSystem.Api.Models.Users;
using AccountingSystem.Data.Access.Constants;
using AccountingSystem.Data.Model;
using AccountingSystem.Filters;
using AccountingSystem.Queries.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Server.RestAPI
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.AdministratorOrManager)]
    public class UsersController : ControllerBase
    {
        private readonly IUserQueryProcessor _query;
        private readonly IMapper _mapper;

        public UsersController(IUserQueryProcessor query, IMapper mapper)
        {
            _query = query;
            _mapper = mapper;
        }

        [HttpGet]
        [QueryableResult]
        public IQueryable<UserModel> Get()
        {
            var result = _query.Get();
            var models = result.ProjectTo<UserModel>(_mapper.ConfigurationProvider);

            return models;
        }

        [HttpGet("{id}")]
        public UserModel Get(int id)
        {
            var item = _query.Get(id);
            var model = _mapper.Map<UserModel>(item);

            return model;
        }

        //[HttpPost]
        //[ValidateModel]
        //public async Task<UserModel> Post([FromBody] CreateUserModel requestModel)
        //{
        //    var item = await _query.Create(requestModel);
        //    var model = _mapper.Map<UserModel>(item);
        //    return model;
        //}

        [HttpPost("{id}/password")]
        [ValidateModel]
        public async Task ChangePassword(int id, [FromBody] ChangeUserPasswordModel requestModel)
        {
            await _query.ChangePassword(id, requestModel);
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<UserModel> Put(int id, [FromBody] UpdateUserModel requestModel)
        {
            var item = await _query.Update(id, requestModel);
            var model = _mapper.Map<UserModel>(item);
            return model;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _query.Delete(id);
        }
    }
}
