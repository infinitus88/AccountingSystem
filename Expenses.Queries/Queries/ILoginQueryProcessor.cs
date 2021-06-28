using AccountingSystem.Api.Models.Login;
using AccountingSystem.Api.Models.Users;
using AccountingSystem.Data.Model;
using AccountingSystem.Queries.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Queries.Queries
{
    public interface ILoginQueryProcessor
    {
        UserWithToken Authenticate(string email, string password);
        Task<User> Register(RegisterModel model);
        Task ChangePassword(ChangeUserPasswordModel requestModel);
    }
}
