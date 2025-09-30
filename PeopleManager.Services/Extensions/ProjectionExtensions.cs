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
    }
}
