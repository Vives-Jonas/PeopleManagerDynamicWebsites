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
    public class FunctionService(PeopleManagerDbContext dbContext)
    {
        public async Task<IList<FunctionResult>> Find()
        {

            return await dbContext.Functions.AsNoTracking().ProjectToResult().ToListAsync();


        }

        public async Task<FunctionResult?> Get(int id)
        {
            return await dbContext.Functions.AsNoTracking().ProjectToResult().FirstOrDefaultAsync(f => f.Id == id);


        }

        public async Task<ServiceResult<FunctionResult>> Create(FunctionRequest request)
        {
            
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return new ServiceResult<FunctionResult>().Required(nameof(request.Name));
                
            }

            var function = new Function
            {
                Name = request.Name,
                Description = request.Description,
            };

            dbContext.Functions.Add(function);

            await dbContext.SaveChangesAsync();

            var functionResult = await Get(function.Id);

            return new ServiceResult<FunctionResult>(functionResult);
            
        }

        public async Task<ServiceResult<FunctionResult>> Update(int id, FunctionRequest request)
        {
            var function = await dbContext.Functions.FirstOrDefaultAsync(f => f.Id == id);

            if (function == null)
            {
                return new ServiceResult<FunctionResult>().NotFound();
            }
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return new ServiceResult<FunctionResult>().Required(nameof(request.Name));

            }

            function.Name = request.Name;
            function.Description = request.Description;

            await dbContext.SaveChangesAsync();

            return new ServiceResult<FunctionResult>(await Get(function.Id));


        }

        public async Task<ServiceResult> Delete(int id)
        {
            var function = await dbContext.Functions.FirstOrDefaultAsync(f => f.Id == id);
           
            if (function is null)
            {
                return new ServiceResult().AlreadyRemoved();
            }

            dbContext.Functions.Remove(function);

            await dbContext.SaveChangesAsync();

            return new ServiceResult();
        }
    }
}
