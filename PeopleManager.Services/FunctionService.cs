using Microsoft.EntityFrameworkCore;
using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using PeopleManager.Model;
using PeopleManager.Repository;
using PeopleManager.Services.Extensions;

namespace PeopleManager.Services
{
    public class FunctionService(PeopleManagerDbContext dbContext)
    {


        public async Task<IList<FunctionResult>> Find() 
        {
            return await dbContext.Functions.AsNoTracking().ProjectToResult().ToListAsync();
            
            
        }

        public async Task<FunctionResult?> Get(int id)
        {
            var function = await dbContext.Functions.AsNoTracking().ProjectToResult().FirstOrDefaultAsync(f => f.Id == id);

            return function;
        }

        public async Task<FunctionResult?> Create(FunctionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return null;
            }

            var function = new Function
            {
                Name = request.Name,
                Description = request.Description,
            };

            dbContext.Functions.Add(function);

            await dbContext.SaveChangesAsync();

            return await Get(function.Id);
        }

        public async Task<FunctionResult?> Update(int id, FunctionRequest request)
        {
            var function = await dbContext.Functions.FirstOrDefaultAsync(f => f.Id == id);

            if (function == null)
            {
                return null;
            }

            function.Name = request.Name;
            function.Description = request.Description;

            await dbContext.SaveChangesAsync();

            return await Get(function.Id);
        }

        public async Task Delete(int id)
        {
            var function = await dbContext.Functions.FirstOrDefaultAsync(f => f.Id == id);

            if (function is null)
            {
                return;
            }
            

            dbContext.Functions.Remove(function);

            await dbContext.SaveChangesAsync();
        }
    }
}
