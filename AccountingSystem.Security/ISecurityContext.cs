using AccountingSystem.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountingSystem.Security
{
    public interface ISecurityContext
    {
        User User { get; }

        bool IsAdministrator { get; }
    }
}
