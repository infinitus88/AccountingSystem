using System;
using System.Text;

namespace AccountingSystem.Security.Auth
{
    public interface ITokenBuilder
    {
        string Build(string name, string[] roles, DateTime expireDate);
    }

}
