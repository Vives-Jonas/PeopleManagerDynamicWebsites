
using System.ComponentModel.DataAnnotations;

namespace PeopleManager.Dto.Requests
{
    public class PersonRequest
    {
        
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public int? FunctionId { get; set; }
    }
}
