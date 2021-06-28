using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccountingSystem.Api.Models.Cards
{
    public class CreateCardModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
