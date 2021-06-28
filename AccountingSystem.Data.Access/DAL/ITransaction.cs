using System;
using System.Text;

namespace AccountingSystem.Data.Access.DAL
{
    public interface ITransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }

}
