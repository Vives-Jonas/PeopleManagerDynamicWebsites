using Microsoft.EntityFrameworkCore;
using PeopleManager.Model;
using PeopleManager.Repository;

namespace PeopleManager.Services
{
    public class PersonService(PeopleManagerDbContext dbContext)
    {
        public async Task<IList<Person>> Find()
        {
            var people = await dbContext.People
                .Include(p => p.Function)
                .ToListAsync();
            return people;
        }

        public async Task<Person?> Get(int id)
        {
            var person = await dbContext.People
                .Include(p => p.Function)
                .FirstOrDefaultAsync(p => p.Id == id);
            return person;
        }

        public async Task<Person?> Create(Person person)
        {
            if (string.IsNullOrWhiteSpace(person.FirstName))
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(person.LastName))
            {
                return null;
            }

            dbContext.People.Add(person);

            await dbContext.SaveChangesAsync();

            return person;
        }


        public async Task<Person?> Update(int id, Person person)
        {
            var dbPerson = await Get(id);

            if (dbPerson == null)
            {
                return null;
            }

            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.Email = person.Email;
            dbPerson.FunctionId = person.FunctionId;

            await dbContext.SaveChangesAsync();

            return dbPerson;
        }

        public async Task Delete(int id)
        {
            var person = await Get(id);

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
