using AccountingSystem.Data.Access.Maps.Common;
using AccountingSystem.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Data.Access.Maps.Main
{
    public class UserMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<User>()
                .ToTable("Users")
                .HasKey(x => x.Id);
        }
    }
}
