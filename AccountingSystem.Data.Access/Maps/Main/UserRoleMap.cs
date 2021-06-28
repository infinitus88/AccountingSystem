using AccountingSystem.Data.Access.Maps.Common;
using AccountingSystem.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Data.Access.Maps.Main
{
    public class UserRoleMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<UserRole>()
                .ToTable("UserRoles")
                .HasKey(x => x.Id);
        }
    }
}
