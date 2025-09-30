using System.ComponentModel.DataAnnotations;

namespace PeopleManager.Dto.Results
{
    public class PersonResult
    {
        public int Id { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public int? FunctionId { get; set; }
        public string? Function { get; set; }
    }
}
