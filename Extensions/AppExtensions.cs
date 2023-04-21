using Microsoft.AspNetCore.Builder;

namespace AppMVC.Extensions
{
    public static class AppExtensions
    {
        public static void AddStatusCodePage(this IApplicationBuilder app)
        {
            app.UseStatusCodePages(appError =>
            {
                appError.Run(async context =>
                {
                    var response = context.Response;
                    var code = response.StatusCode;

                    var content = code switch
                    {
                        404 => "Page not found",
                        500 => "Internal server error",
                        _ => "Unknown error"
                    };

                    await response.WriteAsync(content);
                });
            });
        }
    }
}