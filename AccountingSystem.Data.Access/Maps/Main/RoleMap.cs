using AccountingSystem.Data.Access.Maps.Common;
using AccountingSystem.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Data.Access.Maps.Main
{
    public class RoleMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Role>()
                .ToTable("Roles")
                .HasKey(x => x.Id);
        }
    }
}
