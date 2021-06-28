using System.ComponentModel.DataAnnotations;

namespace AccountingSystem.Api.Models.Users
{
    public class UpdateUserModel
    {
        [Required]
        public string Email { get; set; }
    }
}
