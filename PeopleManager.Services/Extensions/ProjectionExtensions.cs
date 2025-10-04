using PeopleManager.Dto.Results;
using PeopleManager.Model;

namespace PeopleManager.Services.Extensions
{
    public static class ProjectionExtensions
    {
        public static IQueryable<FunctionResult> ProjectToResult(this IQueryable<Function> query)
        {
            return query.Select(f => new FunctionResult
            {
                Id = f.Id,
                Name = f.Name,
                Description = f.Description,
                NumberOfPeople = f.People.Count
            });


        }

        public static IQueryable<PersonResult> ProjectToResult(this IQueryable<Person> query)
        {
            return query.Select(p => new PersonResult
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                FunctionId = p.FunctionId,
                Function = p.Function != null ? p.Function.Name : null
            });
        }
    }
}
