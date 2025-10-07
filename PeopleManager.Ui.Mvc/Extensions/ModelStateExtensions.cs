using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vives.Services.Model;

namespace PeopleManager.Ui.Mvc.Extensions
{
    public static class ModelStateExtensions
    {
        public static void AddServiceMessages(this ModelStateDictionary modelState, IList<ServiceMessage> messages)
        {
            foreach (var message in messages)
            {
                if (!string.IsNullOrWhiteSpace(message.PropertyName))
                {
                    modelState.AddModelError(message.PropertyName, message.Description);
                }
                else
                {
                    modelState.AddModelError("", message.Description);
                }

            }
        }
    }
}
