using System;
using System.Collections.Generic;
using System.Text;

namespace AccountingSystem.Data.Model
{
    public class Card
    {
        public Card()
        {
            CreationTime = DateTime.Now;
            Balance = 0;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string OwnerFullName { get; set; }

        public decimal Balance { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime CreationTime { get; set; }
        public DateTime DeletionTime { get; set; }

        public bool IsDeleted { get; set; }

    }
}
