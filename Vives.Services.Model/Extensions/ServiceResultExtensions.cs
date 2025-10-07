namespace Vives.Services.Model.Extensions
{
    public static class ServiceResultExtensions
    {

        public static T AlreadyRemoved<T>(this T serviceResult)
        where T : ServiceResult
        {
            serviceResult.Messages.Add(new ServiceMessage
            {
                Code = "AlreadyRemoved",
                Description = "Entity was already removed.",
                Type = ServiceMessageType.Warning
            });
            return serviceResult;
        }

        public static T NotFound<T>(this T serviceResult)
            where T : ServiceResult
        {
            serviceResult.Messages.Add(new ServiceMessage
            {
                Code = "NotFound",
                Description = "Id was not found.",
                Type = ServiceMessageType.Error
            });
            return serviceResult;
        }


        public static T Required<T>(this T serviceResult, string propertyName)
            where T : ServiceResult
        {
            serviceResult.Messages.Add(new ServiceMessage
            {
                Code = "Required",
                PropertyName = propertyName,
                Description = $"{propertyName} is required",
                Type = ServiceMessageType.Error
            });
            return serviceResult;
        }

        public static T SuccessfullyRemoved<T>(this T serviceResult)
            where T : ServiceResult
        {
            serviceResult.Messages.Add(new ServiceMessage
            {
                Code = "SuccessfullyRemoved",
                Description = "Entity was successfully already removed.",
                Type = ServiceMessageType.Warning
            });
            return serviceResult;
        }
    }
}
