namespace PeopleManager.Api.Installers
{
    public static class ApiInstaller
    {
        public static WebApplicationBuilder InstallApi(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            return builder;
        }
    }
}
