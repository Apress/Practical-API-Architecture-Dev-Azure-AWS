using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MassRoverAPI.QuickStartSample.Errors
{
    public static class ErrorService
    {
        public static ErrorMessage GetRequestContentMismatchErrorMessage()
        {
            return new RequestContentErrorMessage
            {
                Title = $"Request content mismatch",
                Detail = $"Error in the request context."
            };
        }

        public static ErrorMessage GetEntityNotFoundErrorMessage(Type entity, int id)
        {
            return new EntityNotFoundErrorMessage
            {
                Title = $"{entity.Name} not found",
                Detail = $"No {entity.Name.ToLower()} found for the supplied id - {id}"
            };
        }
    }
}
