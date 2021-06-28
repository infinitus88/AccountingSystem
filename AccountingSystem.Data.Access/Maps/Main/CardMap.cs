using AccountingSystem.Data.Access.Maps.Common;
using AccountingSystem.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountingSystem.Data.Access.Maps.Main
{
    public class CardMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Card>()
                .ToTable("Cards")
                .HasKey(x => x.Id);
        }
    }
}
