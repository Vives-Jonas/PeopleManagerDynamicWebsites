using System.ComponentModel.DataAnnotations;

namespace PeopleManager.Dto.Requests
{
    public class RegisterRequest
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
