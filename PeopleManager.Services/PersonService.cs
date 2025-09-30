using Microsoft.EntityFrameworkCore;
using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using PeopleManager.Model;
using PeopleManager.Repository;

namespace PeopleManager.Services
{
    public class PersonService(PeopleManagerDbContext dbContext)
    {
        public async Task<IList<PersonResult>> Find()
        {
            var people = await dbContext.People
                .Include(p => p.Function).Select(p => new PersonResult
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    Function = p.Function.Name ?? "No Function"
                })
                .ToListAsync();
            return people;
        }

        public async Task<PersonResult?> Get(int id)
        {
            var person = await dbContext.People
                .Include(p => p.Function).Select(p => new PersonResult
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    Function = p.Function.Name ?? "No Function"
                })
                .FirstOrDefaultAsync(p => p.Id == id);
            return person;
        }

        public async Task<PersonResult?> Create(PersonRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName))
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(request.LastName))
            {
                return null;
            }

            var person = new Person
            {
                FirstName =  request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
            };

            dbContext.People.Add(person);

            await dbContext.SaveChangesAsync();

            return await Get(person.Id);
        }


        public async Task<PersonResult?> Update(int id, PersonRequest request)
        {
            var person = await dbContext.People.FirstOrDefaultAsync(p => p.Id == id);
            ;

            if (person == null)
            {
                return null;
            }

            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.Email = request.Email;

            await dbContext.SaveChangesAsync();

            return await Get(person.Id);
        }

        public async Task Delete(int id)
        {
            var person = await dbContext.People.FirstOrDefaultAsync(p => p.Id == id);

            if (person is null)
            {
                return;
            }
            //var person = new Person { Id = id, FirstName = string.Empty, LastName = string.Empty };
            //_dbContext.People.Attach(person);

            dbContext.People.Remove(person);

            await dbContext.SaveChangesAsync();
        }
    }
}
