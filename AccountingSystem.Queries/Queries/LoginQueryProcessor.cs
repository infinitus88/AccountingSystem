using AccountingSystem.Api.Models.Login;
using AccountingSystem.Api.Models.Users;
using AccountingSystem.Data.Access.DAL;
using AccountingSystem.Data.Model;
using AccountingSystem.Queries.Models;
using AccountingSystem.Security;
using AccountingSystem.Security.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;
using AccountingSystem.Api.Common.Exceptions;
using AccountingSystem.Data.Access.Helpers;

namespace AccountingSystem.Queries.Queries
{
    public class LoginQueryProcessor : ILoginQueryProcessor
    {
        private readonly IUnitOfWork _uow;
        private readonly ITokenBuilder _tokenBuilder;
        private readonly IUserQueryProcessor _usersQueryProcessor;
        private readonly ISecurityContext _securityContext;
        private Random _random;

        public LoginQueryProcessor(IUnitOfWork uow, ITokenBuilder tokenBuilder, IUserQueryProcessor usersQueryProcessor, ISecurityContext securityContext)
        {
            _random = new Random();
            _uow = uow;
            _tokenBuilder = tokenBuilder;
            _usersQueryProcessor = usersQueryProcessor;
            _securityContext = securityContext;
        }

        public UserWithToken Authenticate(string email, string password)
        {
            var user = _uow.Query<User>()
                .Where(u => u.Email == email && !u.IsDeleted)
                .FirstOrDefault();

            user.Roles = _uow.Query<UserRole>()
                .Where(r => r.UserId == user.Id)
                .Include(x => x.Role)
                .ToList();
            //var user = (from u in _uow.Query<User>()
            //            where u.Email == email && !u.IsDeleted
            //            select u)
            //            .Include(x => x.Roles)
            //            .ThenInclude(x => x.Role)
            //            .FirstOrDefault();

            if (user == null)
            {
                throw new BadRequestException("username/password aren't right");
            }

            if (string.IsNullOrWhiteSpace(password) || !user.Password.VerifyWithBCrypt(password))
            {
                throw new BadRequestException("username/password aren't right");
            }

            var expiresIn = DateTime.Now + TokenAuthOption.ExpiresSpan;
            var token = _tokenBuilder.Build(user.Email, user.Roles.Select(x => x.Role.Name).ToArray(), expiresIn);

            return new UserWithToken
            {
                ExpiresAt = expiresIn,
                Token = token,
                User = user
            };
        }

        public async Task<User> Register(RegisterModel model)
        {
            var requestModel = new CreateUserModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Email = model.Email
            };

            var user = await _usersQueryProcessor.Create(requestModel);
            return user;
        }

        public async Task ChangePassword(ChangeUserPasswordModel requestModel)
        {
            await _usersQueryProcessor.ChangePassword(_securityContext.User.Id, requestModel);
        }
    }
}
