namespace PeopleManager.Dto.Results
{
    public class FunctionResult
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public string? Description { get; set; }
        public int NumberOfPeople { get; set; }


    }
}
