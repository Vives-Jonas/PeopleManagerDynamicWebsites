using Microsoft.EntityFrameworkCore;
using PeopleManager.Api.Installers;
using PeopleManager.Repository;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

//Use databaseInstaller and swaggerInstaller and servicesInstaller and ApiInstaller and authenticationInstaller and IdentityInstaller
//via chaining door return (van WebApplicationBuilder) in de extension method
builder
    .InstallApi()
    .InstallSwagger()
    .InstallDatabase()
    .InstallServices()
    .InstallAuthentication()
    .InstallIdentity();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    
    
    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<PeopleManagerDbContext>();
    if (dbContext.Database.IsInMemory())
    {
       await dbContext.Seed();
    }
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
