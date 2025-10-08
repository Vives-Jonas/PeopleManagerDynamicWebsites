using Microsoft.AspNetCore.Identity;
using Vives.Services.Model;

namespace PeopleManager.Services.Extensions
{
    public static class AuthenticationResultExtensions
    {
        public static ServiceResult<IdentityUser> LoginFailed(this ServiceResult<IdentityUser> authenticationResult)
        {
            authenticationResult.Messages.Add(

                new ServiceMessage()
                {
                    Code = "LoginFailed",
                    Description = "Unable to login with provided credentials. Username/Password incorrect",
                    Type = ServiceMessageType.Error
                }
            );
            return authenticationResult;
        }
    }
}
