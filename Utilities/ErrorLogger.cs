using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AppMVC.Utilities
{
    public static class ErrorLogger
    {
        public static void LogModelStateErrors(ModelStateDictionary modelState)
        {
            foreach (var entry in modelState.Values)
            {
                foreach (var error in entry.Errors)
                {
                    // log error
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
            }
        }
    }

}
