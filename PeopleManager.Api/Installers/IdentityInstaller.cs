using Microsoft.AspNetCore.Identity;
using PeopleManager.Repository;

namespace PeopleManager.Api.Installers
{
    public static class IdentityInstaller
    {
        public static WebApplicationBuilder InstallIdentity(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<PeopleManagerDbContext>();

            return builder;
        }
    }
}
    