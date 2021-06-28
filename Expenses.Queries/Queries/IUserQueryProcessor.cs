using AccountingSystem.Api.Models.Users;
using AccountingSystem.Data.Model;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingSystem.Queries.Queries
{
    public interface IUserQueryProcessor
    {
        IQueryable<User> Get();
        User Get(int id);
        Task<User> Create(CreateUserModel model);
        Task<User> Update(int id, UpdateUserModel model);
        Task Delete(int id);
        Task ChangePassword(int id, ChangeUserPasswordModel model);

        Task<Role> CreateRole(string name);
        void GrantRole(int id, string[] roleNames);
    }
}
