using System.ComponentModel.DataAnnotations;

namespace AccountingSystem.Api.Models.Cards
{
    public class UpdateCardModel
    {
        [Required]
        public string Name { get; set; }
    }
}
