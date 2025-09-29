using Microsoft.EntityFrameworkCore;
using PeopleManager.Model;
using PeopleManager.Repository;

namespace PeopleManager.Services
{
    public class FunctionService(PeopleManagerDbContext dbContext)
    {


        public async Task<IList<Function>> Find()
        {
            var functions = await dbContext.Functions.ToListAsync();
            return functions;
        }

        public async Task<Function?> Get(int id)
        {
            var function = await dbContext.Functions.FirstOrDefaultAsync(f => f.Id == id);
            return function;
        }

        public async Task<Function?> Create(Function function)
        {
            if (string.IsNullOrWhiteSpace(function.Name))
            {
                return null;
            }

            dbContext.Functions.Add(function);

            await dbContext.SaveChangesAsync();

            return function;
        }

        public async Task<Function?> Update(int id, Function function)
        {
            var dbFunction = await Get(id);

            if (dbFunction == null)
            {
                return null;
            }

            dbFunction.Name = function.Name;
            dbFunction.Description = function.Description;

            await dbContext.SaveChangesAsync();

            return dbFunction;
        }

        public async Task Delete(int id)
        {
            var function = await Get(id);

            if (function is null)
            {
                return;
            }
            //var function = new Function { Id = id, Name = string.Empty };
            //_dbContext.Functions.Attach(function);

            dbContext.Functions.Remove(function);

            await dbContext.SaveChangesAsync();
        }
    }
}
