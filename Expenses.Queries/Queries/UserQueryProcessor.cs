using AccountingSystem.Api.Common.Exceptions;
using AccountingSystem.Api.Models.Users;
using AccountingSystem.Data.Access.DAL;
using AccountingSystem.Data.Access.Helpers;
using AccountingSystem.Data.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Queries.Queries
{
    public class UserQueryProcessor : IUserQueryProcessor
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserQueryProcessor(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public IQueryable<User> Get()
        {
            var query = GetQuery();

            return query;
        }

        private IQueryable<User> GetQuery()
        {
            return _uow.Query<User>()
                .Where(x => !x.IsDeleted);
        }

        public User Get(int id)
        {
            var user = GetQuery().FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new Exception("User is not found");
            }

            return user;
        }


        public async Task<User> Create(CreateUserModel model)
        {
            var email = model.Email.Trim();

            if (GetQuery().Any(u => u.Email == email))
            {
                throw new BadRequestException("The email is already in use");
            }

            var user = _mapper.Map<User>(model);
            user.Password = user.Password.WithBCrypt();

            AddUserRoles(user, model.Roles);

            _uow.Add(user);

            await _uow.CommitAsync();
            return user;
        }

        private void AddUserRoles(User user, string[] roleNames)
        {
            user.Roles.Clear();
            
            foreach (var roleName in roleNames)
            {
                var role = _uow.Query<Role>().FirstOrDefault(x => x.Name == roleName);

                if (role == null)
                {
                    throw new NotFoundException($"Role - {roleName} is not found");
                }

                user.Roles.Add(new UserRole { User = user, Role = role });
            }
        }

        public async Task<User> Update(int id, UpdateUserModel model)
        {
            var user = GetQuery().FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            user = _mapper.Map<User>(model);

            await _uow.CommitAsync();
            return user;
        }

        public async Task Delete(int id)
        {
            var user = GetQuery().FirstOrDefault(u => u.Id == id);
            
            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            if (user.IsDeleted) return;

            user.IsDeleted = true;
            await _uow.CommitAsync();
        }

        public async Task ChangePassword(int id, ChangeUserPasswordModel model)
        {
            var user = Get(id);
            user.Password = model.Password.WithBCrypt();
            await _uow.CommitAsync();
        }
    }
}
