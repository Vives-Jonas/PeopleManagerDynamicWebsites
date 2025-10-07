using Microsoft.EntityFrameworkCore;
using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using PeopleManager.Model;
using PeopleManager.Repository;
using PeopleManager.Services.Extensions;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;

namespace PeopleManager.Services
{
    public class PersonService(PeopleManagerDbContext dbContext)
    {
        public async Task<IList<PersonResult>> Find()
        {
            return await dbContext.People
                .AsNoTracking()
                .Include(p => p.Function)
                .ProjectToResult()
                .ToListAsync();
        }

        public async Task<PersonResult?> Get(int id)
        {
            return await dbContext.People
                .AsNoTracking()
                .Include(p => p.Function)
                .ProjectToResult()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ServiceResult<PersonResult>> Create(PersonRequest request)
        {
            var serviceResult = new ServiceResult<PersonResult>();

            if (string.IsNullOrWhiteSpace(request.FirstName))
            {
                serviceResult.Required(nameof(request.FirstName));
            }
            if (string.IsNullOrWhiteSpace(request.LastName))
            {
                serviceResult.Required(nameof(request.LastName));
            }


            if (!serviceResult.IsSuccess)
            {
                return serviceResult;
            }

            var person = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                FunctionId = request.FunctionId
            };

            dbContext.People.Add(person);

            await dbContext.SaveChangesAsync();

            var personResult =  await Get(person.Id);

            return new ServiceResult<PersonResult>(personResult);
        }


        public async Task<ServiceResult<PersonResult>> Update(int id, PersonRequest request)
        {
            var person = await dbContext.People.FirstOrDefaultAsync(p => p.Id == id);
            ;

            if (person == null)
            {
                return new ServiceResult<PersonResult>().NotFound();
            }

            var serviceResult = new ServiceResult<PersonResult>();

            if (string.IsNullOrWhiteSpace(request.FirstName))
            {
                serviceResult.Required(nameof(request.FirstName));
            }
            if (string.IsNullOrWhiteSpace(request.LastName))
            {
                serviceResult.Required(nameof(request.LastName));
            }


            if (!serviceResult.IsSuccess)
            {
                return serviceResult;
            }

            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.Email = request.Email;
            person.FunctionId = request.FunctionId;

            await dbContext.SaveChangesAsync();

            return new ServiceResult<PersonResult>(await Get(person.Id));
        }

        public async Task<ServiceResult> Delete(int id)
        {
            var person = await dbContext.People.FirstOrDefaultAsync(p => p.Id == id);

            if (person is null)
            {
                return new ServiceResult().AlreadyRemoved();
            }
            //var person = new Person { Id = id, FirstName = string.Empty, LastName = string.Empty };
            //_dbContext.People.Attach(person);

            dbContext.People.Remove(person);

            await dbContext.SaveChangesAsync();

            return new ServiceResult();
        }
    }
}
