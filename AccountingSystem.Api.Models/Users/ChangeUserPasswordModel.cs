using System.ComponentModel.DataAnnotations;

namespace AccountingSystem.Api.Models.Users
{
    public class ChangeUserPasswordModel
    {
        [Required]
        public string Password { get; set; }
    }
}
